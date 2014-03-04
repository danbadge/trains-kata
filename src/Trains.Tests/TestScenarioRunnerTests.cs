using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Trains.Domain;

namespace Trains.Tests
{
	[TestFixture]
	public class TestScenarioRunnerTests
	{
		private TestScenarioRunner _testScenarioRunner;
		private MockConsole _mockConsole;
		private ICalculateDistances _distanceCalculator;
		private IFindRoutes _routesFinder;

		[SetUp]
		public void Setup()
		{
			_mockConsole = new MockConsole();

			_distanceCalculator = MockRepository.GenerateStub<ICalculateDistances>();
			_routesFinder = MockRepository.GenerateStub<IFindRoutes>();

			_routesFinder.Stub(r => r.GetRoutes(null, null)).IgnoreArguments().Return(new List<Route>());
			_routesFinder.Stub(r => r.GetShortestRoute(null, null)).IgnoreArguments().Return(new Route());

			_testScenarioRunner = new TestScenarioRunner(_mockConsole, _distanceCalculator, _routesFinder);
		}

		[Test]
		public void Should_return_summary_of_test_input()
		{
			_testScenarioRunner.Run();

			const string expectedTestInput = "Test Input\n" +
			                                 "1. The distance of the route A-B-C.\n" +
			                                 "2. The distance of the route A-D.\n" +
			                                 "3. The distance of the route A-D-C.\n" +
			                                 "4. The distance of the route A-E-B-C-D.\n" +
			                                 "5. The distance of the route A-E-D.\n" +
			                                 "6. The number of trips starting at C and ending at C with a maximum of 3 stops.\n" +
			                                 "7. The number of trips starting at A and ending at C with exactly 4 stops.\n" +
			                                 "8. The length of the shortest route (in terms of distance to travel) from A to C.\n" +
			                                 "9. The length of the shortest route (in terms of distance to travel) from B to B.\n" +
			                                 "10.The number of different routes from C to C with a distance of less than 30.";

			Assert.That(_mockConsole.GetOutput(), Is.StringStarting(expectedTestInput));
		}

		[Test]
		public void Should_output_error_message_if_distance_cannot_be_calculated()
		{
			_distanceCalculator.Stub(c => c.Calculate(Arg<string>.Is.Anything))
			                   .Throw(new RouteNotFoundException());

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("NO SUCH ROUTE"));
		}

		[Test]
		public void Should_output_distance_and_test_number_for_scenarios_1_to_5()
		{
			_distanceCalculator.Stub(c => c.Calculate(Arg<string>.Is.Anything))
							   .Return(43);

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #1: 43"));
			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #2: 43"));
			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #3: 43"));
			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #4: 43"));
			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #5: 43"));
		}

		[Test]
		public void Should_output_number_of_available_routes_with_no_more_than_3_stops_for_scenario_6()
		{
			var availableRoute = new List<Route>
				{
					new Route { new ConnectedStations("A", "B", 5) }
				};

			_routesFinder.Stub(r => r.GetRoutes("C", "C")).Return(availableRoute).Repeat.Any();

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #6: 1"));
		}

		[Test]
		public void Should_output_number_of_available_routes_with_exactly_4_stops_for_scenario_7()
		{
			var availableRoute = new List<Route>
				{
					new Route
						{
							new ConnectedStations("A", "B", 5),
							new ConnectedStations("A", "B", 5),
							new ConnectedStations("A", "B", 5),
							new ConnectedStations("A", "B", 5),
						}
				};

			_routesFinder.Stub(r => r.GetRoutes("A", "C")).Return(availableRoute).Repeat.Any();

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #7: 1"));
		}

		[Test]
		public void Should_output_the_route_with_the_shortest_distance_for_scenarios_8_and_9()
		{
			var route = new Route
				{
					new ConnectedStations("B", "B", 10),
					new ConnectedStations("B", "B", 4),
				};

			_routesFinder.Stub(r => r.GetShortestRoute(null, null)).IgnoreArguments().Return(route).Repeat.Any();

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #8: 14"));
			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #9: 14"));
		}

		[Test]
		public void Should_output_the_number_of_available_routes_with_a_distance_under_30_for_scenario_10()
		{
			var availableRoute = new List<Route>
				{
					new Route { new ConnectedStations("A", "B", 5) },
					new Route { new ConnectedStations("B", "B", 40) }
				};

			_routesFinder.Stub(r => r.GetRoutes("C", "C")).Return(availableRoute).Repeat.Any();

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #10: 1"));
		}
	}
	
	public class MockConsole : TextWriter
	{
		private string _output;

		public override void WriteLine(string message)
		{
			_output += (!String.IsNullOrEmpty(_output) ? Environment.NewLine : "") + message;
		}

		public override void Write(string message)
		{
			_output += message;
		}

		public override Encoding Encoding
		{
			get { throw new NotImplementedException(); }
		}

		public string GetOutput()
		{
			return _output;
		}
	}
}