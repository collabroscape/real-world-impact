using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWI.Common.Models.Web;
using RWI.Common.Services.Cryptography;
using RWI.WebApi.Models;


namespace RWI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptographyAsyncController : ControllerBase
    {
        private readonly IEncryptionService _encryptionService;

        public CryptographyAsyncController(
            IEncryptionService encryptionService
            )
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        public async Task<ApiResponse<CryptographyResponseModel>> Get()
        {
            Stopwatch stopwatch = null;
            ApiResponse<CryptographyResponseModel> response = new ApiResponse<CryptographyResponseModel>();

            try
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                response = new ApiResponse<CryptographyResponseModel>();

                var cryptoSet = _encryptionService.GenerateCryptoKeySet();
                string encrypted = await _encryptionService.EncryptAsync(cryptoSet, Guid.NewGuid().ToString());
                string decrypted = await _encryptionService.DecryptAsync(cryptoSet, encrypted);

                response.Result = new CryptographyResponseModel()
                {
                    Key = cryptoSet.Key,
                    IV = cryptoSet.IV,
                    Encrypted = encrypted,
                    Decrypted = decrypted
                };
                response.StatusCode = HttpStatusCode.OK;
                response.Timestamp = DateTime.UtcNow;

                stopwatch.Stop();
                response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            }
            catch (Exception ex)
            {
                response = ApiResponse<CryptographyResponseModel>.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}