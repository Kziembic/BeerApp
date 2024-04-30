using CommandLine;

namespace BeerApp;

public class BeerSearchApplicationAbvValue
{
    [Value(0, Required = true, MetaName = "abv", HelpText = "Provides the alcohol by volume to search on (0 - 55).")]
    public double Abv { get; init; }
}