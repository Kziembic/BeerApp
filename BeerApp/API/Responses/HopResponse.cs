using System.Text.Json.Serialization;

namespace BeerApp.API.Responses;

public record HopResponse
{
    [JsonPropertyName("name")] 
    public string HopName { get; init; }
}