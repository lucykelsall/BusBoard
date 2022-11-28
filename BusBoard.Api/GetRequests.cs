using RestSharp;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Text.Json;
using System.CodeDom;

namespace BusBoard.Api
{
        public class GetRequests
        {

        // Ask about get request errors (try / catch? But method then complains does not return the correct value)



        // Method to execute get requests (private to this class)
        private static T GetRequest<T>(string domainUrl, string resourceUrl)
        {
            var client = new RestClient(domainUrl);
            var request = new RestRequest(resourceUrl);
            
            var response = client.Execute<T>(request);
            return response.Data;
        }

        // Method to send get request to validate the requested postcode
        public static PostcodeValidation PostcodeValidationGetRequest(string postcode)
        {
            string postcodeDomainUrl = "https://api.postcodes.io/";
            string postcodeResourceUrl = $"postcodes/{postcode}/validate";

            return GetRequest<PostcodeValidation>(postcodeDomainUrl, postcodeResourceUrl);

        }
        
        // Method to send get request for postcode data for the requested postcode
        public static PostcodeResponse PostcodeDataGetRequest(string postcode)
        {
            string postcodeDomainUrl = "https://api.postcodes.io/";
            string postcodeResourceUrl = $"postcodes/{postcode}";

            return GetRequest<PostcodeResponse>(postcodeDomainUrl, postcodeResourceUrl);

        }

        // Method to send get request for bus stop data (using latitude and longitude from postcode data)
        public static BusStopResponse BusStopDataGetRequest(double latitude, double longitude)
        {
            string busStopDomainUrl = "https://api.tfl.gov.uk/";
            string busStopResourceUrl = $"StopPoint/?lat={latitude}&lon={longitude}&stopTypes=NaptanPublicBusCoachTram&radius=500&modes=bus";

            return GetRequest<BusStopResponse>(busStopDomainUrl, busStopResourceUrl); 
        }

        // Method to send get request for bus arrival data for the requested stop code
        public static List<BusArrivalData> BusArrivalDataGetRequest(string stopCode)
        {
            string busArrivalDomainUrl = "https://api.tfl.gov.uk/";
            string busArrivalResourceUrl = $"StopPoint/{stopCode}/Arrivals";

            return GetRequest<List<BusArrivalData>>(busArrivalDomainUrl, busArrivalResourceUrl);
        }
        }   
}
