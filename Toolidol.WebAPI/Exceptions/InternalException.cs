﻿namespace Toolidol.WebAPI.Exceptions
{
    public class InternalException : Exception
    {
        public InternalException() : base() { }
        public InternalException(string message) : base(message) { }
        public InternalException(string message, Exception innerException) : base(message, innerException) { }
    }
}
