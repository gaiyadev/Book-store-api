using System.Text.Json.Serialization;

namespace BookstoreAPI.CustomResponses.Responses;

public class ApiResponse<T>
{
    public required string Message { get; set; }
    public int StatusCode { get; set; }
    public required string Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }
    
    public required T Data { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? accessToken { get; set; }

}