using Microsoft.AspNetCore.Http;

namespace Tonla.Core.MusicUploader.Savers;

public class FileSaver : IFileSaver
{
    private readonly string _rootFolderPath = Path.Combine(AppContext.BaseDirectory, "files");

    public FileSaver()
    {
        Directory.CreateDirectory(_rootFolderPath);
    }
    
    public Task SaveAsync(IFormFile[] files, CancellationToken token)
    {
        var tasks = new Task[files.Length];
        for (int i = 0; i < files.Length; i++)
        {
            var file = files[i];
            using var stream = new FileStream(Path.Combine(_rootFolderPath, file.FileName), FileMode.Create);
            tasks[i] = file.CopyToAsync(stream, token);
        }

        return Task.WhenAll(tasks);
    }
}