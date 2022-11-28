using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusBoard.Api
{
    public class BusStopResponse
    {

        [JsonProperty("stopPoints")]
        public List<BusStopPoints> StopPoints { get; set; }
    }
}