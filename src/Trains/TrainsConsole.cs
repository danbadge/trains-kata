using System;
using Trains.Domain;

namespace Trains
{
	public class TrainsConsole
	{
		public static void Main(string[] args)
		{
			try
			{
				var fileLocation = args[0] ?? "routing-data.txt";

				var routingDataStore = new RoutingDataStore();
				routingDataStore.LoadFrom(fileLocation);

				var connectedStations = routingDataStore.ConnectedStations;
				var distanceCalculator = new DistanceCalculator(connectedStations);
				var routesFinder = new RouteFinder(connectedStations);

				var testScenarioRunner = new TestScenarioRunner(Console.Out, distanceCalculator, routesFinder);

				testScenarioRunner.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred: {0} - {1}", ex.GetType(), ex.Message);
			}

			Console.ReadLine();
		}
	}
}