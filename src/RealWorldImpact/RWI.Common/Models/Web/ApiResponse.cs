using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace RWI.Common.Models.Web
{
    [DataContract(Name = "response")]
    public class ApiResponse<T> where T : class
    {
        /// <summary>
        /// Return object
        /// </summary>
        [DataMember]
        [Display(Name = "result")]
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
        /// <summary>
        /// Message regarding status of operation
        /// </summary>
        [DataMember]
        [Display(Name = "error_message")]
        [JsonProperty(PropertyName = "error_message")]
        public string ErrorMessage { get; set; }
        /// <summary>
        /// HTTP response status
        /// </summary>
        [DataMember]
        [Display(Name = "status")]
        [JsonProperty(PropertyName = "status")]
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Operation timestamp
        /// </summary>
        [DataMember]
        [Display(Name = "timestamp")]
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Elapsed time
        /// </summary>
        [DataMember]
        [Display(Name = "elapsed")]
        [JsonProperty(PropertyName = "elapsed")]
        public TimeSpan Elapsed { get; set; }

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
