using System.Net;

namespace BookstoreAPI.CustomExceptions;

public class ApplicationException : Exception
{
    protected ApplicationException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    public HttpStatusCode StatusCode { get; private set; }

    public override string ToString()
    {
        return $"{Message}";
    }
    }