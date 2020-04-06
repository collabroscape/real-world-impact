using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RWI.WebApi.Models
{
    [DataContract(Name = "cryptography")]
    public class CryptographyResponseModel
    {
        [DataMember]
        [Display(Name = "key")]
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [DataMember]
        [Display(Name = "iv")]
        [JsonProperty(PropertyName = "iv")]
        public string IV { get; set; }
        [DataMember]
        [Display(Name = "encrypted")]
        [JsonProperty(PropertyName = "encrypted")]
        public string Encrypted { get; set; }
        [DataMember]
        [Display(Name = "decrypted")]
        [JsonProperty(PropertyName = "decrypted")]
        public string Decrypted { get; set; }
    }
}
