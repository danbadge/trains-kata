using System;

namespace Trains
{
	public class RouteNotFoundException : Exception
	{
		public RouteNotFoundException() : base("NO SUCH ROUTE")
		{
		}
	}
}