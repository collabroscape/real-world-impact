using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace RWI.Common.Models.Web
{
    public class ApiResponse<T> where T : class
    {
        /// <summary>
        /// Return object
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// Message regarding status of operation
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// HTTP response status
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Operation timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Elapsed time
        /// </summary>
        public long ElapsedMilliseconds { get; set; }

        public static ApiResponse<T> CreateResponse(HttpStatusCode statusCode, string errorMessage = "")
        {
            return new ApiResponse<T>()
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage,
                Timestamp = DateTime.UtcNow,
            };
        }

    }
}
