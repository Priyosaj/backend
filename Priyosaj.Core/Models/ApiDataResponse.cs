namespace Priyosaj.Core.Models;

public class ApiDataResponse<TRes> : ApiResponse
{
    public ApiDataResponse(TRes data, int statusCode, string? message = null) : base(statusCode, message)
    {
        Data = data;
    }
    
    public TRes Data { get; set; }
}