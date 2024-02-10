using System;
using UnityEngine;
using DG.Tweening;

namespace Views.Bonus
{
    public class WheelView : MonoBehaviour
    {
        public Action WheelEndRotationAction { get; set; }

        public void RotateWheel(int endRotatePoint)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -25);

            transform.DORotate(new Vector3(0, 0, 720 + 25 + endRotatePoint), 1.5f, RotateMode.WorldAxisAdd)
                .OnComplete(delegate { WheelEndRotationAction.Invoke(); });
        }
    }
}