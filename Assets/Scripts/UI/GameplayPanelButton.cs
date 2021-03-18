using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.UI
{
    public class GameplayPanelButton : MonoBehaviour
    {
        [SerializeField] private GameplayPanelButtonsManager manager = null; 
        [SerializeField] private Image backgroundEffect = null;

        private void Awake ()
        {
            backgroundEffect.transform.DOScaleX(0f, 0f);
        }

        internal void EnableTween()
        {
            backgroundEffect.transform.DOScaleX(1f, manager.TweenDuration);
        }

        internal void DisableTween ()
        {
            backgroundEffect.transform.DOScaleX(0f, manager.TweenDuration);
        }
    }
}