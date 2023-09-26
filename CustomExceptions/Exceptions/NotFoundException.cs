using System.Net;

namespace BookstoreAPI.CustomExceptions.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}