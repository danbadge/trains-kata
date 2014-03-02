using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public interface IFindRoutes
	{
		List<Route> GetRoutes(string startStation, string endStation);
		Route GetShortestRoute(string startStation, string endStation);
	}

	public class RoutesFinder : IFindRoutes
	{
		private readonly List<ConnectedStations> _connectedStations;

		public RoutesFinder(List<ConnectedStations> connectedStations)
		{
			_connectedStations = connectedStations;
		}

		public Route GetShortestRoute(string start, string end)
		{
			var allRoutes = GetRoutes(start, end);

			var shortestRoute = allRoutes.OrderBy(r => r.TotalDistance).First();

			return shortestRoute;
		}

		public List<Route> GetRoutes(string startStation, string endStation)
		{
			var possibleRoutes = new List<Route>();
			
			var startingRoutes =  _connectedStations.Where(r => r.StartStation == startStation);

			foreach (var startingRoute in startingRoutes)
			{
				var possibleRoute = new Route
					{
						startingRoute
					};

				var routes = FindCompleteRoutes(endStation, startingRoute.EndStation, possibleRoute);
				possibleRoutes.AddRange(routes);
			}

			return possibleRoutes;
		}

		private IEnumerable<Route> FindCompleteRoutes(string endStation, string nextStation, Route possibleRoute)
		{
			if (possibleRoute.Count() >= 10)
			{
				return new List<Route>();
			}

			var completedRoutes = new List<Route>();

			var nextRoutes = _connectedStations.Where(r => r.StartStation == nextStation).ToList();
			var endRoutes = _connectedStations.Where(r => r.StartStation == nextStation && r.EndStation == endStation).ToList();

			foreach (var endRoute in endRoutes)
			{
				var completedRoute = possibleRoute.Copy();
				completedRoute.Add(endRoute);
				completedRoutes.Add(completedRoute);
			}

			foreach (var nextRoute in nextRoutes)
			{
				var anotherPossibleRoute = possibleRoute.Copy();
				anotherPossibleRoute.Add(nextRoute);
				var routes = FindCompleteRoutes(endStation, nextRoute.EndStation, anotherPossibleRoute);
				completedRoutes.AddRange(routes);
			}

			return completedRoutes;
		}
	}
}