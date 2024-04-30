namespace BeerApp.Domain;

public record BeerSearchRequest
{
    public BeerSearchRequest(double abv)
    {
        Abv = abv;
    }

    public double Abv { get; }
}