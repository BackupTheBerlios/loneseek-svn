using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace LoneSeek.Packets
{
    /// <summary>
    /// Used to mark code which is currently unkown or not yet
    /// implemented correctly. Most of the time this happens
    /// when the documentation is unclear about what that method/attribute
    /// may mean.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, Inherited=false)]
    public class NotImplemented : Attribute
    {
        public NotImplemented()
        {
            if (Debugger.IsLogging())
            {
                Debugger.Log(3, "LoneSeek", "You are using a method which is currently not yet implemented due the lack of documentation.");
            }
        }
    }
}
