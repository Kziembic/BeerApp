namespace BeerApp.Domain;

public record BeerSearchResult
{
    public IReadOnlyList<BeerResult> Beers { get; init; }
}