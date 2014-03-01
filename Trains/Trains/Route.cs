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

	public class ConnectedStations
	{
		public ConnectedStations(string start, string end, int distance)
		{
			StartStation = start;
			EndStation = end;
			Distance = distance;
		}

		public string StartStation { get; set; }
		public string EndStation { get; set; }
		public int Distance { get; set; }
	}
}