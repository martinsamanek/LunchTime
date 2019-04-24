namespace LunchTime.Restaurants.MenuBrno
{
    public class PivniOpice : MenuBrnoBase
    {
        public override string Name => "Pivní opice";
        public override string Url => "https://menubrno.cz/restaurace/0073-restaurace-pivni-opice/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}