using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DivinityGaz.LevelSystem
{
    public class PlayerLevelUI : MonoBehaviour
    {
        private Image backgroundImage = null;
        private float backgroundImageAlphaAmount;

        [Header("References")]
        [SerializeField] private Image progressBar = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private TextMeshProUGUI xpAddedText = null;

        //Fade values
        [Header("Timing Settings")]
        [SerializeField] private float fadeInDuration = 0.2f;
        [SerializeField] private float fadeOutDuration = 0.4f;
        
        private bool isFading = false;
        public bool IsFading { get { return isFading; } }

        public void UpdateUI (LevelUIHanlder levelUIHanlder)
        {
            levelText.text = "level " + levelUIHanlder.CurrentLevel.ToString();
            xpAddedText.text = levelUIHanlder.CurrentXp.ToString() + " / " + levelUIHanlder.CurrentLevelMaxXP.ToString();
            progressBar.fillAmount = (float)levelUIHanlder.CurrentXp / levelUIHanlder.CurrentLevelMaxXP;
        }

        private void Awake ()
        {
            backgroundImage = GetComponent<Image>();
            backgroundImageAlphaAmount = backgroundImage.color.a;
            levelText.DOFade(0f, 0f);
            xpAddedText.DOFade(0f, 0f);
            progressBar.DOFade(0f, 0f);
            backgroundImage.DOFade(0f, 0f);
        }

        public void EnableTween () => TweenFade(1f, true);
        public void DisableTween () => TweenFade(0f, false);
        private void TweenFade (float alphaValue, bool isFadeIn)
        {
            float fadeDuration = isFadeIn ? fadeInDuration : fadeOutDuration;
            isFading = true;
            levelText.DOFade(alphaValue, fadeDuration);
            xpAddedText.DOFade(alphaValue, fadeDuration);
            progressBar.DOFade(alphaValue, fadeDuration);
            backgroundImage.DOFade(isFadeIn ? backgroundImageAlphaAmount : 0f, fadeDuration)
                .OnComplete(() =>
                {
                    isFading = false;
                });
        }
    }
}