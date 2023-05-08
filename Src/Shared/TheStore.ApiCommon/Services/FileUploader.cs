using Microsoft.AspNetCore.Http;
using System.IO.Abstractions;

namespace TheStore.ApiCommon.Services
{
	public class FileUploader : IFileUploader
	{
		private readonly IFileSystem fileSystem;

		public FileUploader(IFileSystem fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public async Task<string> UploadFileAsync(string location, IFormFile formFile, CancellationToken cancellationToken = default)
		{
			var fileName = formFile.FileName;
			var fileFullPath = fileSystem.Path.Combine(location, fileName);
			fileSystem.Directory.CreateDirectory(location);

			using var stream = fileSystem.FileStream.New(fileFullPath, FileMode.Create);
			await formFile.CopyToAsync(stream, cancellationToken);
			await stream.FlushAsync(cancellationToken);

			return fileFullPath;
		}

		public async Task<IEnumerable<string>> UploadFilesAsync(string location, IEnumerable<IFormFile> formFiles, CancellationToken cancellationToken = default)
		{
			var filePaths = new List<string>();

			foreach (var file in formFiles)
			{
				var path = await UploadFileAsync(location, file, cancellationToken);
				filePaths.Add(path);
			}

			return filePaths;
		}
	}
}