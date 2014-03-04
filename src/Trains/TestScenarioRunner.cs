using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Trains.Domain;

namespace Trains
{
	public class TestScenarioRunner
	{
		private readonly TextWriter _console;
		private readonly ICalculateDistancesOfSpecificRoutes _specificRouteDistanceCalculator;
		private readonly IFindRoutes _routeFinder;

		public TestScenarioRunner(TextWriter console, ICalculateDistancesOfSpecificRoutes specificRouteDistanceCalculator, IFindRoutes routeFinder)
		{
			_console = console;
			_specificRouteDistanceCalculator = specificRouteDistanceCalculator;
			_routeFinder = routeFinder;
		}

		public void Run()
		{
			OutputTestScenarioDescriptions();

			var scenarios = new List<Func<int>>
				{
					() => _specificRouteDistanceCalculator.SumDistanceOf("A-B-C"),
					() => _specificRouteDistanceCalculator.SumDistanceOf("A-D"),
					() => _specificRouteDistanceCalculator.SumDistanceOf("A-D-C"),
					() => _specificRouteDistanceCalculator.SumDistanceOf("A-E-B-C-D"),
					() => _specificRouteDistanceCalculator.SumDistanceOf("A-E-D"),
					() => _routeFinder.GetRoutes("C", "C").WithMaxStops(3).Count,
					() => _routeFinder.GetRoutes("A", "C").WithExactStops(4).Count,
					() => _routeFinder.GetShortestRoute("A", "C").TotalDistance,
					() => _routeFinder.GetShortestRoute("B", "B").TotalDistance,
					() => _routeFinder.GetRoutes("C", "C").WithADistanceLessThan(30).Count
				};

			RunScenarios(scenarios);
		}

		private void RunScenarios(IList<Func<int>> scenarios)
		{
			for (var i = 1; i <= scenarios.Count; i++)
			{
				try
				{
					var result = scenarios[i - 1]();
					OutputScenarioResult(i, result);
				}
				catch (RouteNotFoundException exception)
				{
					OutputScenarioResult(i, exception.Message);
				}
			}
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