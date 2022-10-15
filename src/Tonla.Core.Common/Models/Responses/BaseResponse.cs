
using FluentValidation.Results;

namespace Tonla.Core.Common.Models.Responses;

public abstract record BaseResponse
{
    protected BaseResponse(ValidationResult result)
    {
        foreach (var error in result.Errors)
        {
            if (error is null) continue;
            ErrorMessages.Add(error.ErrorCode);
        }
    }

    public HashSet<string> ErrorMessages { get; } = new();
}