using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    public class BusStopPoints
    {
        [JsonProperty]
        public string CommonName { get; set; }
        [JsonProperty]
        public double Distance { get; set; }
        [JsonProperty]
        public string IcsCode { get; set; }
        [JsonProperty]
        public string Id { get; set; }
        [JsonProperty]
        public string Indicator { get; set; }
        [JsonProperty]
        public double Lat { get; set; }
        [JsonProperty]
        public double Lon { get; set; }
        [JsonProperty]
        public List<string> Modes { get; set; }
        [JsonProperty]
        public string NaptanId { get; set; }
        [JsonProperty]
        public string StationNaptan { get; set; }
        [JsonProperty]
        public bool Status { get; set; }
        [JsonProperty]
        public string StopLetter { get; set; }
        [JsonProperty]
        public string StopType { get; set; }
    }
}