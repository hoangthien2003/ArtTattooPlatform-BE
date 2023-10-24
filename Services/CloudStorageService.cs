using back_end.Utils.ConfigOptions;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace back_end.Services
{
    public interface ICloudStorageService
    {
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        Task DeleteFileAsync(string fileNameToDelete);
    }
    public class CloudStorageService : ICloudStorageService
    {
        private readonly GCSConfigOptions _options;
        private readonly ILogger<CloudStorageService> _logger;
        private readonly GoogleCredential _googleCredential;
        public CloudStorageService(IOptions<GCSConfigOptions> options, ILogger<CloudStorageService> logger)
        {
            _options = options.Value;
            _logger = logger;
            
            try
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environment == Environments.Production)
                {
                    //Store the json file in Secrets.
                    _googleCredential = GoogleCredential.FromJson(_options.GCPStorageAuthFile);
                }
                else
                {
                    _googleCredential = GoogleCredential.FromFile(_options.GCPStorageAuthFile);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                throw;
            }
        }
        public Task DeleteFileAsync(string fileNameToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            try
            {
                _logger.LogInformation($"Uploading: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                using (var memoryStream = new MemoryStream())
                {
                    await fileToUpload.CopyToAsync(memoryStream);
                    //Create Storage Client from Google Credential
                    using (var storageClient = StorageClient.Create(_googleCredential))
                    {
                        //upload file stream
                        var uploadedFile = await storageClient.UploadObjectAsync(
                            _options.GoogleCloudStorageBucketName, fileNameToSave, 
                            fileToUpload.ContentType, memoryStream);
                        _logger.LogInformation($"Uploaded: file {fileNameToSave} to storage {_options.GoogleCloudStorageBucketName}");
                        return uploadedFile.MediaLink;
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while uploading file {fileNameToSave}: {ex.Message}");
                throw;
            }
        }
    }
}
