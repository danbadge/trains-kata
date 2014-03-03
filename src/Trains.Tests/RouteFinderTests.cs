using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Trains.Domain;

namespace Trains.Tests
{
	[TestFixture]
	public class RouteFinderTests
	{
		[Test]
		public void Given_a_route_of_C_to_C_then_return_the_number_of_routes_with_no_more_than_3_stops()
		{
			var routingData = TestData.ConnectedStations;

			var routesFinder = new RouteFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("C", "C").WithMaxStops(3).Count();

			Assert.That(numberOfRoutes, Is.EqualTo(2));
		}

		[Test]
		public void Given_a_route_of_A_to_C_then_return_the_number_of_routes_with_exactly_4_stops()
		{
			var routingData = TestData.ConnectedStations;

			var routesFinder = new RouteFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("A", "C").WithExactStops(4).Count();

			Assert.That(numberOfRoutes, Is.EqualTo(3));
		}

		[Test]
		public void Given_a_route_of_A_to_C_then_return_the_shortest_distance_available_to_travel()
		{
			var routingData = TestData.ConnectedStations;

			var routesFinder = new RouteFinder(routingData);

			var distance = routesFinder.GetShortestRoute("A", "C").TotalDistance;

			Assert.That(distance, Is.EqualTo(9));
		}

		[Test]
		public void Given_a_route_of_B_to_B_then_return_the_shortest_distance_available_to_travel()
		{
			var routingData = TestData.ConnectedStations;

			var routesFinder = new RouteFinder(routingData);

			var distance = routesFinder.GetShortestRoute("B", "B").TotalDistance;

			Assert.That(distance, Is.EqualTo(9));
		}

		[Test]
		public void Given_a_route_of_C_to_C_then_return_the_number_of_routes_with_a_distance_less_than_30()
		{
			var routingData = TestData.ConnectedStations;

			var routesFinder = new RouteFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("C", "C").WithADistanceLessThan(30).Count();

			Assert.That(numberOfRoutes, Is.EqualTo(7));
		}

		[Test]
		public void Should_stop_searching_for_routes_after_10_stops()
		{
			var routingData = new List<ConnectedStations>
				{
					new ConnectedStations("A", "C", 5),
					new ConnectedStations("C", "A", 4)
				};

			var routesFinder = new RouteFinder(routingData);

			var routes = routesFinder.GetRoutes("A", "A");

			var maxStops = routes.OrderByDescending(r => r.Count()).First().Count();
			Assert.That(maxStops, Is.EqualTo(10));
		}

		[Test]
		public void Should_try_a_similar_route_after_exhausting_one_path()
		{
			var routingData = new List<ConnectedStations>
				{
					new ConnectedStations("A", "C", 5),
					new ConnectedStations("C", "A", 4),
					new ConnectedStations("C", "E", 5),
					new ConnectedStations("E", "A", 3)
				};

			var routesFinder = new RouteFinder(routingData);

			var routes = routesFinder.GetRoutes("A", "A");
			
			Assert.True(routes.Any(r => OutputRouteAsString(r) == "ACCEEA"));
		}

		private string OutputRouteAsString(IEnumerable<ConnectedStations> route)
		{
			return route.Aggregate("", (current, stations) => current + (stations.StartStation + stations.EndStation));
		}
	}
}