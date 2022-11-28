using Newtonsoft.Json;

namespace BusBoard.Api
{
    public class PostcodeData
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

}
