﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public interface ICalculateDistances
	{
		int Calculate(string route);
	}

	public class DistanceCalculator : ICalculateDistances
	{
		private readonly Dictionary<string, int> _distances;

		public DistanceCalculator(string routingData)
		{
			_distances = GetDistancesBetweenStations(routingData);
		}

		public int Calculate(string route)
		{
			var stations = route.Split(Convert.ToChar("-"));

			var distance = 0;
			for (var i = 1; i < stations.Count(); i++)
			{
				var immediateStations = stations[i - 1] + stations[i];

				if (!_distances.ContainsKey(immediateStations))
					throw new RouteNotFoundException();

				distance += _distances[immediateStations];
			}

			return distance;
		}

		private static Dictionary<string, int> GetDistancesBetweenStations(string routingData)
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