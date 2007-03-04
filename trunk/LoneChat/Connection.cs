using System;
using System.Collections.Generic;
using System.Text;

namespace LoneChat
{
    /// <summary>
    /// Represents one connection.
    /// </summary>
    public class Connection
    {
        private String host;
        private Int32 port;
        private String name;
        private String user;
        private String pass;

        public Connection()
        {
        }

        /// <summary>
        /// Host/IP to connect to.
        /// </summary>
        public String Host
        {
            get { return host; }
            set { host = value; }
        }

        /// <summary>
        /// Port to connect to.
        /// </summary>
        public Int32 Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// Name of the connection.
        /// </summary>
        public String Name
        {
            set { name = value; }
            get { return name; }
        }

        /// <summary>
        /// Username to use.
        /// </summary>
        public String Username
        {
            set { user = value; }
            get { return user; }
        }

        /// <summary>
        /// Password to use.
        /// </summary>
        public String Password
        {
            set { pass = value; }
            get { return pass; }
        }
    }
}
