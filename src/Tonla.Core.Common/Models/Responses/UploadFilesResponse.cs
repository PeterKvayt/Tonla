
using FluentValidation.Results;

namespace Tonla.Core.Common.Models.Responses;

public record UploadFilesResponse : BaseResponse
{
    public UploadFilesResponse(ValidationResult result) : base(result)
    {
    }
}