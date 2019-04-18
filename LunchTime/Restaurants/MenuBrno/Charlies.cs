namespace LunchTime.Restaurants.MenuBrno
{
    public class Charlies : MenuBrnoBase
    {
        public override int Id => 2;
        public override string Name => "Charlies square";
        public override string Url => "https://menubrno.cz/restaurace/0070-charlies-square/";
        public override string Web => "http://www.charliessquare.cz/denni-menu";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}