using GeoCoordinatePortable;
using LunchTime.Enums;

namespace LunchTime.Restaurants.MenuBrno
{
	public class Statl : MenuBrnoBase
	{
		public override string Name => "Štatl";

		public override string Url => "https://menubrno.cz/restaurace/0349-Statl/";

		public override string Web => "";

		public override GeoCoordinate Location => new GeoCoordinate(49.1955489, 16.6074561);

		public override CityEnum City => CityEnum.Brno;

		protected override int[] SoupLinesPositions => new[] { 1 };

		protected override int FirstMealLinesPositions => 2;
	}
}
