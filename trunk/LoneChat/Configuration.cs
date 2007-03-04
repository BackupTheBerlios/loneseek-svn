using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LoneChat
{
    /// <summary>
    /// Yields the configuration for the LoneChat application.
    /// </summary>
    [Serializable]
    public class Configuration
    {
        private static Configuration state = new Configuration();
        private static String ConfigFile = "lonechat.xml";

        private List<Connection> connections = new List<Connection>();

        /// <summary>
        /// So no one can create an instance of it.
        /// </summary>
        private Configuration()
        {
        }

        /// <summary>
        /// Retrieves the configuration instance.
        /// </summary>
        public static Configuration Instance
        {
            get { return state; }
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        public static void Load()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                StreamReader reader = new StreamReader(ConfigFile);
                Configuration config = null;

                config = serializer.Deserialize(reader) as Configuration;
                state = config;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        public static void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                StreamWriter writer = new StreamWriter(ConfigFile);

                serializer.Serialize(writer, state);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Sets or retrieves a list of connections.
        /// </summary>
        public List<Connection> Connections
        {
            get { return connections; }
            set { connections = value; }
        }
    }
}
