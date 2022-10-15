using Microsoft.AspNetCore.Http;
using Tonla.Core.Common.Models.Requests;
using Tonla.Core.Common.Models.Responses;

namespace Tonla.Core.MusicUploader.Handlers;

public interface IUploadFilesHandler
{
    Task<UploadFilesResponse> ExecuteAsync(UploadFilesRequest request, CancellationToken token);
}