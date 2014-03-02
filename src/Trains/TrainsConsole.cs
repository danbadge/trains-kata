using System;

namespace Trains
{
	internal class TrainsConsole
	{
		private static void Main(string[] args)
		{
			var routingDataStore = new RoutingDataStore();
			routingDataStore.Load();

			var connectedStations = routingDataStore.ConnectedStations;
			var distanceCalculator = new DistanceCalculator(connectedStations);
			var routesFinder = new RoutesFinder(connectedStations);

			var testScenarioRunner = new TestScenarioRunner(Console.Out, distanceCalculator, routesFinder);

			testScenarioRunner.Run();

			Console.ReadLine();
		}
	}
}