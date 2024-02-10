using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Views.Shop
{
    public class ShopPanelView : MonoBehaviour
    {
        public Action<int> PressBtnAction { get; set; }

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

        private void Notification(int index)
        {
            PressBtnAction.Invoke(index);
        }
    }
}