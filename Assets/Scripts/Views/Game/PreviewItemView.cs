using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

            Vector3 newRotation = new Vector3(0,0,0);
            
            switch (index)
            {
                case 0:
                    newRotation = new Vector3(0, 0, -90);
                    break;
                case 2:
                    newRotation = new Vector3(0, 0, -90);
                    break;
                case 3:
                    newRotation = new Vector3(0, 0, -90);
                    break;
                case 4:
                    newRotation = new Vector3(0, 180, -90);
                    break;
                case 5:
                    newRotation = new Vector3(0, 180, 0);
                    break;
            }

            _mainImage.gameObject.transform.DORotate(newRotation, 0);
        }
    }
}