using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Storage
{
    public class AssetStorageService : BaseAzureBlobStorageService, IAssetStorageService
    {
        private readonly IConfiguration _configuration;

        public AssetStorageService(IConfiguration configuration) : base(configuration, "assets")
        {
            _configuration = configuration;
        }

        public byte[] GetAssetFile(string path)
        {
            return base.GetFile(path);
        }

        public async Task<byte[]> GetAssetFileAsync(string path)
        {
            return await base.GetFileAsync(path);
        }
    }
}
