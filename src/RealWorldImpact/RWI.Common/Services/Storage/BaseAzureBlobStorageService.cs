using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.RetryPolicies;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Storage
{
    public abstract class BaseAzureBlobStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly string _containerName;

        private bool _isConnected;
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private string _connectionString;

        public BaseAzureBlobStorageService(
            IConfiguration configuration,
            string containerName
            )
        {
            _configuration = configuration;
            _containerName = containerName;

            _isConnected = false;
        }

        private void _Connect()
        {
            if (!_isConnected)
            {
                _connectionString = _configuration.GetConnectionString("AzureStorage");
                _storageAccount = (string.IsNullOrEmpty(_connectionString) ? CloudStorageAccount.DevelopmentStorageAccount : CloudStorageAccount.Parse(_connectionString));
                _blobClient = _storageAccount.CreateCloudBlobClient();
                _blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
            }

            _isConnected = true;
        }

        private void _VerifyContainer(CloudBlobContainer container)
        {
            container.CreateIfNotExists(
                BlobContainerPublicAccessType.Off,
                new BlobRequestOptions() { ServerTimeout = new TimeSpan(1, 0, 0) });
        }

        private async Task _VerifyContainerAsync(CloudBlobContainer container)
        {
            await container.CreateIfNotExistsAsync(
                BlobContainerPublicAccessType.Off,
                new BlobRequestOptions() { ServerTimeout = new TimeSpan(1, 0, 0) },
                (OperationContext)null);
        }

        protected void DeleteFile(string path)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            _VerifyContainer(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            blob.DeleteIfExists();
        }

        protected async Task DeleteFileAsync(string path)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            await _VerifyContainerAsync(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            await blob.DeleteIfExistsAsync();
        }

        protected byte[] GetFile(string path)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            _VerifyContainer(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            byte[] contents = null;
            if (blob != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    blob.DownloadToStream(stream);
                    contents = stream.ToArray();
                }
            }
            return contents;
        }

        protected async Task<byte[]> GetFileAsync(string path)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            await _VerifyContainerAsync(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            byte[] contents = null;
            if (blob != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await blob.DownloadToStreamAsync(stream);
                    contents = stream.ToArray();
                }
            }
            return contents;
        }

        protected void SaveFile(string path, byte[] contents)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            _VerifyContainer(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            using (MemoryStream stream = new MemoryStream(contents))
            {
                blob.UploadFromStream(stream);
            }
        }

        protected async Task SaveFileAsync(string path, byte[] contents)
        {
            _Connect();

            CloudBlobContainer container = _blobClient.GetContainerReference(_containerName);
            await _VerifyContainerAsync(container);
            CloudBlockBlob blob = container.GetBlockBlobReference(path);
            using (MemoryStream stream = new MemoryStream(contents))
            {
                await blob.UploadFromStreamAsync(stream);
            }
        }

    }
}
