using Tonla.Core.Common.Models.Requests;
using Tonla.Core.Common.Models.Responses;
using Tonla.Core.MusicUploader.Savers;
using Tonla.Core.MusicUploader.Validators;

namespace Tonla.Core.MusicUploader.Handlers;

public class UploadFilesHandler : IUploadFilesHandler
{
    private readonly UploadFilesValidator _validator;
    private readonly IFileSaver _fileSaver;

    public UploadFilesHandler(UploadFilesValidator validator, IFileSaver fileSaver)
    {
        _validator = validator;
        _fileSaver = fileSaver;
    }
    
    public async Task<UploadFilesResponse> ExecuteAsync(UploadFilesRequest request, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync(request, token);

        var response = new UploadFilesResponse(validationResult);
        
        if (!validationResult.IsValid) return response;

        await _fileSaver.SaveAsync(request, token);

        return response;
    }
}