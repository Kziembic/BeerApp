namespace BeerApp.Domain;

public record BeerSearchError
{
    public BeerSearchError(IReadOnlyList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public IReadOnlyList<string> ErrorMessages { get; init; }
}