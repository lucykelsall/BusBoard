using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public class BusStopResponse
    {

        [JsonProperty("stopPoints")]
        public List<BusStopPoints> StopPoints { get; set; }
    }
}