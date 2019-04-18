namespace LunchTime.Restaurants.MenuBrno
{
    public class Jakoby : MenuBrnoBase
    {
        public override int Id => 5;
        public override string Name => "Jakoby";
        public override string Url => "https://menubrno.cz/restaurace/0091-jakoby/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {3};
        protected override int FirstMealLinesPositions => 5;
    }
}