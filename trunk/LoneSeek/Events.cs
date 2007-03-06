using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace LoneSeek
{
    public delegate void LoneSeekEvent ( object sender );
    public delegate void LoneSeekRoomEvent ( object sender, ChatRoom room );
    public delegate void LoneSeekChatMessageEvent ( object sender, ChatMessage message, ChatRoom room );
    public delegate void LoneSeekPrivateMessageEvent ( object sender, ChatMessage message );
    public delegate void LoneSeekUserEvent ( object sender, String user, ChatRoom room);
}
