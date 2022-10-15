using Microsoft.AspNetCore.Http;

namespace Tonla.Core.Common.Models.Requests;

public record UploadFilesRequest
{
    public UploadFilesRequest(IFormFile?[]? files)
    {
        Files = files ?? Array.Empty<IFormFile>();
    }
    
    public IFormFile?[] Files { get; }
}