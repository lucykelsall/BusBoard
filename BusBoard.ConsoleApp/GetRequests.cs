using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Text.Json;
using System.CodeDom;

namespace BusBoard.ConsoleApp
{
    partial class Program
    {
        class GetRequests
        {
            // Function to send get request for postcode data for the requested postcode
            public static PostcodeResponse PostcodeDataGetRequest(string postcode)
            {
                var client = new RestClient("https://api.postcodes.io/");

                var request = new RestRequest("postcodes/" + postcode);

                var response = client.Execute<PostcodeResponse>(request);

                return response.Data;

            }

            // Function to send get request for bus stop data (using latitude and longitude from postcode data)
            public static StopPointResponse BusStopDataGetRequest(double latitude, double longitude)
            {
                var client = new RestClient("https://api.tfl.gov.uk/");

                var request = new RestRequest("StopPoint/?lat=" + latitude + "&lon=" + longitude + "&stopTypes=NaptanPublicBusCoachTram&radius=500&modes=bus");

                var response = client.Execute<StopPointResponse>(request);

                //var response = client.Execute(request);

                //var deserialise = JsonConvert.DeserializeObject<PostcodeResponse>(response.Content);

                //var resultList = JsonConvert.DeserializeObject<List<SomeObject>>(jsonstring);

                //Console.WriteLine(deserialise.Data);
                //Console.ReadLine();
                
                //return deserialise.Result;
                return response.Data;
            }



            // Function to send get request for bus arrival data for the requested stop code
            public static List<BusArrivalData> BusArrivalDataGetRequest(string stopCode)
            {
                var client = new RestClient("https://api.tfl.gov.uk/");

                var request = new RestRequest("StopPoint/" + stopCode + "/Arrivals");

                var response = client.Execute<List<BusArrivalData>>(request);

                return response.Data;
            }



            // Previous attempt at using async/await

            /*public static async Task<PostcodeResponse.PostcodeObject> PostcodeDataGetRequest(string postcode)
            {
                var client = new RestClient("https://api.postcodes.io/");
                client.UseNewtonsoftJson();

                var request = new RestRequest("postcodes/" + postcode, Method.Get, (Method)DataFormat.Json);

                var response = await client.ExecuteGetAsync(request);

                var deserialise = JsonConvert.DeserializeObject<PostcodeResponse>(response.Content);

                return deserialise.Result;

            }
            */

        }
    }
}
