﻿using System.Runtime.Serialization;

namespace BlogApp.WebApp.Exceptions
{
    public class InvalidEmailOrPasswordException : Exception
    {
        public InvalidEmailOrPasswordException()
        {
        }

        public InvalidEmailOrPasswordException(string? message) : base(message)
        {
        }

        public InvalidEmailOrPasswordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
