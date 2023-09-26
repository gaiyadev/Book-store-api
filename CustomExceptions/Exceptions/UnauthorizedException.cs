using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}