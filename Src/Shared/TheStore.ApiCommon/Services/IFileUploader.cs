using Microsoft.AspNetCore.Http;

namespace TheStore.ApiCommon.Services
{
	public interface IFileUploader
	{
		public Task<string> UploadFileAsync(string location, IFormFile formFile, CancellationToken cancellationToken = default);
		public Task<IEnumerable<string>> UploadFilesAsync(string location, IEnumerable<IFormFile> formFiles, CancellationToken cancellationToken = default);
	}
}