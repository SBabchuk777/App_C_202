using Unity.VisualScripting;

namespace Models.Game
{
    public class GameModel
    {
        private int _score;

        public int Score => _score;
        
        public bool IsActiveBoosterBtn(int count)
        {
            return  count > 0;
        }

        public GameModel()
        {
            _score = 0;
        }

        public void UpdateScore(int index)
        {
            int addValue = CountScoreAdd(index);

            _score += addValue;
        }

        private int CountScoreAdd(int index)
        {
            switch (index)
            {
                case 0:
                    return 15;
                case 1:
                    return 5;
               case 2:
                    return 8;
               default:
                   return 0;
            }
        }
    }
}