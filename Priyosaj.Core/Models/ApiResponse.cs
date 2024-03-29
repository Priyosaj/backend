namespace Priyosaj.Core.Models;

public class ApiResponse
{
    public ApiResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        IsSuccess = statusCode.ToString().StartsWith("2");
    }

    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    
    public bool IsPaginated { get; set; }

    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A bad request, you have made",
            403 => "Uh oh, Gandalf is blocking the way!",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 =>
                "Errors are the path to the dark side.  Errors lead to anger.   Anger leads to hate.  Hate leads to career change.",
            _ => null
        };
    }
}