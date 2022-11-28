using RestSharp;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcome message
            Console.WriteLine("Welcome. Here you can supply a postcode and see the next buses at the two closest bus stops.");

            // Ask the user for a postcode and store the response in a variable
            Console.Write("Please enter a postcode: ");
            string userPostcode = Console.ReadLine();
            Console.WriteLine();

            // Validate the postcode and return an error message if doesn't exist
            var postcodeValidation = GetRequests.PostcodeValidationGetRequest(userPostcode);

            if (postcodeValidation.Result == false)
            {
                Console.WriteLine("Sorry, that is not a valid postcode. The program will now exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            // Get an object containing postcode data for that postcode
            var postcodeData = GetRequests.PostcodeDataGetRequest(userPostcode).Result;

            // Get bus stop data for the latitude and longitude of that postcode (within 500m radius)
            var busStopResponse = GetRequests.BusStopDataGetRequest(postcodeData.Latitude, postcodeData.Longitude);
            var busStops = busStopResponse.StopPoints;

            // Check whether there is any bus stop data in the list - if not it's probably a postcode not covered by TfL
            if (busStops.Count == 0)
            {
                Console.WriteLine("Sorry, that postcode is outside of London. The program will now exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            // Ensure list of bus stop points sorted by proximity and choose the two nearest
            var busStopsOrderedByProximity = (from entry in busStops orderby entry.Distance ascending select entry).ToList();

            var firstNearestBusStop = busStopsOrderedByProximity[0];
            var secondNearestBusStop = busStopsOrderedByProximity[1];

            // For the first and second nearest bus stops, use the ID of the stop to make a get request for its arrival data
            var firstNearestBusStopArrivalData = GetRequests.BusArrivalDataGetRequest(firstNearestBusStop.Id);
            var secondNearestBusStopArrivalData = GetRequests.BusArrivalDataGetRequest(secondNearestBusStop.Id);

            // Create variables to check whether these bus stops have returned arrival times or are empty (not in use)
            var firstNearestStopInUse = true;
            var secondNearestStopInUse = true;

            if (firstNearestBusStopArrivalData.Count == 0)
            {
                firstNearestStopInUse = false;
            }
            if (secondNearestBusStopArrivalData.Count == 0)
            {
                secondNearestStopInUse = false;
            }

            // Ensure the lists of arrival data are sorted by next arrival time
            var sortedFirstNearestBusStopArrivalData = firstNearestBusStopArrivalData.OrderBy(bus => bus.TimeToStation).ToList();
            var sortedSecondNearestBusStopArrivalData = secondNearestBusStopArrivalData.OrderBy(bus => bus.TimeToStation).ToList();

            // Print out the timings and line numbers of the next five buses due at both stops
            Console.WriteLine($"The closest bus stop to you is named {firstNearestBusStop.CommonName} (ID number {firstNearestBusStop.Id}).");
            if (firstNearestStopInUse == true && firstNearestBusStopArrivalData.Count >= 5)
            {
                Console.WriteLine("The next five buses to arrive there will be:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("{0}. The {1} to {2} is due in {3} minutes",
                        i + 1,
                        sortedFirstNearestBusStopArrivalData[i].LineId,
                        sortedFirstNearestBusStopArrivalData[i].DestinationName,
                        sortedFirstNearestBusStopArrivalData[i].TimeToStation / 60);
                }
            }
            else if (firstNearestStopInUse == true && firstNearestBusStopArrivalData.Count < 5)
            {
                for (int i = 0; i < (firstNearestBusStopArrivalData.Count - 1); i++)
                {
                    Console.WriteLine("{0}. The {1} to {2} is due in {3} minutes",
                        i + 1,
                        sortedFirstNearestBusStopArrivalData[i].LineId,
                        sortedFirstNearestBusStopArrivalData[i].DestinationName,
                        sortedFirstNearestBusStopArrivalData[i].TimeToStation / 60);
                }
                Console.WriteLine("There are fewer than five buses currently scheduled from this stop.");
            }
            else
            {
                Console.WriteLine("This stop seems to be out of use at present.");
            }
            Console.WriteLine();

            Console.WriteLine($"The second-closest bus stop to you is named {secondNearestBusStop.CommonName} (ID number {secondNearestBusStop.Id}).");
            if (secondNearestStopInUse == true && secondNearestBusStopArrivalData.Count >= 5)
            {
                Console.WriteLine("The next five buses to arrive there will be:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("{0}. The {1} to {2} is due in {3} minutes",
                        i + 1,
                        sortedSecondNearestBusStopArrivalData[i].LineId,
                        sortedSecondNearestBusStopArrivalData[i].DestinationName,
                        sortedSecondNearestBusStopArrivalData[i].TimeToStation / 60);
                }
            }
            else
            {
                Console.WriteLine("This stop seems to be out of use at present.");
            }
            
            Console.ReadLine();
        }
    }
}
