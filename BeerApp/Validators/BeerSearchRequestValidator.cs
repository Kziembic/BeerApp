using BeerApp.Domain;
using FluentValidation;

namespace BeerApp.Validators;

public class BeerSearchRequestValidator : AbstractValidator<BeerSearchRequest>
{
    public BeerSearchRequestValidator()
    {
        RuleFor(request => request.Abv)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(55)
            .WithMessage("Please provide percentage alcohol by value between 0 and 55");
    }
}