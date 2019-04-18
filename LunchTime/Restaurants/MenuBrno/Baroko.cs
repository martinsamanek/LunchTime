namespace LunchTime.Restaurants.MenuBrno
{
    public class Baroko : MenuBrnoBase
    {
        public override int Id => 1;
        public override string Name => "Baroko";
        public override string Url => "https://menubrno.cz/restaurace/0076-restaurace-baroko/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}