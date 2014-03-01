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
		[Test]
		public void Should_calculate_the_distance_from_A_to_C_via_B()
		{
			const string routingData = "AB5 BC4";

			var distanceCalculator = new DistanceCalculator(routingData);

			var distance = distanceCalculator.Calculate("A-B-C");

			Assert.That(distance, Is.EqualTo(9));
		}

		[Test]
		public void Should_calculate_the_distance_from_A_to_D()
		{
			const string routingData = "AD5";

			var distanceCalculator = new DistanceCalculator(routingData);

			var distance = distanceCalculator.Calculate("A-D");

			Assert.That(distance, Is.EqualTo(5));
		}
    }
}
