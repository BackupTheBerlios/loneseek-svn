using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek
{
    /// <summary>
    /// Represents a chat message.
    /// </summary>
    public class ChatMessage
    {
        private String message;
        private String sender;
        private String room;
        private Int32 id;
        private DateTime timestamp;
        private Boolean privMessage;

        public ChatMessage()
        {
        }

        /// <summary>
        /// Sets or retrieves the message sent.
        /// </summary>
        public String Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// Sets or retrieves the sender of the message.
        /// </summary>
        public String Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        /// <summary>
        /// Sets or retrieves the room this message belongs to. Notice if
        /// IsPrivateMessage is false this value undefined.
        /// </summary>
        public String Room
        {
            get { return room; }
            set { room = value; }
        }

        /// <summary>
        /// Sets or retrieves the message id. This value is only valid and set
        /// when IsPrivateMessage is true.
        /// </summary>
        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Sets or retrieves a Time and Date when this message has been sent.
        /// </summary>
        public DateTime Time
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        /// <summary>
        /// True if this message is a private message from the sender, false otherwise.
        /// </summary>
        public Boolean IsPrivateMessage
        {
            get { return privMessage; }
            set { privMessage = value; }
        }
    }
}
