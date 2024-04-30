using BeerApp.Domain;
using InCube.Core.Functional;

namespace BeerApp.Services;

public interface IBeerSearchService
{
    Task<Either<BeerSearchResult, BeerSearchError>> SearchByAbvValueAsync(BeerSearchRequest request);
}