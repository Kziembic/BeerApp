using System.Text.Json.Serialization;

namespace BeerApp.API.Responses;

public record IngredientResponse
{
    [JsonPropertyName("hops")] 
    public IReadOnlyList<HopResponse> Hops { get; init; }
}