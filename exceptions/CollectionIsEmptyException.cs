using System;

namespace FirstProjectCS
{
    public class CollectionIsEmptyException : Exception
    {
        public CollectionIsEmptyException()
        {
        }

        public CollectionIsEmptyException(string message) : base(message)
        {
        }

        public CollectionIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}