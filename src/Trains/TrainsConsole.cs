using System;
using System.Collections.Generic;
using Trains.Domain;

namespace Trains
{
	public class TrainsConsole
	{
		public static void Main(string[] args)
		{
			try
			{
				var fileLocation = args.Length > 0 ? args[0] : "routing-data.txt";

				var connectedStations = LoadConnectedStationsFrom(fileLocation);
				var testScenarioRunner = CreateTestScenarioRunner(connectedStations);

				testScenarioRunner.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred: {0} - {1}", ex.GetType(), ex.Message);
			}

			Console.ReadLine();
		}

		private static List<ConnectedStations> LoadConnectedStationsFrom(string fileLocation)
		{
			var routingDataStore = new RoutingDataStore();
			routingDataStore.LoadFrom(fileLocation);
			return routingDataStore.ConnectedStations;
		}

		private static TestScenarioRunner CreateTestScenarioRunner(List<ConnectedStations> connectedStations)
		{
			var distanceCalculator = new DistanceCalculator(connectedStations);
			var routesFinder = new RouteFinder(connectedStations);
			var testScenarioRunner = new TestScenarioRunner(Console.Out, distanceCalculator, routesFinder);
			return testScenarioRunner;
		}
	}
}