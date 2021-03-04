using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Transform menuPanel;
	Event keyEvent;
	Text buttonText;
	KeyCode newKey;

	bool waitingForKey;


	void Start ()
	{
		//Assign menuPanel to the Panel object in our Canvas
		//Make sure it's not active when the game starts
		//menuPanel = transform.Find("Panel");
		menuPanel.gameObject.SetActive(false);
		waitingForKey = false;

		/*iterate through each child of the panel and check
		 * the names of each one. Each if statement will
		 * set each button's text component to display
		 * the name of the key that is associated
		 * with each command. Example: the ForwardKey
		 * button will display "W" in the middle of it
		 */
		for(int i = 0; i < menuPanel.childCount; i++)
		{
			if(menuPanel.GetChild(i).name == "ForwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.forward.ToString();
			else if(menuPanel.GetChild(i).name == "BackwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.backward.ToString();
			else if(menuPanel.GetChild(i).name == "LeftKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.left.ToString();
			else if(menuPanel.GetChild(i).name == "RightKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.right.ToString();
			else if(menuPanel.GetChild(i).name == "InventoryKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.InventoryKey.ToString();
			else if(menuPanel.GetChild(i).name == "MapKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.mKey.ToString();
			else if(menuPanel.GetChild(i).name == "HealKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = InputManager.IM.hKey.ToString();
		}
	}


	void Update ()
	{
		//Escape key will open or close the panel
		if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            menuPanel.gameObject.SetActive(true);
        }
		else if(Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
			menuPanel.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}

	void OnGUI()
	{
		/*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
		keyEvent = Event.current;

		//Executes if a button gets pressed and
		//the user presses a key
		if(keyEvent.isKey && waitingForKey)
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
	public void StartAssignment(string keyName)
	{
		if(!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	//Assigns buttonText to the text component of
	//the button that was pressed
	public void SendText(Text text)
	{
		buttonText = text;
	}

	//Used for controlling the flow of our below Coroutine
	IEnumerator WaitForKey()
	{
		while(!keyEvent.isKey)
			yield return null;
	}

	/*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;

		yield return WaitForKey(); //Executes endlessly until user presses a key

		switch(keyName)
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
			InputManager.IM.InventoryKey = newKey; //set jump to new keycode
			buttonText.text = InputManager.IM.InventoryKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("inventoryKey", InputManager.IM.InventoryKey.ToString()); //save new key to playerprefs
			break;
		case "mKey":
			InputManager.IM.mKey = newKey; //set jump to new keycode
			buttonText.text = InputManager.IM.mKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("mapKey", InputManager.IM.mKey.ToString()); //save new key to playerprefs
			break;
		case "hKey":
			InputManager.IM.hKey = newKey; //set jump to new keycode
			buttonText.text = InputManager.IM.hKey.ToString(); //set button text to new key
			PlayerPrefs.SetString("healKey", InputManager.IM.hKey.ToString()); //save new key to playerprefs
			break;
		}

		yield return null;
	}
}
