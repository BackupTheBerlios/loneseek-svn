using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LoneSeek.Packets;
using LoneSeek.FileSharing;

namespace LoneSeek
{
    public enum SoulSeekVersion
    {
        /// <summary>
        /// Latest version of the official SoulSeek client.
        /// </summary>
        Latest = 156    // Version 156 the latest.
    }

    public enum UserStatus
    {
        /// <summary>
        /// Unkown status.
        /// </summary>
        Unkown = -1,
        /// <summary>
        /// The user is offline.
        /// </summary>
        Offline = 0,
        /// <summary>
        /// We/the user is marked as being away.
        /// </summary>
        Away = 1,
        /// <summary>
        /// We/the user is online.
        /// </summary>
        Online = 2
    }

    /// <summary>
    /// Represents the connector class to the SoulSeek network.
    /// </summary>
    public partial class LoneSeekClient
    {
        private Socket socket = null;
        private NetworkStream stream = null;
        private Queue<QueueItem> packetQueue = new Queue<QueueItem>();
        private Thread thread = null;
        private ThreadStart start = null;
        private Thread queueThread = null;
        private ThreadStart queueStart = null;
        private Socket listenSocket = null;
        private IPEndPoint endp = null;
        private AsyncCallback acceptCallback = null;
        private PeerList peerList = new PeerList();
        private FileIndex fileIndex = new FileIndex();
        private Int32 incomingPort = 0;
        private Boolean loggedIn = false;
        private String welcomeMessage = "";
        private IPAddress publicAddress = null;
        private String username = "";
        private String password = "";
        private ChatRoomList roomlist = null;


        public event LoneSeekPrivateMessageEvent OnPrivateMessage;
        public event LoneSeekEvent OnConnected;
        public event LoneSeekEvent OnDisconnected;
        public event LoneSeekEvent OnLogin;
        public event LoneSeekEvent OnRoomListUpdated;
        public event LoneSeekRoomEvent OnRoomJoined;
        public event LoneSeekRoomEvent OnRoomLeft;
        public event LoneSeekChatMessageEvent OnChatMessage;
        public event LoneSeekUserEvent OnUserJoinedRoom;
        public event LoneSeekUserEvent OnUserLeftRoom;

        /// <summary>
        /// Used to queue a packet dispatching thing.
        /// </summary>
        private class QueueItem
        {
            public Packet Packet = null;
            public Peer Peer = null;

            public QueueItem(Packet packet, Peer peer)
            {
                Peer = peer;
                Packet = packet;
            }
        }

        /// <summary>
        /// Construct a new LoneSeek client.
        /// </summary>
        public LoneSeekClient()
        {
            start = new ThreadStart(RecvThread);
            queueStart = new ThreadStart(DispatchThread);
            acceptCallback = new AsyncCallback(AcceptCallback);
            roomlist = new ChatRoomList(this);

            if (!PacketFactory.Initialized)
            { // Initialize factory.
                PacketFactory.Initialize();
            }
            // Initialize dispatcher
            InitializeDispatcher();
        }

        /// <summary>
        /// Retrieves or sets the incoming port.
        /// </summary>
        public int IncomingPort
        {
            get { return incomingPort; }
            set 
            { 
                incomingPort = value;
                endp = new IPEndPoint(IPAddress.Any, incomingPort);
            }
        }

        /// <summary>
        /// Returns an array of peers connected to the client.
        /// </summary>
        public PeerList Peers
        {
            get { return peerList; }
        }

        /// <summary>
        /// Retrieves a list of shared files.
        /// </summary>
        public FileIndex FileIndexer
        {
            get { return fileIndex; }
        }

        /// <summary>
        /// Retrieves if the instance is currently connected to a network.
        /// </summary>
        public Boolean Connected
        {
            get { return socket != null && socket.Connected; }
        }

        /// <summary>
        /// Returns true if the is logged in.
        /// </summary>
        public Boolean LoggedIn
        {
            get { return loggedIn; }
        }

