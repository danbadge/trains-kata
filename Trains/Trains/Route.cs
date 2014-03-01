using System.Collections.Generic;

namespace Trains
{
	public class Route : List<ConnectedStations>
	{
		public Route Copy()
		{
			var route = new Route();
			route.AddRange(this);
			return route;
		}
	}
}