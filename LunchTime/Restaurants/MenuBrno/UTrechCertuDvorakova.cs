using GeoCoordinatePortable;

namespace LunchTime.Restaurants.TODO
{
	public class UTrechCertuDvorakova : ABrnoRestaurant
	{
		public override string Name => "U Třech Čertů (Dvořákova)";

		public override string Url => "https://menubrno.cz/restaurace/0231-u-trech-Certu/";

		public override GeoCoordinate Location => new GeoCoordinate(49.1961267, 16.6107650);
		protected override int[] SoupLinesPositions => new[] { 2 };

		protected override int FirstMealLinesPositions => 3;
	}
}
