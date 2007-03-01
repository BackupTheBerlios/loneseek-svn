using System;
using System.Collections.Generic;
using System.Text;

namespace LoneSeek
{
    /// <summary>
    /// Thrown when there happened an error in the subsystem.
    /// </summary>
    public class LoneSeekException : ApplicationException
    {
        public LoneSeekException()
        {
        }

        public LoneSeekException(string message)
            : base(message)
        {
        }
    }
}
