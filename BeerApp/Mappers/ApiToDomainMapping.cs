using BeerApp.API.Responses;
using BeerApp.Domain;

namespace BeerApp.Mappers;

public static class ApiToDomainMapping
{
    public static BeerResult ToResult(this BeerSearchResponse response)
    {
        return new BeerResult
        {
            Name = response.Name,
            Description = response.Description,
            Alcohol = response.Alcohol,
            Bitterness = response.Bitterness,
            HopsUsed = response.Ingredients.Hops.Select(h => h.HopName).Distinct().ToList()
        };
    }
}