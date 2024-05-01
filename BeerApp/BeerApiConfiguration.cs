namespace BeerApp;

public abstract record BeerApiConfiguration
{
    public const string BeerApiKey = "BeerApi:BaseAddress";

    public string BaseAddress { get; set; }
}