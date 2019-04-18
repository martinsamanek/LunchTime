namespace LunchTime.Restaurants.MenuBrno
{
    public class DaVinci : MenuBrnoBase
    {
        public override int Id => 3;
        public override string Name => "Da Vinci Restaurant & Caffe Bar";
        public override string Url => "https://menubrno.cz/restaurace/0261-da-vinci-restaurant-caffe-bar/";
        public override string Web => "";
        protected override int[] SoupLinesPositions => new[] {1};
        protected override int FirstMealLinesPositions => 2;
    }
}