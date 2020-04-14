using RWI.Common.Models.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Cryptography
{
    public class AesManagedEncryptionService : IEncryptionService
    {
        public string Decrypt(CryptoKeySet cryptoKeySet, string valueToDecrypt)
        {
            string plaintext = null;

            using (AesManaged algorithm = new AesManaged())
            {
                algorithm.Key = Convert.FromBase64String(cryptoKeySet.Key);
                algorithm.IV = Convert.FromBase64String(cryptoKeySet.IV);
                byte[] cipher = Convert.FromBase64String(valueToDecrypt);

                ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream stream = new MemoryStream(cipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            plaintext = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public async Task<string> DecryptAsync(CryptoKeySet cryptoKeySet, string valueToDecrypt)
        {
            string plaintext = null;

            using (AesManaged algorithm = new AesManaged())
            {
                algorithm.Key = Convert.FromBase64String(cryptoKeySet.Key);
                algorithm.IV = Convert.FromBase64String(cryptoKeySet.IV);
                byte[] cipher = Convert.FromBase64String(valueToDecrypt);

                ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream stream = new MemoryStream(cipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            plaintext = await streamReader.ReadToEndAsync();
                        }
                    }
                }
            }

            return plaintext;
        }

        public string Encrypt(CryptoKeySet cryptoKeySet, string valueToEncrypt)
        {
            byte[] encrypted;
            using (AesManaged algorithm = new AesManaged())
            {
                algorithm.Key = Convert.FromBase64String(cryptoKeySet.Key);
                algorithm.IV = Convert.FromBase64String(cryptoKeySet.IV);

                ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(valueToEncrypt);
                        }
                        encrypted = stream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public async Task<string> EncryptAsync(CryptoKeySet cryptoKeySet, string valueToEncrypt)
        {
            byte[] encrypted;
            using (AesManaged algorithm = new AesManaged())
            {
                algorithm.Key = Convert.FromBase64String(cryptoKeySet.Key);
                algorithm.IV = Convert.FromBase64String(cryptoKeySet.IV);

                ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            await streamWriter.WriteAsync(valueToEncrypt);
                        }
                        encrypted = stream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public CryptoKeySet GenerateCryptoKeySet()
        {
            CryptoKeySet cryptoKeySet = new CryptoKeySet();

            using (AesManaged algorithm = new AesManaged())
            {
                algorithm.GenerateKey();
                algorithm.GenerateIV();

                cryptoKeySet.Key = Convert.ToBase64String(algorithm.Key);
                cryptoKeySet.IV = Convert.ToBase64String(algorithm.IV);
            }

            return cryptoKeySet;
        }
    }
}
