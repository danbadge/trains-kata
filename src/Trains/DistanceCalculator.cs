using System;
using System.Collections.Generic;
using System.Linq;
using Trains.Domain;

namespace Trains
{
	public interface ICalculateDistances
	{
		int Calculate(string route);
	}

	public class DistanceCalculator : ICalculateDistances
	{
		private readonly List<ConnectedStations> _routingData;

		public DistanceCalculator(List<ConnectedStations> routingData)
		{
			_routingData = routingData;
		}

		public int Calculate(string route)
		{
			var stations = route.Split(Convert.ToChar("-"));

			var distance = 0;
			for (var i = 1; i < stations.Count(); i++)
			{
				var startStation = stations[i - 1];
				var endStation =stations[i];

				if (!_routingData.Any(r => r.StartStation == startStation && r.EndStation == endStation))
					throw new RouteNotFoundException();

				distance += _routingData.First(r => r.StartStation == startStation
				                                    && r.EndStation == endStation).Distance;
			}

			return distance;
		}
	}
}