using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityGaz.UI
{
    public class GameplayPanelButtonsManager : MonoBehaviour
    {
        [SerializeField] private float tweenDuration = 0.7f;
        internal float TweenDuration { get { return tweenDuration; } }

        [SerializeField] private GameplayPanelButton lastButtonClicked;

        private List<GameplayPanelButton> buttons = new List<GameplayPanelButton>();

        private void Awake ()
        {
            foreach (Transform transform in transform)
            {
                if (transform.TryGetComponent(out GameplayPanelButton button))
                {
                    buttons.Add(button);
                }
            }

            lastButtonClicked.EnableTween();
        }

        public void OnButtonClicked (GameplayPanelButton newButton)
        {
            if (newButton == lastButtonClicked) { return; }

            lastButtonClicked.DisableTween();
            newButton.EnableTween();
            lastButtonClicked = newButton;
        }
    }
}