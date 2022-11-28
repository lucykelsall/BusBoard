using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace BusBoard.Api
{
    public class PostcodeValidation
    {
        [JsonProperty]
        public int Status { get; set; }
        [JsonProperty]
        public bool Result { get; set; }
    }
}
