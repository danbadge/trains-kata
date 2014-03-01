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

			var distance = CalculateDistance(routingData, "A-B-C");

			Assert.That(distance, Is.EqualTo(9));
		}

		private int CalculateDistance(string routingData, string route)
		{
			var distances = DistanceBetweenStations(routingData);

			var stations = route.Split(Convert.ToChar("-"));

			var distance = 0;
			for (var i = 1; i < stations.Count(); i++)
			{
				var immediateStations = stations[i - 1] + stations[i];
				distance += distances[immediateStations];
			}

			return distance;
		}

		private static Dictionary<string, int> DistanceBetweenStations(string routingData)
		{
			var distances = new Dictionary<string, int>();
			var nodes = routingData.Split(Convert.ToChar(" "));
			foreach (var node in nodes)
			{
				var stations = node.Substring(0, 2);
				var distance = Convert.ToInt32(node.Substring(2, 1));
				distances.Add(stations, distance);
			}
			return distances;
		}
    }
}
