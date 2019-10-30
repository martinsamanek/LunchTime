using GeoCoordinatePortable;

namespace LunchTime.Restaurants.MenuBrno
{
	public class DaVinci : ABrnoRestaurant
	{
		public override string Name => "Da Vinci Restaurant & Caffe Bar";

		public override string Url => "https://menubrno.cz/restaurace/0261-da-vinci-restaurant-caffe-bar/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1961364, 16.6097242);
	}
}
