using TMPro;
using UnityEngine;

namespace DivinityGaz.Settings.UI
{
    public class KeyBindingButtons : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyText = null;
        [SerializeField] private string actionName = string.Empty;

        private void Start () => keyText.text = CheckIfAlpha(InputManager.IM.GetActionKeyName(actionName));

        private string CheckIfAlpha (string actionName)
        {
            string returnString = actionName;

            switch (actionName)
            {
                case "Alpha1": returnString = "&"; break;
                case "Alpha2": returnString = "é"; break;
                case "Alpha3": returnString = "#"; break;
                case "Alpha4": returnString = "'"; break;
                case "Alpha5": returnString = "("; break;
                case "Alpha6": returnString = "-"; break;
                case "Alpha7": returnString = "è"; break;
                case "Alpha8": returnString = "_"; break;
                case "Alpha9": returnString = "ç"; break;
                case "Alpha0": returnString = "à"; break;
            }

            return returnString;
        }
    }
}