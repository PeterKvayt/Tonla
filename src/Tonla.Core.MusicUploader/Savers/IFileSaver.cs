using Tonla.Core.Common.Models.Requests;

namespace Tonla.Core.MusicUploader.Savers;

public interface IFileSaver
{
    Task SaveAsync(UploadFilesRequest request, CancellationToken token);
}