﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RWI.Common.Models.Parsing;
using RWI.Common.Models.Web;
using RWI.Common.Services.Location;

namespace RWI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachedBasicNetOpAsyncController : ControllerBase
    {
        private readonly ICachedLocationDataService _cachedLocationDataService;

        public CachedBasicNetOpAsyncController(
            ICachedLocationDataService cachedLocationDataService
            )
        {
            _cachedLocationDataService = cachedLocationDataService;
        }

        [HttpGet]
        public async Task<ApiResponse<Zip>> Get()
        {
            Stopwatch stopwatch = null;
            ApiResponse<Zip> response = new ApiResponse<Zip>();

            try
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                response = new ApiResponse<Zip>();

                response.Result = await _cachedLocationDataService.GetRandomZipAsync();
                response.StatusCode = HttpStatusCode.OK;
                response.Timestamp = DateTime.UtcNow;

                stopwatch.Stop();
                response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            }
            catch (Exception ex)
            {
                response = ApiResponse<Zip>.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}