namespace Models
{
    public class ShopModel
    {
        public int FirstBoosterCost => 100;
        public int SecondBoosterCost => 200;

        public bool IsActiveBtn(int moneyCount, int cost)
        {
            return moneyCount >= cost;
        }
    }
}