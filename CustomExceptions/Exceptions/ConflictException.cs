using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}