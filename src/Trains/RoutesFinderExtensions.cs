using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public static class RoutesFinderExtensions
	{
		public static List<Route> WithMaxStops(this List<Route> routes, int maxStops)
		{
			return routes.Where(r => r.Count() <= maxStops).ToList();
		}

		public static List<Route> WithExactStops(this List<Route> routes, int exactStops)
		{
			return routes.Where(r => r.Count() == exactStops).ToList();
		}

		public static List<Route> WithADistanceLessThan(this List<Route> routes, int maxDistance)
		{
			return routes.Where(r => r.TotalDistance < maxDistance).ToList();
		}
	}
}