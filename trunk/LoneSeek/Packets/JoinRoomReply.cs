using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Sent by the server when we successfuly joined a room. This packet
    /// is somewhat special, since it's data format is falling out of
    /// the scheme. We override Parse() here and read the data ourselfs.
    /// </summary>
    public class JoinRoomReply : Packet
    {
        private List<User> users = null;
        private String room = "";

        public JoinRoomReply()
        {
            this.type = PacketType.JoinRoom;
            this.direction = PacketDirection.ServerToClient;
            users = new List<User>();
        }

        /// <summary>
        /// Parses the data format
        /// </summary>
        /// <param name="array"></param>
        public override void Parse(byte[] array)
        {
            MemoryStream memory = new MemoryStream(array);
            PacketStream stream = new PacketStream(memory);

            try
            {
                Int32 statcount = 0;
                String[] users;
                Int32[] status;
                List<Int32> avgspeed = new List<Int32>();
                List<Int32> something = new List<Int32>();
                List<Int32> cntfiles = new List<Int32>();
                List<Int32> cntdirs = new List<Int32>();
                Int32[] cntslotsfull;

                // The first thing is the room we have joined.
                room = stream.ReadString();
                // Second: Users in this room.
                users = stream.ReadStrings();
                // Third: Status of each user
                status = stream.ReadInts();
                // Fourth: Read number of statistics
                statcount = stream.ReadInt();
                // Read each
                while (statcount > 0)
                {
                    Int32 read = 0;

                    // Read average speed.
                    read = stream.ReadInt();
                    avgspeed.Add(read);
                    // Read something
                    read = stream.ReadInt();
                    something.Add(read);
                    // Read number of files.
                    read = stream.ReadInt();
                    cntfiles.Add(read);
                    // Read Number of directories
                    read = stream.ReadInt();
                    cntdirs.Add(read);
                    --statcount;
                }
                // Last but not least: read states.
                cntslotsfull = stream.ReadInts();
                // Now assemble the users
                for (int i = 0; i < users.Length; ++i)
                {
                    User user = new User(users[i]);
                    // Now apply the rest of the data.
                    if (i < status.Length)
                    { // Status of the user
                        user.Status = (UserStatus)status[i];
                    }
                    if (i < avgspeed.Count)
                    { // Average speed
                        user.AverageSpeed = avgspeed[i];
                    }
                    if (i < something.Count)
                    { // Unkown
                        user.Unkown = something[i];
                    }
                    if (i < cntfiles.Count)
                    { // Number of files.
                        user.Files = cntfiles[i];
                    }
                    if (i < cntdirs.Count)
                    { // Number of directories.
                        user.Directories = cntdirs[i];
                    }
                    if (i < cntslotsfull.Length)
                    { // Number of full slots.
                        user.FullSlots = cntslotsfull[i];
                    }
                    // And finally add him to our list
                    this.users.Add(user);
                }
            }
            catch (Exception)
            { // There was an error.
            }
        }

        /// <summary>
        /// Retrieves the room.
        /// </summary>
        public String Room
        {
            get { return room; }
        }

        /// <summary>
        /// Returns a number of users.
        /// </summary>
        public User[] Users
        {
            get { return users.ToArray(); }
        }
    }
}
