using RWI.Common.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Cryptography
{
    public interface IEncryptionService
    {
        CryptoKeySet GenerateCryptoKeySet();
        string Encrypt(CryptoKeySet cryptoKeySet, string valueToEncrypt);
        Task<string> EncryptAsync(CryptoKeySet cryptoKeySet, string valueToEncrypt);
        string Decrypt(CryptoKeySet cryptoKeySet, string valueToDecrypt);
        Task<string> DecryptAsync(CryptoKeySet cryptoKeySet, string valueToDecrypt);
    }
}
