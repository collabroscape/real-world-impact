using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Storage
{
    public interface IAssetStorageService
    {
        byte[] GetAssetFile(string path);
        Task<byte[]> GetAssetFileAsync(string path);
    }
}
