using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] 
        private Text _scoreText;

        public void UpdateText(int value)
        {
            _scoreText.text = value.ToString();
        }
    }
}