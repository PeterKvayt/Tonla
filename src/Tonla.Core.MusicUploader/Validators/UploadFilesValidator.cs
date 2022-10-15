using FluentValidation;
using Tonla.Core.Common.Models.Requests;

namespace Tonla.Core.MusicUploader.Validators;

public sealed class UploadFilesValidator : AbstractValidator<UploadFilesRequest>
{
    private readonly UploadFileValidator _fileValidator = new ();
    
    private const string InvalidFilesCount = "InvalidFilesCount";
    private const string FileIsNull = "FileIsNull";

    public UploadFilesValidator()
    {
        RuleFor(x => x.Files)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithErrorCode(InvalidFilesCount)
            .NotEmpty().WithErrorCode(InvalidFilesCount)
            .Must(x => x.Length <= 10).WithErrorCode(InvalidFilesCount);

        RuleForEach(x => x.Files)
            .NotNull().WithErrorCode(FileIsNull)
            .SetValidator(_fileValidator!).When(x => x is not null);
    }
}