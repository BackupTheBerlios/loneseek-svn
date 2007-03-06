using System;
using System.Collections.Generic;
using System.Text;
using LoneSeek.Packets;

namespace LoneSeek
{
    /// <summary>
    /// Represents an user.
    /// </summary>
    public class User
    {
        private String name;
        private Int32 avgspeed;
        private Int32 cntdownloads;
        private Int32 something;
        private Int32 cntfiles;
        private Int32 cntdirs;
        private Int32 cntslotsfull;
        private UserStatus status = UserStatus.Unkown;

        /// <summary>
        /// Constructs a new empty user.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Constructs a new user with a name.
        /// </summary>
        /// <param name="name"></param>
        public User(String name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sets or retrieves the users name.
        /// </summary>
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Retrieves the average speed of the user.
        /// </summary>
        public Int32 AverageSpeed
        {
            get { return avgspeed; }
            set { avgspeed = value; }
        }

        /// <summary>
        /// Retrieves number of downloads.
        /// </summary>
        public Int32 Downloads
        {
            get { return cntdownloads; }
            set { cntdownloads = value; }
        }

        /// <summary>
        /// Not known yet.
        /// </summary>
        public Int32 Unkown
        {
            get { return something; }
            set { something = value; }
        }

        /// <summary>
        /// Retrieves the number of shared files.
        /// </summary>
        public Int32 Files
        {
            get { return cntfiles; }
            set { cntfiles = value; }
        }

        /// <summary>
        /// Retrieves the number of shared directories.
        /// </summary>
        public Int32 Directories
        {
            get { return cntdirs; }
            set { cntdirs = value; }
        }

        /// <summary>
        /// Retrieves the number of full slots.
        /// </summary>
        public Int32 FullSlots
        {
            get { return cntslotsfull; }
            set { cntslotsfull = value; }
        }

        /// <summary>
        /// Retrieves the current status of the user.
        /// </summary>
        public UserStatus Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
