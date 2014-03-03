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
		private const int MaxNumberOfStations = 10;
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
			return GetCompleteRoutes(startStation, endStation, new Route());
		}

		private List<Route> GetCompleteRoutes(string nextStation, string endStation, Route possibleRoute)
		{
			if (possibleRoute.Count() >= MaxNumberOfStations)
				return new List<Route>();

			var allRoutes = new List<Route>();

			var completedRoutes = GetAnyCompletedRoutes(nextStation, endStation, possibleRoute);
			allRoutes.AddRange(completedRoutes);

			var longerCompletedRoutes = GetAnyCompletedRoutesBeyondImmediateStations(nextStation, endStation, possibleRoute);
			allRoutes.AddRange(longerCompletedRoutes);

			return allRoutes;
		}

		private IEnumerable<Route> GetAnyCompletedRoutesBeyondImmediateStations(string nextStation, string endStation, Route routeTaken)
		{
			var completedRoutes = new List<Route>();

			var nextConnections = _connectedStations.Where(r => r.StartStation == nextStation).ToList();
			foreach (var nextConnection in nextConnections)
			{
				var potentialRoute = routeTaken.Copy();
				potentialRoute.Add(nextConnection);

				var routes = GetCompleteRoutes(nextConnection.EndStation, endStation, potentialRoute);
				completedRoutes.AddRange(routes);
			}

			return completedRoutes;
		}

		private IEnumerable<Route> GetAnyCompletedRoutes(string nextStation, string endStation, Route routeTaken)
		{
			var completedRoutes = new List<Route>();

			var finalConnections = _connectedStations
				.Where(r => r.StartStation == nextStation
				            && r.EndStation == endStation).ToList();

			foreach (var finalConnection in finalConnections)
			{
				var completedRoute = routeTaken.Copy();
				completedRoute.Add(finalConnection);
				completedRoutes.Add(completedRoute);
			}
			return completedRoutes;
		}
	}
}