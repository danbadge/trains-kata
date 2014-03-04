using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains.Domain
{
	public interface ICalculateDistancesOfSpecificRoutes
	{
		int SumDistanceOf(string route);
	}

	public class SpecificRouteDistanceCalculator : ICalculateDistancesOfSpecificRoutes
	{
		private readonly List<ConnectedStations> _routingData;

		public SpecificRouteDistanceCalculator(List<ConnectedStations> routingData)
		{
			_routingData = routingData;
		}

		public int SumDistanceOf(string route)
		{
			var stations = route.Split(Convert.ToChar("-"));

			var distance = 0;
			for (var i = 1; i < stations.Count(); i++)
			{
				var startStation = stations[i - 1];
				var endStation =stations[i];

				if (RouteCannotBeCompleted(startStation, endStation))
					throw new RouteNotFoundException();

				distance += _routingData.First(r => r.StartStation == startStation
				                                    && r.EndStation == endStation).Distance;
			}

			return distance;
		}

		private bool RouteCannotBeCompleted(string startStation, string endStation)
		{
			return !_routingData.Any(r => r.StartStation == startStation && r.EndStation == endStation);
		}
	}
}