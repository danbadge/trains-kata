using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Trains.Domain
{
	public class RoutingDataStore
	{
		public List<ConnectedStations> ConnectedStations { get; private set; }

		public void LoadFrom(string fileLocation)
		{
			ConnectedStations = new List<ConnectedStations>();

			if (!File.Exists(fileLocation))
				throw new FileNotFoundException("Routing data file could not be found");
			
			var routingData = File.ReadAllText(fileLocation);
			
			if (String.IsNullOrEmpty(routingData))
				throw new FileLoadException("Routing data file contained no data");

			ConvertTextToAListOfConnectedStations(routingData);
		}

		private void ConvertTextToAListOfConnectedStations(string routingData)
		{
			var routes = routingData.Split(Convert.ToChar(",")).Select(r => r.Trim()).ToArray();

			foreach (var stations in routes.Select(route => route.ToCharArray()))
			{
				var connectedStations = new ConnectedStations(stations[0].ToString(CultureInfo.InvariantCulture),
				                                              stations[1].ToString(CultureInfo.InvariantCulture),
				                                              (int) char.GetNumericValue(stations[2]));
				ConnectedStations.Add(connectedStations);
			}
		}
	}
}