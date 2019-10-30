using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class PivniOpice : ABrnoRestaurant
	{
		public override string Name => "Pivní opice";

		public override string Url => "https://menubrno.cz/restaurace/0073-restaurace-pivni-opice/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1979103, 16.6060008);
	}
}
