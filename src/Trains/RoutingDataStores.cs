﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Trains
{
	public class RoutingDataStores
	{
		public List<ConnectedStations> ConnectedStations { get; private set; }

		public void Load()
		{
			ConnectedStations = new List<ConnectedStations>();

			const string fileLocation = "routing-data.txt";

			var routingData = File.ReadAllText(fileLocation);

			var routes = routingData.Split(Convert.ToChar(" ")).ToArray();

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