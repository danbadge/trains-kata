using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public interface IFindRoutes
	{
		List<Route> GetRoutes(string startStation, string endStation);
		Route GetShortestRoute(string startStation, string endStation);
	}

	public class RouteFinder : IFindRoutes
	{
		private readonly List<ConnectedStations> _connectedStations;

		public RouteFinder(List<ConnectedStations> connectedStations)
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
			var completedRoutes = FindCompleteRoutes(startStation, endStation, new Route());
			return completedRoutes;
		}

		private List<Route> FindCompleteRoutes(string nextStation, string endStation, Route possibleRoute)
		{
			if (possibleRoute.Count() >= 10)
				return new List<Route>();

			var completedRoutes = new List<Route>();

			var finishedRoutes = _connectedStations
				.Where(r => r.StartStation == nextStation
				            && r.EndStation == endStation).ToList();

			foreach (var finishedRoute in finishedRoutes)
			{
				var completedRoute = possibleRoute.Copy();
				completedRoute.Add(finishedRoute);
				completedRoutes.Add(completedRoute);
			}

			var nextRoutes = _connectedStations.Where(r => r.StartStation == nextStation).ToList();
			foreach (var nextRoute in nextRoutes)
			{
				var anotherPossibleRoute = possibleRoute.Copy();
				anotherPossibleRoute.Add(nextRoute);

				var routes = FindCompleteRoutes(nextRoute.EndStation, endStation, anotherPossibleRoute);
				completedRoutes.AddRange(routes);
			}

			return completedRoutes;
		}
	}
}