using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Trains.Domain;

namespace Trains.Tests
{
	[TestFixture]
	public class RoutingDataStoreTests
	{
		[Test]
		public void Should_throw_exception_if_routing_data_can_not_be_found()
		{
			var routingDataStore = new RoutingDataStore();

			var exception = Assert.Throws<FileNotFoundException>(routingDataStore.Load);

			Assert.That(exception.Message, Is.EqualTo("Routing data file could not be found"));
		}

		[Test]
		public void Should_throw_exception_if_routing_data_file_is_empty()
		{
			File.WriteAllText("routing-data.txt", "");

			var routingDataStore = new RoutingDataStore();

			var exception = Assert.Throws<Exception>(routingDataStore.Load);

			Assert.That(exception.Message, Is.EqualTo("Routing data file contained no data"));
		}

		[Test]
		public void Should_populate_connected_stations_from_data_file()
		{
			File.WriteAllText("routing-data.txt", "AB4, BC5, DC6");

			var routingDataStore = new RoutingDataStore();

			routingDataStore.Load();

			var firstConnectedStation = routingDataStore.ConnectedStations.First();
			Assert.That(firstConnectedStation.StartStation, Is.EqualTo("A"));
			Assert.That(firstConnectedStation.EndStation, Is.EqualTo("B"));
			Assert.That(firstConnectedStation.Distance, Is.EqualTo(4));

			Assert.That(routingDataStore.ConnectedStations.Count(), Is.EqualTo(3));
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete("routing-data.txt");
		}
	}
}
