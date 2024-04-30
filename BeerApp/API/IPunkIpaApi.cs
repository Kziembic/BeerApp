using BeerApp.API.Responses;
using Refit;

namespace BeerApp.API;

public interface IPunkIpaApi
{
    [Get("/beers?abv_lt={abv}")]
    Task<IReadOnlyList<BeerSearchResponse>> SearchByAbvLowerThanAsync(double abv);
}