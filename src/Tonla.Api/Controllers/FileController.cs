using Microsoft.AspNetCore.Mvc;
using Tonla.Core.Common.Models.Requests;
using Tonla.Core.Common.Models.Responses;
using Tonla.Core.MusicUploader.Handlers;

namespace Tonla.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [RequestSizeLimit(50_000_000)]
    public async Task<UploadFilesResponse> Upload([FromServices] IUploadFilesHandler handler, params IFormFile[]? files)
    {
        var cts = new CancellationTokenSource();
        return await handler.ExecuteAsync(new UploadFilesRequest(files), cts.Token);
    }
}