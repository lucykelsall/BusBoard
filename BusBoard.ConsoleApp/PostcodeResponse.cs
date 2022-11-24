using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    partial class Program
    {
        // Because the Json response is nested inside a 'result' object, the PostcodeObject class is nested inside PostcodeResponse
        // [JsonProperty] directs it to look there? The class could also have some properties which are not taken from the Json
        
        public class PostcodeResponse
        {
            public class PostcodeObject
            {
                [JsonProperty]
                public string Postcode { get; set; }
                [JsonProperty]
                public string Country { get; set; }
                [JsonProperty]
                public double Longitude { get; set; }
                [JsonProperty]
                public double Latitude { get; set; }
                [JsonProperty]
                public string Region { get; set; }
                [JsonProperty]
                public string Incode { get; set; }
                [JsonProperty]
                public string Outcode { get; set; }
            }

            [JsonProperty] 
            public PostcodeObject Result { get; set; }
        }
    }
}
