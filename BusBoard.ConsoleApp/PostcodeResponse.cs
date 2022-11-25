using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace BusBoard.ConsoleApp
{
    // Because the Json response is nested inside a 'result' object, the PostcodeObject class is nested inside PostcodeResponse
    // [JsonProperty] directs it to look there? The class could also have some properties which are not taken from the Json
    public class PostcodeResponse
        {
            [JsonProperty ("result")] 
            public PostcodeData Result { get; set; }
        }
}
