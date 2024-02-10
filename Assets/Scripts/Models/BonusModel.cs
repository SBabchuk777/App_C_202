using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class BonusModel
    {
        private List<int> _prizes;
        private List<int> _angles;
        private int _currentRandomIndex;

        public int GetAngle
        {
            get
            {
                _currentRandomIndex = Random.Range(0, _angles.Count);

                return _angles[_currentRandomIndex];
            }
        }

        public int GetPrize => _prizes[_currentRandomIndex];

        public BonusModel()
        {
            FillPrizesAndAnglesLists();
        }

        private void FillPrizesAndAnglesLists()
        {
            _prizes = new List<int>();
            _angles = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                int prize;
                int angle;
                
                switch (i)
                {
                    case 0:
                        prize = 100;
                        angle = 0;
                        break;
                    case 1:
                        prize = 200;
                        angle = 45;
                        break;
                    case 2:
                        prize = 50;
                        angle = 90;
                        break;
                    case 3:
                        prize = 0;
                        angle = 135;
                        break;
                    case 4:
                        prize = 10;
                        angle = 180;
                        break;
                    case 5:
                        prize = 0;
                        angle = 225;
                        break;
                    case 6:
                        prize = 50;
                        angle = 270;
                        break;
                    default:
                        prize = 10;
                        angle = 315;
                        break;
                }
                
                _prizes.Add(prize);
                _angles.Add(angle);
            }
        }
    }
}