        /// <summary>
        /// Retrieves the public IP address of the client.
        /// </summary>
        public IPAddress PublicAddress
        {
            get { return publicAddress; }
        }

        /// <summary>
        /// Retrieves the welcoming message the server sent.
        /// </summary>
        public String WelcomeMessage
        {
            get { return welcomeMessage; }
        }

        /// <summary>
        /// Sets or retrieves the username.
        /// </summary>
        public String Username
        {
            get { return username; }
            set
            {
                if (value == null)
                {
                    username = String.Empty;
                }
                else
                {
                    username = value;
                }
            }
        }

        /// <summary>
        /// Sets or retrieves the password.
        /// </summary>
        public String Password
        {
            get { return password; }
            set 
            {
                if (value == null)
                {
                    password = String.Empty;
                }
                else
                {
                    password = value;
                }
            }
        }

        /// <summary>
        /// Retrieves a list of available chat rooms.
        /// </summary>
        public ChatRoomList ChatRooms
        {
            get { return roomlist; }
        }

        /// <summary>
        /// Called everytime someone want's a connection with us.
        /// </summary>
        /// <param name="result"></param>
        private void AcceptCallback(IAsyncResult result)
        {
            bool reset = true;
            try
            {
                Socket client = null;
                Peer peer = null;

                // Accept him.
                client = listenSocket.EndAccept(result);
                peer = new Peer(client);
                // Pushback peer
                peerList.Add(peer);
            }
            catch (ObjectDisposedException)
            { // Do not reset an AcceptCallback
                reset = false;
            }
            finally
            { // Reset accept callback
                if ( reset ) listenSocket.BeginAccept(acceptCallback, null);
            }
        }

        /// <summary>
        /// Dispatches the given packet.
        /// </summary>
        private void DispatchThread()
        {
            try
            {
                while (true)
                { // Forever
                    Thread.Sleep(1000);
                    QueueItem item = null;
                    int count = 0;

                    lock (packetQueue)
                    { // Get count
                        count = packetQueue.Count;
                    }
                    if (count > 0)
                    { // But only if we have a packet
                        lock (packetQueue)
                        { // Get a packet and dispatch it
                            item = packetQueue.Dequeue();
                        }
                        // Now dispatch the packet
                        Dispatch(item.Packet, item.Peer);
                    }
                }
            }
            catch (ThreadAbortException)
            { // Thread might get aborted.
            }
        }

