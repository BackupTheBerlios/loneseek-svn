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
    }
}
