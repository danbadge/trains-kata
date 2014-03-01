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
		[TestCase("A-E-B-C-D", 22)]
		public void Given_a_route_to_travel_then_calculate_the_total_distance(string route, int expectedDistance)
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var distanceCalculator = new DistanceCalculator(routingData);

			var actualDistance = distanceCalculator.Calculate(route);

			Assert.That(actualDistance, Is.EqualTo(expectedDistance));
		}
	}
}
