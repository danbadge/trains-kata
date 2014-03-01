using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Trains
{
	[TestFixture]
	public class TrainsTest
	{
		[TestCase("A-B-C", 9)]
		[TestCase("A-D", 5)]
		[TestCase("A-D-C", 13)]
		public void Given_a_route_to_travel_then_calculate_the_total_distance(string route, int expectedDistance)
		{
			const string routingData = "AB5 BC4 DC8 AD5";

			var distanceCalculator = new DistanceCalculator(routingData);

			var actualDistance = distanceCalculator.Calculate(route);

			Assert.That(actualDistance, Is.EqualTo(expectedDistance));
		}
	}
}
