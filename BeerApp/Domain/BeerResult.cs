namespace BeerApp.Domain;

public record BeerResult
{
    public string Name { get; init; }
    
    public string Description { get; init; }
    
    public double Alcohol { get; init; }
    
    public double Bitterness { get; init; }
    
    public IReadOnlyList<string> HopsUsed { get; init; }
}