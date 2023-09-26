using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}