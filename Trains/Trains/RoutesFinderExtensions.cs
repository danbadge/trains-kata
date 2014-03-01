using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public static class RoutesFinderExtensions
	{
		public static int WithMaxStops(this List<Route> routes, int maxStops)
		{
			return routes.Count(r => r.Count() <= maxStops);
		}

		public static int WithExactStops(this List<Route> routes, int exactStops)
		{
			return routes.Count(r => r.Count() == exactStops);
		}
	}
}