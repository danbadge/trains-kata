using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Trains.Tests
{
	[TestFixture]
	public class TestScenarioRunnerTests
	{
		private TestScenarioRunner _testScenarioRunner;
		private MockConsole _mockConsole;

		[SetUp]
		public void Setup()
		{
			_mockConsole = new MockConsole();

			_testScenarioRunner = new TestScenarioRunner(_mockConsole);
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

	public class TestScenarioRunner
	{
		private readonly TextWriter _console;

		public TestScenarioRunner(TextWriter console)
		{
			_console = console;
		}

		public void Run()
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
			               "10.The number of different routes from C to C with a distance of less than 30.");


		}
	}
}