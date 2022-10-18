using Tonla.Core.Common.Models.Requests;

namespace Tonla.Core.MusicUploader.Savers;

public class FileSaver : IFileSaver
{
    private readonly string _rootFolderPath = Path.Combine(AppContext.BaseDirectory, "files");

    public FileSaver()
    {
        Directory.CreateDirectory(_rootFolderPath);
    }
    
    public Task SaveAsync(UploadFilesRequest request, CancellationToken token)
    {
        var filesCount = request.Files.Length;
        var tasks = new Task[filesCount];
        for (int i = 0; i < filesCount; i++)
        {
            var file = request.Files[i] ?? throw new NullReferenceException("File should not be null during saving.");
            using var stream = new FileStream(Path.Combine(_rootFolderPath, file.FileName), FileMode.Create);
            tasks[i] = file.CopyToAsync(stream, token);
        }

        return Task.WhenAll(tasks);
    }
}