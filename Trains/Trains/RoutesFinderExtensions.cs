using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains
{
	public static class RoutesFinderExtensions
	{
		public static int WithMaxStops(this List<List<Tuple<string, string>>> routes, int maxStops)
		{
			return routes.Count(r => r.Count() <= maxStops);
		}

		public static int WithExactStops(this List<List<Tuple<string, string>>> routes, int exactStops)
		{
			return routes.Count(r => r.Count() == exactStops);
		}
	}
}