using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	//Used for singleton
	public static InputManager IM;

	//Create Keycodes that will be associated with each of our commands.
	//These can be accessed by any other script in our game
	public KeyCode InventoryKey {get; set;}
	public KeyCode mKey {get; set;}
	public KeyCode hKey {get; set;}
	public KeyCode forward {get; set;}
	public KeyCode backward {get; set;}
	public KeyCode left {get; set;}
	public KeyCode right {get; set;}



	void Awake()
	{
		//Singleton pattern
		if(IM == null)
		{
			DontDestroyOnLoad(gameObject);
            IM = this;
		}	
		else if(IM != this)
		{
			Destroy(gameObject);
		}

		/*Assign each keycode when the game starts.
		 * Loads data from PlayerPrefs so if a user quits the game, 
		 * their bindings are loaded next time. Default values
		 * are assigned to each Keycode via the second parameter
		 * of the GetString() function
		 */
		InventoryKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("inventoryKey", "I"));
		hKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("healKey", "H"));
		mKey = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mapKey", "M"));
		forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "Z"));
		backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
		left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
		right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

	}
}
