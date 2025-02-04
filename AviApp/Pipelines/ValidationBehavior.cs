using AviApp.Results;
using FluentValidation;
using MediatR;

namespace AviApp.Pipelines;

public class ValidationBehavior<TRequest, TResponse>(
    IHttpContextAccessor httpContextAccessor,
    IValidator<TRequest>? validator = null)
    :
        IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await next();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.BadRequest(
                validationFailure.ErrorMessage,
                validationFailure.ErrorCode));
            
        httpContextAccessor.HttpContext.Items.Add("Errors", errors);

        return (dynamic)errors;
    }
}