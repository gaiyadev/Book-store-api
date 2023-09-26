using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class InternalServerException : ApplicationException
{
    public InternalServerException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}