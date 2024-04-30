using System.Text.Json.Serialization;

namespace BeerApp.API.Responses;

public record BeerSearchResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    [JsonPropertyName("description")]
    public string Description { get; init; }
    
    [JsonPropertyName("abv")] 
    public double Alcohol { get; init; }
    
    [JsonPropertyName("ibu")] 
    public double Bitterness { get; init; }
    
    [JsonPropertyName("ingredients")] 
    public IngredientResponse Ingredients { get; init; }
}

