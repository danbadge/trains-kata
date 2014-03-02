using System.Collections.Generic;

namespace Trains.Tests
{
	public class TestData
	{
		public static List<ConnectedStations> ConnectedStations
		{
			get
			{
				return new List<ConnectedStations>
					{
						new ConnectedStations("A", "B", 5),
						new ConnectedStations("B", "C", 4),
						new ConnectedStations("C", "D", 8),
						new ConnectedStations("D", "C", 8),
						new ConnectedStations("D", "E", 6),
						new ConnectedStations("A", "D", 5),
						new ConnectedStations("C", "E", 2),
						new ConnectedStations("E", "B", 3),
						new ConnectedStations("A", "E", 7),
					};
			}
		}
	}
}