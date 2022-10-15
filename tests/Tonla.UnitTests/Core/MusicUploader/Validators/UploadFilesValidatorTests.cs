using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Tonla.Core.Common.Models.Requests;
using Tonla.Core.MusicUploader.Validators;

namespace Tonla.UnitTests.Core.MusicUploader.Validators;

[TestFixture]
public class FilesValidatorTests
{
    private readonly UploadFilesValidator _validator = new ();
    
    [Test]
    public async Task FilesWhenThemNullTest()
    {
        var result = await _validator.TestValidateAsync(new UploadFilesRequest(null));
        
        result
            .ShouldHaveValidationErrorFor(x => x.Files)
            .WithErrorCode("InvalidFilesCount");
        result.Errors.Count.Should().Be(1);
    }

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(10, true)]
    [TestCase(11, false)]
    public async Task FilesCountTest(int length, bool isSuccess)
    {
        var stream = new FileStream("path", FileMode.Append);
        var files = new IFormFile[length];
        for (int i = 0; i < length; i++)
            files[i] = new FormFile(stream, 1000, 1000, "name", "filename");

        var result = await _validator.TestValidateAsync(new UploadFilesRequest(files));
        
        if (isSuccess)
            result.ShouldNotHaveValidationErrorFor(x => x.Files);
        else
        {
            result
                .ShouldHaveValidationErrorFor(x => x.Files)
                .WithErrorCode("InvalidFilesCount");
            result.Errors.Count.Should().Be(1);
        }
    }

    [Test]
    public async Task NullFileTest()
    {
        IFormFile?[] files = { null };
        
        var result = await _validator.TestValidateAsync(new UploadFilesRequest(files));
        
        result
            .ShouldHaveValidationErrorFor("Files[0]")
            .WithErrorCode("FileIsNull");
        result.Errors.Count.Should().Be(1);
    }
    
    [Test]
    public async Task NullFilesTest()
    {
        IFormFile?[] files = { null, null, null };
        
        var result = await _validator.TestValidateAsync(new UploadFilesRequest(files));
        
        result.ShouldHaveValidationErrorFor("Files[0]").WithErrorCode("FileIsNull");
        result.ShouldHaveValidationErrorFor("Files[1]").WithErrorCode("FileIsNull");
        result.ShouldHaveValidationErrorFor("Files[2]").WithErrorCode("FileIsNull");
        result.Errors.Count.Should().Be(3);
    }

    [Test]
    public async Task FilesCascadeModeTest()
    {
        var request = new UploadFilesRequest(null);
        var result = await _validator.TestValidateAsync(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(1);
    }
    
    [Test]
    public async Task FileCascadeModeTest()
    {
        var stream = new FileStream("path", FileMode.Append);
        IFormFile[] files =
        {
            new FormFile(stream, 1, 0, "name", "filename"),
            new FormFile(stream, 1, 1000, "name", "filename"),
            new FormFile(stream, 1, 0, "name", "filename")
        };
        
        var result = await _validator.TestValidateAsync(new UploadFilesRequest(files));

        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
    }
}