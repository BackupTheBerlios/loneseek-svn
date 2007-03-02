using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek
{
    /// <summary>
    /// Represents a list of all chat rooms.
    /// </summary>
    public class ChatRoomList : List<ChatRoom>
    {
        private LoneSeekClient client = null;

        /// <summary>
        /// Constructs a new empty chat room list.
        /// </summary>
        public ChatRoomList(LoneSeekClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Add a new room.
        /// </summary>
        /// <param name="room">Name of the room.</param>
        /// <param name="count">Number of users within this room.</param>
        public void Add(String room, Int32 users)
        {
            ChatRoom item = null;

            if (room == null)
            { // Must not be null.
                throw new ArgumentNullException("room");
            }
            item = new ChatRoom(client, room, users);
            // Add the room.
            Add(item);
        }


        /// <summary>
        /// Finds a chatroom with the given name.
        /// </summary>
        /// <param name="name">Name to look for.</param>
        /// <param name="ignoreCase">True if the search should be case insensitive.</param>
        /// <returns></returns>
        public ChatRoom Find(String name, Boolean ignoreCase)
        {
            foreach (ChatRoom room in this)
            {
                if (String.Compare(room.Name, name, ignoreCase) == 0)
                {
                    return room;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds a chatroom with the given name.
        /// </summary>
        /// <param name="name">Name to look for.</param>
        /// <returns></returns>
        public ChatRoom Find(String name)
        {
            return Find(name, false);
        }
	
    }
}
