using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS.exceptions
{
    public class DuplicateObjectException : Exception
    {
        public DuplicateObjectException()
        {
        }

        public DuplicateObjectException(string message) : base(message)
        {
        }

        public DuplicateObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
