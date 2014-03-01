using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Trains.Tests
{
	[TestFixture]
	public class NumberOfRoutesFinderTests
	{
		[Test]
		public void Given_a_route_of_C_to_C_then_return_the_number_of_routes_with_no_more_than_3_stops()
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var routesFinder = new RoutesFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("C", "C").WithMaxStops(3);

			Assert.That(numberOfRoutes, Is.EqualTo(2));
		}

		[Test]
		public void Given_a_route_of_A_to_C_then_return_the_number_of_routes_with_exactly_4_stops()
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var routesFinder = new RoutesFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("A", "C").WithExactStops(4);

			Assert.That(numberOfRoutes, Is.EqualTo(3));
		}

		[Test]
		public void Given_a_route_of_A_to_C_then_return_the_shortest_distance_available_to_travel()
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var routesFinder = new RoutesFinder(routingData);

			var distance = routesFinder.GetShortestRoute("A", "C").TotalDistance;

			Assert.That(distance, Is.EqualTo(9));
		}

		[Test]
		public void Given_a_route_of_B_to_B_then_return_the_shortest_distance_available_to_travel()
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var routesFinder = new RoutesFinder(routingData);

			var distance = routesFinder.GetShortestRoute("B", "B").TotalDistance;

			Assert.That(distance, Is.EqualTo(9));
		}

		[Test]
		public void Given_a_route_of_C_to_C_then_return_the_number_of_routes_with_a_distance_less_than_30()
		{
			const string routingData = "AB5 BC4 CD8 DC8 DE6 AD5 CE2 EB3 AE7";

			var routesFinder = new RoutesFinder(routingData);

			var numberOfRoutes = routesFinder.GetRoutes("C", "C").WithADistanceLessThan(30);

			Assert.That(numberOfRoutes, Is.EqualTo(7));
		}

		[Test]
		public void Should_stop_searching_for_routes_after_10_stops()
		{
			const string routingData = "AC5 CA4";

			var routesFinder = new RoutesFinder(routingData);

			var routes = routesFinder.GetRoutes("A", "A");

			var maxStops = routes.OrderByDescending(r => r.Count()).First().Count();
			Assert.That(maxStops, Is.EqualTo(10));
		}

		[Test]
		public void Should_try_a_similar_route_after_exhausting_one_path()
		{
			const string routingData = "AC5 CA4 CE5 EA3";

			var routesFinder = new RoutesFinder(routingData);

			var routes = routesFinder.GetRoutes("A", "A");
			
			Assert.True(routes.Any(r => OutputRouteAsString(r) == "ACCEEA"));
		}

		private string OutputRouteAsString(IEnumerable<ConnectedStations> route)
		{
			return route.Aggregate("", (current, stations) => current + (stations.StartStation + stations.EndStation));
		}
	}
}