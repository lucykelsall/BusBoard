using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public class PostcodeValidation
    {
        [JsonProperty]
        public int Status { get; set; }
        [JsonProperty]
        public bool Result { get; set; }
    }
}
