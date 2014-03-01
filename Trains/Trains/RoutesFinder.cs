using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public class RoutesFinder
	{
		private readonly List<Tuple<string, string>> _routes = new List<Tuple<string, string>>();
	
		public RoutesFinder(string routingData)
		{
			var routingDataWithoutDistance = RemoveDigitsFrom(routingData);

			var routes = routingDataWithoutDistance.Split(Convert.ToChar(" ")).ToArray();

			foreach (var stations in routes.Select(route => route.ToCharArray()))
			{
				_routes.Add(new Tuple<string, string>(stations[0].ToString(), stations[1].ToString()));
			}
		}

		public List<List<Tuple<string, string>>> GetRoutes(string startStation, string endStation)
		{
			var possibleRoutes = new List<List<Tuple<string, string>>>();
			
			var startingRoutes =  _routes.Where(r => r.Item1 == startStation);

			foreach (var startingRoute in startingRoutes)
			{
				var possibleRoute = new List<Tuple<string, string>>
					{
						startingRoute
					};

				var routes = FindCompleteRoutes(endStation, startingRoute.Item2, possibleRoute);
				possibleRoutes.AddRange(routes);
			}

			return possibleRoutes;
		}

		private IEnumerable<List<Tuple<string, string>>> FindCompleteRoutes(string endStation, string nextStation, List<Tuple<string, string>> possibleRoute)
		{
			if (possibleRoute.Count() >= 10)
			{
				return new List<List<Tuple<string, string>>>();
			}

			var completedRoutes = new List<List<Tuple<string, string>>>();

			var nextRoutes = _routes.Where(r => r.Item1 == nextStation).ToList();
			var endRoutes = _routes.Where(r => r.Item1 == nextStation && r.Item2 == endStation).ToList();

			foreach (var endRoute in endRoutes)
			{
				var completedRoute = possibleRoute.Select(r => r).ToList();
				completedRoute.Add(endRoute);
				completedRoutes.Add(completedRoute);
			}

			foreach (var nextRoute in nextRoutes)
			{
				var anotherPossibleRoute = possibleRoute.Select(r => r).ToList();
				anotherPossibleRoute.Add(nextRoute);
				var routes = FindCompleteRoutes(endStation, nextRoute.Item2, anotherPossibleRoute);
				completedRoutes.AddRange(routes);
			}

			return completedRoutes;
		}

		private static string RemoveDigitsFrom(string routingData)
		{
			return new string(routingData.Where(r => char.IsLetter(r)
			                                         || char.IsSeparator(r)).ToArray());
		}
	}
}