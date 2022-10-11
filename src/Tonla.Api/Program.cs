using Tonla.Core.MusicUploader.Handlers;
using Tonla.Core.MusicUploader.Savers;
using Tonla.Core.MusicUploader.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUploadFilesHandler, UploadFilesHandler>();
builder.Services.AddSingleton<IFileSaver, FileSaver>();
builder.Services.AddSingleton<FilesValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();