using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class PreviewItemView : MonoBehaviour
    {
        [SerializeField] 
        private Image _mainImage;
        [SerializeField] 
        private List<Sprite> _sprites;

        public void SetSprite(int index)
        {
            _mainImage.sprite = _sprites[index];
            _mainImage.SetNativeSize();

            RectTransform imageRect = _mainImage.rectTransform;
            
            imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, imageRect.rect.width/4);
            imageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, imageRect.rect.height/4);
        }
    }
}