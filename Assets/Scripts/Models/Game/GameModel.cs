namespace Models.Game
{
    public class GameModel
    {
        public bool IsActiveBoosterBtn(int count)
        {
            return  count > 0;
        }
    }
}