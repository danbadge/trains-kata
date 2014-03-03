using System.Collections.Generic;
using System.Linq;

namespace Trains.Domain
{
	public class Route : List<ConnectedStations>
	{
		public Route Copy()
		{
			var route = new Route();
			route.AddRange(this);
			return route;
		}

		public int TotalDistance
		{
			get { return this.Sum(r => r.Distance); }
		}
	}
}