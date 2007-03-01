using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek.Packets
{
    public enum PacketType
    {
        /// <summary>
        /// Packet type is unkown.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// We wish to login.
        /// </summary>
        Login = 1,
        /// <summary>
        /// We want set the port we are reachable over.
        /// </summary>
        SetWaitPort = 2,
        /// <summary>
        /// We want a users remote ip.
        /// </summary>
        GetPeerAddress = 3,
        /// <summary>
        /// We want to monitor a users status.
        /// </summary>
        AddUser = 5,
        /// <summary>
        /// We want to see the users status.
        /// </summary>
        GetUserStatus = 7,
        /// <summary>
        /// We want to say something to the chat room.
        /// </summary>
        SayChatRoom = 13,
        /// <summary>
        /// We want to join a chat room.
        /// </summary>
        JoinRoom = 14,
        /// <summary>
        /// We want to leave the room.
        /// </summary>
        LeaveRoom = 15,
        /// <summary>
        /// We get notified that a user joins the room.
        /// </summary>
        UserJoinedRoom = 16,
        /// <summary>
        /// We get notified that a user leaves the room.
        /// </summary>
        UserLeftRoom = 17,
        /// <summary>
        /// We cannot connect to a peer directly, so we ask the server
        /// to tell him that he should connect to us instead.
        /// </summary>
        ConnectToPeer = 18,
        /// <summary>
        /// We would like to send a private message to a user.
        /// </summary>
        MessageUser = 22,
        /// <summary>
        /// We would like to confirm the receive of a message.
        /// </summary>
        MessageAck = 23,
        FileSearch = 24,
        SetStatus = 28,
        SharedFoldersFiles = 35,
        GetUserStats = 36,
        QueueDownloads = 40,
        PlaceInLineResponse = 60,
        RoomAdded = 62,
        RoomRemoved = 63,
        RoomList = 64,
        ExactFileSearch = 65,
        AdminMessage = 66,
        GlobalUserList = 67,
        TunneledMessage = 68,
        PrivilegedUsers = 69,
        /// <summary>
        /// Whatever that may be.
        /// </summary>
        GetParentList = 71,
        AddToPrivileged = 91,
        CheckPrivileges = 92,
        CantConnectToPeer = 1001
    }

    /// <summary>
    /// Direction of the packet.
    /// </summary>
    public enum PacketDirection
    {
        /// <summary>
        /// Something we send.
        /// </summary>
        ClientToServer = 0,
        /// <summary>
        /// Something we receive.
        /// </summary>
        ServerToClient = 1
    }
}
