using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class NaKnofliku : ABrnoRestaurant
	{
		public override string Name => "Na Knoflíku";

		public override string Url => "https://menubrno.cz/restaurace/0253-na-knofliku/";

		public override GeoCoordinate Location => new GeoCoordinate(49.19629, 16.608893);
	}
}
