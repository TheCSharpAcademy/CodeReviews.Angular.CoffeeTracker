using FluentValidation;

namespace CoffeeTracker.Api.Contracts.V1;

/// <summary>
/// The validation rules for the <see cref="CoffeeRecordRequest"/> model using FluentValidation. 
/// It ensures that the request data conforms to the expected format before processing.
/// </summary>
public class CoffeeRecordRequestValidator : AbstractValidator<CoffeeRecordRequest>
{
    #region Constructors

    public CoffeeRecordRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
    }

    #endregion
}
