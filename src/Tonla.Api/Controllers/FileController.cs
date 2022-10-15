using Microsoft.AspNetCore.Mvc;
using Tonla.Core.Common.Models.Requests;
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
    public async Task Upload([FromServices] IUploadFilesHandler handler, params IFormFile[]? files)
    {
        var cts = new CancellationTokenSource();
        await handler.ExecuteAsync(new UploadFilesRequest(files), cts.Token);
    }
}