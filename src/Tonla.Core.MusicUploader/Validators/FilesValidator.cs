using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Tonla.Core.MusicUploader.Validators;

public sealed class FilesValidator : AbstractValidator<IFormFile[]>
{
    public FilesValidator()
    {
    }
}