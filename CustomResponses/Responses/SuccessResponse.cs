using Microsoft.AspNetCore.Mvc;

namespace BookstoreAPI.CustomResponses.Responses;

public static class SuccessResponse
{
    public static IActionResult HandleCreated<T>(string message, T data )
    {
        var apiResponse = new ApiResponse<T>
        {
            Message = message,
            StatusCode = 201,
            Status = "Success",
            Data = data,
        };

        return new ObjectResult(apiResponse)
        {
            StatusCode = 201
        };
    }
    
    public static IActionResult HandleOk<T>(string message, T data, string? accessToken)
    {
        var apiResponse = new ApiResponse<T>
        {
            Message = message,
            StatusCode = 200,
            Status = "Success",
            Data = data,
            accessToken = accessToken
        };

        return new ObjectResult(apiResponse)
        {
            StatusCode = 200
        };
    }
}