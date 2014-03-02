namespace Trains
{
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