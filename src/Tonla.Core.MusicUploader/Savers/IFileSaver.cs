using Microsoft.AspNetCore.Http;

namespace Tonla.Core.MusicUploader.Savers;

public interface IFileSaver
{
    Task SaveAsync(IFormFile[] files, CancellationToken token);
}