using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Shop
{
    public class BuyBtn : MonoBehaviour
    {
        public Action PressBtnAction { get; set; }

        [SerializeField] 
        private Button _btn;
        [SerializeField] 
        private GameObject _shadowGO;

        private void OnEnable()
        {
            _btn.onClick.AddListener(Notification);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void SetBtnActive(bool value)
        {
            _btn.interactable = value;
            _shadowGO.SetActive(!value);
        }

        private void Notification()
        {
            PressBtnAction.Invoke();
        }
    }
}