        /// <summary>
        /// Send a packet to the server.
        /// </summary>
        /// <param name="packet">Packet to send.</param>
        public void Send(Packet packet)
        {
            try
            {
                if (socket != null && socket.Connected)
                { // Only if we have a connected socket
                    lock (socket)
                    { // Lock our socket
                        // And send the packet through
                        socket.Send(packet.ToByteArray());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Send a private message to the user.
        /// </summary>
        /// <param name="message">Message to send.</param>
        public void Send(User user, String message)
        {
            if (message != "" && message != null)
            {
                MessageUserRequest request = new MessageUserRequest();

                request.User = user.Name;
                request.Message = message;
                // Send packet
                Send(request);
            }
        }

        /// <summary>
        /// Thread to do Select() on our socket.
        /// </summary>
        private void RecvThread()
        {
            try
            {
                List<Socket> mysock = new List<Socket>();
                while (true)
                {
                    if (socket == null)
                    { // Just sleep it out.
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        mysock.Clear();
                        lock (socket)
                        { // Lock the use of our socket
                            mysock.Add(socket);
                            Socket.Select(mysock, null, null, 1000);
                            if (mysock.Count > 0)
                            { // Yeah read from our socket
                                lock (stream )
                                {
                                    try
                                    { // Create a packet off our network stream
                                        Packet packet = PacketFactory.FromStream(stream);
                                        if (packet != null)
                                        { // Dispatch the packet.
                                            lock (packetQueue)
                                            { // Enqueue a non peer packet.
                                                packetQueue.Enqueue(new QueueItem(packet, null));
                                            }
                                        }
                                    }
                                    catch ( InvalidPacketException )
                                    { // Invalid packet.
                                    }
                                } // lock(stream)
                            } // if (mysock)
                        } // lock(socket)
                    } // else
                } // while
            }
            catch (ThreadAbortException)
            { // Thread may got aborted.
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Connect to a SoulSeek server.
        /// </summary>
        /// <param name="host">Hostname of the server.</param>
        /// <param name="port">Port on which the server listens.</param>
        public void Connect(String host, Int32 port)
        {
            try
            {
                if (socket == null)
                { // Only connect if we are not listening.
                    IPAddress addr;

                    if (!IPAddress.TryParse(host, out addr))
                    {
                        IPHostEntry entry = Dns.GetHostEntry(host);
                        if (entry == null || entry.AddressList.Length == 0)
                        { // Nothing found
                            throw new ApplicationException("Could connect to remote host.");
                        }
                        addr = entry.AddressList[0];
                    }
                    // Create a new socket.
                    socket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    // Connect to the remote host
                    socket.Connect(addr, port);
                    stream = new NetworkStream(socket);
                    // Create new thread to listen for packets.
                    thread = new Thread(start);
                    thread.Start();
                    // Start dispatching queue
                    queueThread = new Thread(queueStart);
                    queueThread.Start();
                    if (endp != null)
                    { // Create incoming port, but only if the user specified a port.
                        listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        // Listen on any on the port specified
                        listenSocket.Bind(endp);
                        // Start listening
                        listenSocket.Listen(5);
                        // Event handler for incoming connections.
                        listenSocket.BeginAccept(acceptCallback, null);
                    }

                    if (OnConnected != null) OnConnected(this);
                }
            }
            catch (Exception e)
            { // Throw error
                throw new LoneSeekException(e.Message);
            }
        }

        /// <summary>
        /// Disconnects from the remote server.
        /// </summary>
        /// <param name="runGC">True if the method should cause the GC to collect.</param>
        public void Disconnect(Boolean runGC)
        {
            try
            {
                if (socket != null)
                { // Shutdown and close the socket.
                    lock (socket)
                    { // Lock the socket
                        // Abort our threads
                        thread.Abort();
                        thread = null;
                        queueThread.Abort();
                        queueThread = null;
                        // Void our stream
                        stream = null;
                        socket.Shutdown(SocketShutdown.Both);
                        // Close the socket
                        socket.Close();
                        // Clear all informations
                        lock (peerList) peerList.Clear();
                        lock (packetQueue) packetQueue.Clear();
                        lock (roomlist) roomlist.Clear();
                        socket = null;
                        // Close listen socket
                        listenSocket.Shutdown(SocketShutdown.Both);
                        listenSocket.Close();
                        listenSocket = null;
                        if (OnDisconnected != null) OnDisconnected(this);
                        // And run the GC
                        if ( runGC ) GC.Collect();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Disconnects from a client and automatically running
        /// the Garbage Collector to clear up our mess.
        /// </summary>
        public void Disconnect()
        {
            Disconnect(true);
        }

        /// <summary>
        /// Called to send data after the login was successful.
        /// </summary>
        private void PostLogin()
        {
            SetWaitPortRequest waitport = new SetWaitPortRequest();
            SetSharedCountRequest shared = new SetSharedCountRequest();
            // Tell the server how much we are sharing
            shared.SharedFiles = fileIndex.Files;
            shared.SharedFolders = fileIndex.Directories;
            // Tell the server how much we share
            Send(shared);
            // Only tell him the port if we have one.
            if (endp != null)
            { // Tell the server our port
                waitport.Port = incomingPort;
                // Advertise our wait port
                Send(waitport);
            }
        }

        /// <summary>
        /// Login to the server given username and password.
        /// </summary>
        public void Login()
        {
            LoginRequest login = new LoginRequest();

            if (username == String.Empty ||
                 password == String.Empty)
            { // No username/password
                throw new LoneSeekException("Please specify username and password.");
            }
            login.Username = username;
            login.Password = password;
            // Send the packet
            Send(login);
        }
    }
}
