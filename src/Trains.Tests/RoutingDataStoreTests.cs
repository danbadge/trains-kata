using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Trains.Tests
{
	[TestFixture]
	public class RoutingDataStoreTests
	{
		[Test]
		public void Should_throw_exception_if_routing_data_can_not_be_found()
		{
			var routingDataStore = new RoutingDataStores();

			Assert.Throws<FileNotFoundException>(routingDataStore.Load);
		}

		[Test]
		public void Should_populate_connected_stations_from_data_file()
		{
			File.WriteAllText("routing-data.txt", "AB4 BC5");

			var routingDataStore = new RoutingDataStores();

			routingDataStore.Load();

			var firstConnectedStation = routingDataStore.ConnectedStations.First();
			Assert.That(firstConnectedStation.StartStation, Is.EqualTo("A"));
			Assert.That(firstConnectedStation.EndStation, Is.EqualTo("B"));
			Assert.That(firstConnectedStation.Distance, Is.EqualTo(4));
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete("routing-data.txt");
		}
	}
}
