using System;
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
using RWI.WebApi.Models;

namespace RWI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplexNetOpController : ControllerBase
    {
        private readonly ILocationDataService _locationDataService;

        public ComplexNetOpController(
            ILocationDataService locationDataService
            )
        {
            _locationDataService = locationDataService;
        }

        [HttpGet]
        public ApiResponse<CityZipModel> Get()
        {
            Stopwatch stopwatch = null;
            ApiResponse<CityZipModel> response = new ApiResponse<CityZipModel>();

            try
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
                response = new ApiResponse<CityZipModel>();

                City city = _locationDataService.GetRandomCity();
                Zip zip = _locationDataService.GetClosestZip(city);

                response.Result = CityZipModel.Build(city, zip);
                response.StatusCode = HttpStatusCode.OK;
                response.Timestamp = DateTime.UtcNow;

                stopwatch.Stop();
                response.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            }
            catch (Exception ex)
            {
                response = ApiResponse<CityZipModel>.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}