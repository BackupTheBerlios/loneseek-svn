using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using LoneSeek.Packets;
using LoneSeek.FileSharing;

namespace LoneSeek
{
    /// <summary>
    /// The part which does the package dispatching.
    /// </summary>
    public partial class LoneSeekClient
    {
        private delegate void PacketDispatcher (Packet packet, Peer peer);
        private static Dictionary<PacketType, PacketDispatcher> dispatchers = null;

        /// <summary>
        /// Initializes the dispatcher engine.
        /// </summary>
        private void InitializeDispatcher()
        {
            if (dispatchers == null)
            { // Create new event
                dispatchers = new Dictionary<PacketType, PacketDispatcher>();

                // Login dispatcher
                dispatchers[PacketType.Login] = new PacketDispatcher(DispatchLogin);
                // Room list dispatcher
                dispatchers[PacketType.RoomList] = new PacketDispatcher(DispatchRoomList);
                // Join room dispatcher
                dispatchers[PacketType.JoinRoom] = new PacketDispatcher(DispatchJoinRoom);
                // Left room dispatcher
                dispatchers[PacketType.LeaveRoom] = new PacketDispatcher(DispatchLeaveRoom);
                // SayChatroom dispatcher
                dispatchers[PacketType.SayChatRoom] = new PacketDispatcher(DispatchRoomMessage);
                // Private messages
                dispatchers[PacketType.MessageUser] = new PacketDispatcher(DispatchMessageUser);
                // User left a room.
                dispatchers[PacketType.UserLeftRoom] = new PacketDispatcher(DispatchUserLeftRoom);
                // User joined a room.
                dispatchers[PacketType.UserJoinedRoom] = new PacketDispatcher(DispatchUserJoined);
            }
        }

        /// <summary>
        /// Tries to process the given packet. If no dispatcher is
        /// registered for the given packet type nothing is being done.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        public void Dispatch(Packet packet, Peer peer)
        {
            PacketDispatcher dispatcher = null;

            if (dispatchers.ContainsKey(packet.Type))
            {
                dispatcher = dispatchers[packet.Type];
                // Call dispatcher
                dispatcher(packet, peer);
            }
        }

        /// <summary>
        /// Dispatches a login packet.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchLogin(Packet packet, Peer peer)
        {
            LoginReply login = packet as LoginReply;

            try
            {
                // Now let's see if we have successfuly logged in.
                loggedIn = login.Successful;
                if (loggedIn)
                { // Read the rest only if it worked.
                    welcomeMessage = login.WelcomeMessage.Replace("\n", "\r\n");
                    publicAddress = login.IPAddress;
                    // Send the rest of our data.
                    PostLogin();
                }
                else
                { // Some how remove the old values.
                    welcomeMessage = "";
                    publicAddress = IPAddress.Any;
                }
                if (OnLogin != null) OnLogin(this);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatches a room list.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchRoomList(Packet packet, Peer peer)
        {
            RoomListReply reply = packet as RoomListReply;
            String[] rooms = reply.Rooms;
            Int32[] counts = reply.UserCounts;

            if (rooms != null && counts != null)
            {
                lock (roomlist)
                { // Clear room list.
                    roomlist.Clear();
                    // Add all items.
                    for (int i = 0; i < rooms.Length && i < counts.Length; ++i)
                    { // Create a new chat room object
                        roomlist.Add(rooms[i], counts[i]);
                    }
                }
                // Call the event
                if (OnRoomListUpdated != null) OnRoomListUpdated(this);
            }
        }

        /// <summary>
        /// Dispatch if we would like to join a room.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchJoinRoom(Packet packet, Peer peer)
        {
            JoinRoomReply reply = packet as JoinRoomReply;
            String roomname = reply.Room;
            ChatRoom room = null;
            User[] users = reply.Users;

            try
            {
                room = roomlist.Find(roomname);
                lock (room)
                {
                    // Add the users to our list.
                    room.Users.AddRange(users);
                    // Call event
                    if (OnRoomJoined != null) OnRoomJoined(this, room);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatches when we have "left a room".
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchLeaveRoom(Packet packet, Peer peer)
        {
            LeaveRoomReply reply = packet as LeaveRoomReply;
            String roomname = reply.Room;
            ChatRoom room = null;

            try
            {
                room = roomlist.Find(roomname);
                lock (room)
                { // Lock it so we can clear the user list.
                    room.Users.Clear();
                    // This would automatically cause Joined to return false.
                    if (OnRoomLeft != null) OnRoomLeft(this, room);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatches a message from a room.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchRoomMessage(Packet packet, Peer peer)
        {
            SayChatroomReply reply = packet as SayChatroomReply;
            ChatMessage message = new ChatMessage();
            ChatRoom room = null;

            try
            {
                room = roomlist.Find(reply.Room);
                if (room == null)
                { // Room must not be null.
                    throw new ArgumentNullException("room");
                }
                // Construct message.
                message.IsPrivateMessage = false;
                message.Sender = reply.User;
                message.Message = reply.Message;
                // He has sent it now.
                message.Time = DateTime.Now;
                message.Room = reply.Room;
                // Call event.
                if (OnChatMessage != null) OnChatMessage(this, message, room);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatches a private message.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchMessageUser(Packet packet, Peer peer)
        {
            MessageUserReply reply = packet as MessageUserReply;
            MessageAckedRequest ack = new MessageAckedRequest();
            ChatMessage message = new ChatMessage();

            try
            {
                ack.MessageId = reply.MessageId;
                // Send an ack out for the message
                Send(ack);

                // Now dispatch this message
                message.Id = reply.MessageId;
                message.Message = reply.Message;
                message.Sender = reply.User;
                message.Time = reply.Time;
                message.IsPrivateMessage = true;

                if (OnPrivateMessage != null) OnPrivateMessage(this, message);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatch when someone came in.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchUserJoined(Packet packet, Peer peer)
        {
            UserJoinedReply reply = packet as UserJoinedReply;

            try
            {
                ChatRoom room = null;

                // Find the chatroom it goes.
                room = roomlist.Find(reply.Room);
                lock (room)
                {
                    User user = new User(reply.User);
                    // Add the user to the room.
                    room.Users.Add(user);
                }
                if (room != null)
                { 
                    if (OnUserJoinedRoom != null) OnUserJoinedRoom(this, reply.User, room);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Dispatch when someone went out.
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="peer"></param>
        private void DispatchUserLeftRoom(Packet packet, Peer peer)
        {
            UserJoinedReply reply = packet as UserJoinedReply;

            try
            {
                ChatRoom room = null;

                // Find the chatroom it goes.
                room = roomlist.Find(reply.Room);
                lock (room)
                {
                    // Delete the user.
                    // **FIXME** Someone should improve the speed of this code.
                    for ( int i = 0; i < room.Users.Count; ++i )
                    {
                        User user = room.Users[i];
                        if (String.Compare(user.Name, reply.User) == 0)
                        {
                            room.Users.RemoveAt(i);
                            return;
                        }
                    }
                }

                if (room != null)
                {
                    if (OnUserLeftRoom != null) OnUserLeftRoom(this, reply.User, room);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
