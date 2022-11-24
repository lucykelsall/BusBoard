using RestSharp;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

namespace BusBoard.ConsoleApp
{
    partial class Program
    {
        static void Main(string[] args)
        {
            /*
            // Ask the user for a stop code (NB stop code for Softwire: 490008660N)
            Console.Write("Please enter a stop code: ");
            string userStopCode = Console.ReadLine();
            
            // Get a list of objects containing bus data for that stop code
            List<BusArrivalData> busArrivalData = GetRequests.BusArrivalDataGetRequest(userStopCode);

            // Sort the list by next arrival time
            var sortedBusArrivalData = busArrivalData.OrderBy(bus => bus.TimeToStation).ToList();

            // Print out the timings and line numbers of the next five buses due
            Console.WriteLine("The next five buses to arrive will be:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("{0}. The {1} to {2} will be arriving in {3} seconds", 
                    i+1, sortedBusArrivalData[i].LineId, sortedBusArrivalData[i].DestinationName, sortedBusArrivalData[i].TimeToStation);
            }

            Console.ReadLine();
            */


            // Ask the user for a postcode
            Console.Write("Please enter a postcode: ");
            string userPostcode = Console.ReadLine();

            // Get an object containing postcode data for that postcode
            var postcodeData = GetRequests.PostcodeDataGetRequest(userPostcode).Result;
            
            Console.WriteLine(postcodeData.Postcode);
            Console.WriteLine(postcodeData.Longitude);
            Console.WriteLine(postcodeData.Latitude);

            var latitude = postcodeData.Latitude;
            var longitude = postcodeData.Longitude;

            var busStopData = GetRequests.BusStopDataGetRequest(latitude, longitude);

            Console.WriteLine(busStopData.StopPoints[0].Lat);
            Console.WriteLine(busStopData.StopPoints[0].Lon);


            Console.ReadLine();

            // The user can supply a postcode and see the next buses at the two nearest bus stops.


            // xhttps://api.tfl.gov.uk/StopPoint/?lat={lat}&lon={lon}&stopTypes={stopTypes}[&radius][&useStopPointHierarchy][&modes][&categories][&returnLines]
        }
    }
}
