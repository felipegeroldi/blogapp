using System.Runtime.Serialization;

namespace BlogApp.WebApp.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException()
        {
        }

        public EmailAlreadyRegisteredException(string? message) : base(message)
        {
        }

        public EmailAlreadyRegisteredException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
