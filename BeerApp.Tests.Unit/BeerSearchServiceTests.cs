using AutoFixture;
using BeerApp.API;
using BeerApp.API.Responses;
using BeerApp.Domain;
using BeerApp.Mappers;
using BeerApp.Services;
using BeerApp.Validators;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BeerApp.Tests.Unit;

public class BeerSearchServiceTests
{
    private BeerSearchService _sut;
    private Mock<IPunkIpaApi> _punkIpaApiMock = null!;
    private IValidator<BeerSearchRequest> _validator = null!;
    private IFixture _fixture = null!;

    [SetUp]
    public void Setup()
    {
        _punkIpaApiMock = new Mock<IPunkIpaApi>();
        _validator = new BeerSearchRequestValidator();
        _sut = new BeerSearchService(_punkIpaApiMock.Object, _validator);
        _fixture = new Fixture();
    }

    [Test]
    public async Task SearchByAbvValueAsync_ShouldReturnResult_WhenAbvValueIsValid()
    {
        const double abv = 1.1;
        var request = new BeerSearchRequest(abv);
        var response = _fixture.Create<IReadOnlyList<BeerSearchResponse>>();
        _punkIpaApiMock.Setup(m => m.SearchByAbvLowerThanAsync(abv)).ReturnsAsync(response);
        var expected = new BeerSearchResult
        {
            Beers = response.Select(b => b.ToResult()).ToList()
        };
        
        var result = await _sut.SearchByAbvValueAsync(request);

        result.Left.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public async Task SearchByAbvValueAsync_ShouldReturnError_WhenAbvValueIsInvalid()
    {
        const double abv = 200;
        var request = new BeerSearchRequest(abv);
        var errorMessages = new[] {"Please provide percentage alcohol by value between 0 and 55"};
        var expected = new BeerSearchError(errorMessages);
        
        var result = await _sut.SearchByAbvValueAsync(request);

        result.Right.Should().BeEquivalentTo(expected);
    }
}