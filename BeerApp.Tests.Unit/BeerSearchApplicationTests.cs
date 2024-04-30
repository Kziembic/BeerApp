using System.Globalization;
using System.Text.Json;
using AutoFixture;
using BeerApp.Domain;
using BeerApp.Output;
using BeerApp.Services;
using FluentAssertions;
using InCube.Core.Functional;
using Moq;
using NUnit.Framework;

namespace BeerApp.Tests.Unit;

public class BeerSearchApplicationTests
{
    private BeerSearchApplication _sut;
    private Mock<IConsoleWriter> _consoleWriterMock;
    private Mock<IBeerSearchService> _beerSearchServiceMock;
    private IFixture _fixture = null!;

    [SetUp]
    public void SetUp()
    {
        _consoleWriterMock = new Mock<IConsoleWriter>();
        _beerSearchServiceMock = new Mock<IBeerSearchService>();
        _sut = new BeerSearchApplication(_consoleWriterMock.Object, _beerSearchServiceMock.Object);
        _fixture = new Fixture();
    }

    [Test]
    public async Task RunAsync_ShouldPassBeersToTheConsoleOnce_WhenAbvValueIsValid()
    {
        const double abv = 1.1;
        var args = new[] {$"{abv}"};

        var beerSearchResult = _fixture.Create<BeerSearchResult>();
        Either<BeerSearchResult, BeerSearchError> result = beerSearchResult;
        
        var searchRequest = new BeerSearchRequest(abv);
        _beerSearchServiceMock.Setup(m => m.SearchByAbvValueAsync(searchRequest)).ReturnsAsync(result);
        var expected = JsonSerializer.Serialize(result.Left, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var passed = string.Empty;
        _consoleWriterMock.Setup(m => m.WriteLine(It.IsAny<string>()))
            .Callback<string>(arg =>
            {
                passed = arg;
            });
        
        await _sut.RunAsync(args);

        _consoleWriterMock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once);
        passed.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public async Task RunAsync_ShouldPassErrorToTheConsoleOnce_WhenAbvValueIsValid()
    {
        const string invalidAbv = "-5";
        var args = new[] {$"{invalidAbv}"};

        var beerSearchResult = _fixture.Create<BeerSearchError>();
        Either<BeerSearchResult, BeerSearchError> result = beerSearchResult;
        
        _beerSearchServiceMock.Setup(m => m.SearchByAbvValueAsync(It.IsAny<BeerSearchRequest>())).ReturnsAsync(result);
        var expected = string.Join(". ", result.Right.ErrorMessages);

        var passed = string.Empty;
        _consoleWriterMock.Setup(m => m.WriteLine(It.IsAny<string>()))
            .Callback<string>(arg =>
            {
                passed = arg;
            });
        
        await _sut.RunAsync(args);

        _consoleWriterMock.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Once);
        passed.Should().BeEquivalentTo(expected);
    }
}