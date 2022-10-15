using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Tonla.Core.MusicUploader.Validators;

public sealed class UploadFileValidator : AbstractValidator<IFormFile>
{
    private const string InvalidFileLength = "InvalidFileLength";
    
    public UploadFileValidator()
    {
        RuleFor(x => x.Length)
            .GreaterThan(0).WithErrorCode(InvalidFileLength)
            .LessThanOrEqualTo(10 * 1024 * 1024).WithErrorCode(InvalidFileLength);
    }
}