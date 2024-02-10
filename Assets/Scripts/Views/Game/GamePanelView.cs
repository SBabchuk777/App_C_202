using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class GamePanelView : MonoBehaviour
    {
        public Action<int> PressBtnAction { get; set; }

        [SerializeField] 
        private Text _text;
        [SerializeField] private List<Button> _btns;

        private void OnEnable()
        {
            for (int i = 0; i < _btns.Count; i++)
            {
                int index = i;
                
                _btns[i].onClick.AddListener(delegate { Notification(index); });
            }
        }

        private void OnDisable()
        {
            foreach (var btn in _btns)
            {
                btn.onClick.RemoveAllListeners();
            }
        }

        public void SetText(int score)
        {
            if (_text == null)
            {
                return;
            }

            _text.text = "Your score: " + score;
        }

        private void Notification(int index)
        {
            PressBtnAction.Invoke(index);
        }
    }
}