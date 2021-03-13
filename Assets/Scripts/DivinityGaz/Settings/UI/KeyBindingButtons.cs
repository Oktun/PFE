using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DivinityGaz.Settings.UI
{
    public class KeyBindingButtons : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyText = null;
        [SerializeField] private string actionName = string.Empty;

        private void Start () => keyText.text = InputManager.IM.GetActionKeyName(actionName);
    }
}