using NUnit.Framework;

namespace Trains.Tests
{
	[TestFixture]
	public class DistanceCalculatorTests
	{
		private DistanceCalculator _distanceCalculator;

		[TestFixtureSetUp]
		public void Setup()
		{
			var routingData = TestData.ConnectedStations;

			_distanceCalculator = new DistanceCalculator(routingData);
		}

		[TestCase("A-B-C", 9)]
		[TestCase("A-D", 5)]
		[TestCase("A-D-C", 13)]
		[TestCase("A-E-B-C-D", 22)]
		public void Given_a_route_to_travel_then_calculate_the_total_distance(string route, int expectedDistance)
		{
			var actualDistance = _distanceCalculator.Calculate(route);

			Assert.That(actualDistance, Is.EqualTo(expectedDistance));
		}

		[Test]
		public void Given_a_route_which_cannot_be_completed_then_throw_exception()
		{
			var exception = Assert.Throws<RouteNotFoundException>(() => _distanceCalculator.Calculate("A-E-D"));

			Assert.That(exception.Message, Is.EqualTo("NO SUCH ROUTE"));
		}
	}
}
