using System.Collections.Generic;
using System.IO;

namespace Trains
{
	public class TestScenarioRunner
	{
		private readonly TextWriter _console;
		private readonly ICalculateDistances _distanceCalculator;

		public TestScenarioRunner(TextWriter console, ICalculateDistances distanceCalculator)
		{
			_console = console;
			_distanceCalculator = distanceCalculator;
		}

		public void Run()
		{
			OutputTestScenarioDescriptions();

			const int scenarioNumber = 1;
			
			RunThroughDistanceScenarios(scenarioNumber);


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

		private void RunThroughDistanceScenarios(int scenarioNumber)
		{
			var distanceScenarios = new List<string>
				{
					"A-B-C",
					"A-D",
					"A-D-C",
					"A-E-B-C-D",
					"A-E-D"
				};

			foreach (var distanceScenario in distanceScenarios)
			{
				try
				{
					var distance = _distanceCalculator.Calculate(distanceScenario);
					OutputScenarioResult(scenarioNumber, distance.ToString());
				}
				catch (RouteNotFoundException exception)
				{
					OutputScenarioResult(scenarioNumber, exception.Message);
				}
				scenarioNumber++;
			}
		}

		private void OutputScenarioResult(int scenarioNumber, string result)
		{
			_console.WriteLine("Output #" + scenarioNumber + ": " + result);
		}
	}
}