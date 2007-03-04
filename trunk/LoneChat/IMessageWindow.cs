using System;
using System.Collections.Generic;
using System.Text;
using LoneSeek;

namespace LoneChat
{
    enum MessageWindowType
    {
        ServerWindow = 0,
        RoomWindow = 1
    }

    interface IMessageWindow
    {
        void AddMessage(String Sender, String Message);
        
        LoneSeekClient Client
        {
            get;
            set;
        }


        MessageWindowType GetWindowType();
    }
}
