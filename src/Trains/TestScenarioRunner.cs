using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Trains.Domain;

namespace Trains
{
	public class TestScenarioRunner
	{
		private readonly TextWriter _console;
		private readonly ICalculateDistances _distanceCalculator;
		private readonly IFindRoutes _routeFinder;

		public TestScenarioRunner(TextWriter console, ICalculateDistances distanceCalculator, IFindRoutes routeFinder)
		{
			_console = console;
			_distanceCalculator = distanceCalculator;
			_routeFinder = routeFinder;
		}

		public void Run()
		{
			OutputTestScenarioDescriptions();

			RunDistanceScenarios();

			RunRouteFindingScenarios();
		}

		private void OutputTestScenarioDescriptions()
		{
			_console.Write("Test Input\n" +
			               "1. The distance of the route A-B-C.\n" +
			               "2. The distance of the route A-D.\n" +
			               "3. The distance of the route A-D-C.\n" +
			               "4. The distance of the route A-E-B-C-D.\n" +
			               "5. The distance of the route A-E-D.\n" +
			               "6. The number of trips starting at C and ending at C with a maximum of 3 stops.\n" +
			               "7. The number of trips starting at A and ending at C with exactly 4 stops.\n" +
			               "8. The length of the shortest route (in terms of distance to travel) from A to C.\n" +
			               "9. The length of the shortest route (in terms of distance to travel) from B to B.\n" +
			               "10.The number of different routes from C to C with a distance of less than 30.\n\n");
		}

		private void RunDistanceScenarios()
		{
			var distanceScenarios = new List<string>
				{
					"A-B-C",
					"A-D",
					"A-D-C",
					"A-E-B-C-D",
					"A-E-D"
				};

			var scenarioNumber = 1;
			foreach (var distanceScenario in distanceScenarios)
			{
				try
				{
					var distance = _distanceCalculator.Calculate(distanceScenario);
					OutputScenarioResult(scenarioNumber, distance);
				}
				catch (RouteNotFoundException exception)
				{
					OutputScenarioResult(scenarioNumber, exception.Message);
				}
				scenarioNumber++;
			}
		}

		private void RunRouteFindingScenarios()
		{
			var routesWithMaxStops = _routeFinder.GetRoutes("C", "C").WithMaxStops(3).Count;
			OutputScenarioResult(6, routesWithMaxStops);

			var routesWithExactStops = _routeFinder.GetRoutes("A", "C").WithExactStops(4).Count;
			OutputScenarioResult(7, routesWithExactStops);

			var shortestDistance8 = _routeFinder.GetShortestRoute("A", "C").TotalDistance;
			OutputScenarioResult(8, shortestDistance8);

			var shortestDistance9 = _routeFinder.GetShortestRoute("B", "B").TotalDistance;
			OutputScenarioResult(9, shortestDistance9);

			var numberOfAvailableRoutes = _routeFinder.GetRoutes("C", "C").WithADistanceLessThan(30).Count;
			OutputScenarioResult(10, numberOfAvailableRoutes);
		}

		private void OutputScenarioResult(int scenarioNumber, int results)
		{
			OutputScenarioResult(scenarioNumber, results.ToString(CultureInfo.InvariantCulture));
		}

		private void OutputScenarioResult(int scenarioNumber, string result)
		{
			_console.WriteLine("Output #" + scenarioNumber + ": " + result);
		}
	}
}