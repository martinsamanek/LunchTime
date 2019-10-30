using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
	public class UTrechCertuStarobrnenska : ABrnoRestaurant
	{
		public override string Name => "U Třech Čertů (Starobrněnská)";

		public override string Url => "https://menubrno.cz/restaurace/0232-u-trech-Certu/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1922914, 16.6071378);
	}
}
