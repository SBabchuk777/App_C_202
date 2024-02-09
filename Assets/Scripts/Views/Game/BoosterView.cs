using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class BoosterView : MonoBehaviour
    {
        public Action PressBtnAction { get; set; }

        [SerializeField] 
        private Button _btn;
        [SerializeField] 
        private Text _countText;

        private void OnEnable()
        {
            _btn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetInteractableBtn(bool value)
        {
            _btn.interactable = value;
        }

        public void SetCountText(int value)
        {
            _countText.text = value.ToString();
        }

        private void Notification()
        {
            PressBtnAction.Invoke();
        }
    }
}