using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}