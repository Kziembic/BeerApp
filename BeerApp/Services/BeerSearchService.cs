using BeerApp.API;
using BeerApp.Domain;
using BeerApp.Mappers;
using FluentValidation;
using InCube.Core.Functional;

namespace BeerApp.Services;

public class BeerSearchService(IPunkIpaApi punkIpaApi, IValidator<BeerSearchRequest> validator) : IBeerSearchService
{
    public async Task<Either<BeerSearchResult, BeerSearchError>> SearchByAbvValueAsync(BeerSearchRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return new BeerSearchError(errorMessages);
        }

        var response = await punkIpaApi.SearchByAbvLowerThanAsync(request.Abv);

        return new BeerSearchResult
        {
            Beers = response.Select(r => r.ToResult()).ToList()
        };
    }
}