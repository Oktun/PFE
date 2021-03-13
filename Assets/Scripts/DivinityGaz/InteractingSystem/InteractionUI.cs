using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel = null;

    [Space]
    [SerializeField] private TextMeshProUGUI interactionText = null;

    public void DisplayText (string textToDisplay)
    {
        interactionText.text = textToDisplay;
        interactionPanel.SetActive(true);
    }

    public void DisableDisplay ()
    {
        interactionPanel.SetActive(false);
    }
}
