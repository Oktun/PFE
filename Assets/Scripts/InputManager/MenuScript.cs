using DivinityGaz.Managers;
using System.Collections;
using TMPro;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;

    public Transform menuPanel;
    private Event keyEvent;
    private TextMeshProUGUI buttonText;
    private KeyCode newKey;

    bool waitingForKey;

    bool isDisplayingUI = false;

    void Start ()
    {
        //Assign menuPanel to the Panel object in our Canvas
        //Make sure it's not active when the game starts
        //menuPanel = transform.Find("Panel");
        waitingForKey = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update ()
    {
        //Escape key will open or close the panel
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(true);
            gameManager.SettingsUIEnable = menuPanel.gameObject.activeSelf;
        } else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(false);
            gameManager.SettingsUIEnable = menuPanel.gameObject.activeSelf;
        }
    }

    void OnGUI ()
    {
        /*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
        keyEvent = Event.current;

        //Executes if a button gets pressed and
        //the user presses a key
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
            waitingForKey = false;
        }
    }

    /*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
    public void StartAssignment (string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText (TextMeshProUGUI text)
    {
        buttonText = text;
    }

    //Used for controlling the flow of our below Coroutine
    IEnumerator WaitForKey ()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    /*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
    public IEnumerator AssignKey (string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey(); //Executes endlessly until user presses a key

        switch (keyName)
        {
            case "forward":
            InputManager.IM.forward = newKey; //Set forward to new keycode
            buttonText.text = InputManager.IM.forward.ToString(); //Set button text to new key
            PlayerPrefs.SetString("forwardKey", InputManager.IM.forward.ToString()); //save new key to PlayerPrefs
            break;
            case "backward":
            InputManager.IM.backward = newKey; //set backward to new keycode
            buttonText.text = InputManager.IM.backward.ToString(); //set button text to new key
            PlayerPrefs.SetString("backwardKey", InputManager.IM.backward.ToString()); //save new key to PlayerPrefs
            break;
            case "left":
            InputManager.IM.left = newKey; //set left to new keycode
            buttonText.text = InputManager.IM.left.ToString(); //set button text to new key
            PlayerPrefs.SetString("leftKey", InputManager.IM.left.ToString()); //save new key to playerprefs
            break;
            case "right":
            InputManager.IM.right = newKey; //set right to new keycode
            buttonText.text = InputManager.IM.right.ToString(); //set button text to new key
            PlayerPrefs.SetString("rightKey", InputManager.IM.right.ToString()); //save new key to playerprefs
            break;
            case "iKey":
            InputManager.IM.inventoryKey = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.inventoryKey.ToString(); //set button text to new key
            PlayerPrefs.SetString("inventoryKey", InputManager.IM.inventoryKey.ToString()); //save new key to playerprefs
            break;
            case "mKey":
            InputManager.IM.map = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.map.ToString(); //set button text to new key
            PlayerPrefs.SetString("mapKey", InputManager.IM.map.ToString()); //save new key to playerprefs
            break;
            case "hKey":
            InputManager.IM.skillTree = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.skillTree.ToString(); //set button text to new key
            PlayerPrefs.SetString("healKey", InputManager.IM.skillTree.ToString()); //save new key to playerprefs
            break;
            case "axe":
            InputManager.IM.axe = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.axe.ToString(); //set button text to new key
            PlayerPrefs.SetString("axeKey", InputManager.IM.axe.ToString()); //save new key to playerprefs
            break;
            case "hand":
            InputManager.IM.hand = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.hand.ToString(); //set button text to new key
            PlayerPrefs.SetString("handKey", InputManager.IM.hand.ToString()); //save new key to playerprefs
            break;
            case "bow":
            InputManager.IM.bow = newKey; //set jump to new keycode
            buttonText.text = InputManager.IM.bow.ToString(); //set button text to new key
            PlayerPrefs.SetString("bowKey", InputManager.IM.bow.ToString()); //save new key to playerprefs
            break;
        }

        yield return null;
    }
}
