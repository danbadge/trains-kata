using System;
using System.IO;
using NUnit.Framework;

namespace Trains.Tests
{
	[TestFixture]
	public class TrainsConsoleTests
	{
		[Test]
		public void Should_output_error_messages_and_not_throw_exceptions_if_data_does_not_exist()
		{
			using (var consoleOutCatcher = new StringWriter())
			{
				Console.SetOut(consoleOutCatcher);

				Assert.DoesNotThrow(() => TrainsConsole.Main(new[] { "no-data.txt" }));

				const string expectedErrorMessage = "An error occurred: System.IO.FileNotFoundException - Routing data file could not be found";
				Assert.That(consoleOutCatcher.ToString(), Is.StringStarting(expectedErrorMessage));
			}
		}

		[Test]
		public void Should_run_successfully_if_data_does_not_exist()
		{
			using (var consoleOutCatcher = new StringWriter())
			{
				Console.SetOut(consoleOutCatcher);

				TrainsConsole.Main(new string[]{});

				Assert.That(consoleOutCatcher.ToString(), Is.StringStarting("Test Input"));
				Assert.That(consoleOutCatcher.ToString(), Is.Not.StringContaining("An error occurred:"));
			}
		}

		[TearDown]
		public void Teardown()
		{
			var standardOutput = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
			Console.SetOut(standardOutput);
		}
	}
}