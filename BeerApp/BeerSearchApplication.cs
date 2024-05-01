using System.Text.Json;
using BeerApp.Domain;
using BeerApp.Output;
using BeerApp.Services;
using CommandLine;
using InCube.Core.Functional;

namespace BeerApp;

public class BeerSearchApplication(IConsoleWriter consoleWriter, IBeerSearchService beerSearchService)
{
    public async Task RunAsync(IEnumerable<string> args)
    {
        await Parser.Default
            .ParseArguments<BeerSearchApplicationAbvValue>(args)
            .WithParsedAsync(async value =>
            {
                var searchRequest = new BeerSearchRequest(value.Abv);
                var result = await beerSearchService.SearchByAbvValueAsync(searchRequest);
                HandleSearchResult(result);
            });
    }

    private void HandleSearchResult(Either<BeerSearchResult, BeerSearchError> result)
    {
        if (result.IsLeft)
        {
            var formattedTextResult = JsonSerializer.Serialize(result.Left, new JsonSerializerOptions
            {
                WriteIndented = true   
            });
            consoleWriter.WriteLine(formattedTextResult);
        }
        else
        {
            var formattedErrors = string.Join(". ", result.Right.ErrorMessages);
            consoleWriter.WriteLine(formattedErrors);
        }
    }
}