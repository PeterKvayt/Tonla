using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Tonla.Core.MusicUploader.Validators;

namespace Tonla.UnitTests.Core.MusicUploader.Validators;

[TestFixture]
public class FileValidatorTests
{
    private readonly UploadFileValidator _validator = new ();
    
    [TestCase(-1000, false)]
    [TestCase(-1, false)]
    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(10 * 1024 * 1024, true)]
    [TestCase(10 * 1024 * 1024 + 1, false)]
    public async Task FileLengthTest(int fileLength, bool isSuccess)
    {
        var stream = new FileStream("path", FileMode.Append);
        var file = new FormFile(stream, 0, fileLength, "name", "filename");

        var result = await _validator.TestValidateAsync(file);
        
        if (isSuccess)
            result.ShouldNotHaveValidationErrorFor("Files[0].Length");
        else
            result
                .ShouldHaveValidationErrorFor("Files[0].Length")
                .WithErrorCode("InvalidFileLength");
    }
}