using System.Net;

namespace Priyosaj.Core.Utils;

public abstract class ABaseException : Exception
{
    protected ABaseException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}

public class NotFoundException : ABaseException
{
    public NotFoundException(string? message = null) : base((int)HttpStatusCode.NotFound,
        message ?? "Resource Not Found")
    {
    }
}

public class UnAuthorizedException : ABaseException
{
    public UnAuthorizedException(string? message = null) : base((int)HttpStatusCode.Unauthorized,
        message ?? "UnAuthorized Access")
    {
    }
}