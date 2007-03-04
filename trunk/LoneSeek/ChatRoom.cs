using System;
using System.Collections.Generic;
using System.Text;
using LoneSeek.Packets;

namespace LoneSeek
{
    /// <summary>
    /// Represents a chat room.
    /// </summary>
    public class ChatRoom
    {
        private String name = "";
        private Int32 usercount = 0;
        private LoneSeekClient client = null;
        private List<User> users = new List<User>();

        /// <summary>
        /// Constructs a new ChatRoom object and initializes the name
        /// of the chat room.
        /// </summary>
        /// <param name="client">LoneSeekClient object the room belongs to.</param>
        /// <param name="name">Name of the chat room.</param>
        /// <param name="count">Number of users in the chat room.</param>
        public ChatRoom(LoneSeekClient client, String name, Int32 count)
        {
            this.client = client;
            this.name = name;
            this.usercount = count;
        }

        /// <summary>
        /// The name of the chat room.
        /// </summary>
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Number of users in the room.
        /// </summary>
        public Int32 UserCount
        {
            get { return usercount; }
        }

        /// <summary>
        /// Retrieves if we are a member of this chat room.
        /// </summary>
        public bool Joined
        {
            get { return (users.Count > 0); }
        }

        /// <summary>
        /// Users in the room.
        /// </summary>
        public List<User> Users
        {
            get { return users; }
        }

        /// <summary>
        /// Joins this room.
        /// </summary>
        public void Join()
        {
            if (!Joined)
            { // If we are not already a member
                JoinRoomRequest request = new JoinRoomRequest();

                request.Room = name;
                // We are sending the join request.
                client.Send(request);
            }
        }

        /// <summary>
        /// Leaves this room.
        /// </summary>
        public void Leave()
        {
            if (Joined)
            { // Craft a new packet.
                LeaveRoomRequest request = new LeaveRoomRequest();

                request.Room = name;
                // Send this message.
                client.Send(request);
            }
        }

        /// <summary>
        /// Sends a message to all the clients in this room.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public void SendMessage(String message)
        {
            if (!Joined)
            { // If we are not joined, than... well don't do this.
                SayChatroomRequest request = new SayChatroomRequest();

                request.Room = name;
                request.Message = message;
                // Send this packet.
                client.Send(request);
            }
        }
    }
}
