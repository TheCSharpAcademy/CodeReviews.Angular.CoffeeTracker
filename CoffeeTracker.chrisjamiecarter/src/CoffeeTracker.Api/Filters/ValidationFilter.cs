﻿using FluentValidation;

namespace CoffeeTracker.Api.Filters;

/// <summary>
/// Applies validation logic to an incoming request of type <typeparamref name="TRequest"/> using FluentValidation.
/// It acts as an endpoint filter, ensuring that requests meet validation rules before 
/// proceeding to the next middleware or endpoint logic.
/// </summary>
/// <typeparam name="TRequest">The type of the request to be validated.</typeparam>
public class ValidationFilter<TRequest> : IEndpointFilter
{
    #region Fields

    private readonly IValidator<TRequest> _validator;

    #endregion
    #region Constructors

    public ValidationFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    #endregion
    #region Methods

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TRequest>().First();

        var result = await _validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!result.IsValid)
        {
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return await next(context);
    }

    #endregion
}
