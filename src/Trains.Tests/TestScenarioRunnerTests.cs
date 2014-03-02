﻿using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace Trains.Tests
{
	[TestFixture]
	public class TestScenarioRunnerTests
	{
		private TestScenarioRunner _testScenarioRunner;
		private MockConsole _mockConsole;
		private ICalculateDistances _distanceCalculator;

		[SetUp]
		public void Setup()
		{
			_mockConsole = new MockConsole();

			_distanceCalculator = MockRepository.GenerateStub<ICalculateDistances>();

			_testScenarioRunner = new TestScenarioRunner(_mockConsole, _distanceCalculator);
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
		public void Should_calculate_the_distance_between_routes_5_times()
		{
			_testScenarioRunner.Run();

			_distanceCalculator.AssertWasCalled(d => d.Calculate(Arg<String>.Is.Anything), d => d.Repeat.Times(5));
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
		public void Should_output_distance_and_test_number()
		{
			_distanceCalculator.Stub(c => c.Calculate(Arg<string>.Is.Anything))
							   .Return(43);

			_testScenarioRunner.Run();

			Assert.That(_mockConsole.GetOutput(), Is.StringContaining("Output #1: 43"));
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