using UnityEngine;
using UnityEngine.UI;

namespace Views.Bonus
{
    public class ResultPanelView : MonoBehaviour
    {
        [SerializeField] 
        private Text _text;

        public void UpdateText(bool value, int prize)
        {
            string text = value ? $"You win {prize} coins!" : "Try again tomorrow";

            _text.text = text;
        }
    }
}