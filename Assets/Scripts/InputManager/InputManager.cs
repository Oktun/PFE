using UnityEngine;

public class InputManager : MonoBehaviour
{

    //Used for singleton
    public static InputManager IM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode inventoryKey { get; set; }
    public KeyCode map { get; set; }
    public KeyCode skillTree { get; set; }
    public KeyCode hand { get; set; }
    public KeyCode axe { get; set; }
    public KeyCode bow { get; set; }
    public KeyCode forward { get; set; } = KeyCode.Z;
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }

    void Awake ()
    {
        //Singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        } else if (IM != this)
        {
            Destroy(gameObject);
        }

        /*Assign each keycode when the game starts.
		 * Loads data from PlayerPrefs so if a user quits the game, 
		 * their bindings are loaded next time. Default values
		 * are assigned to each Keycode via the second parameter
		 * of the GetString() function
		 */
        inventoryKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("inventoryKey", "I"));
        hand = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("handKey", "1"));
        axe = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("axeKey", "2"));
        bow = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bowKey", "3"));
        skillTree = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillKey", "H"));
        map = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mapKey", "M"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "Z"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

    }

    public string GetActionKeyName (string actionName)
    {
        string actionKey = string.Empty;

        switch (actionName)
        {
            case "foward": actionKey = forward.ToString(); break;
            case "backward": actionKey = backward.ToString(); break;
            case "left": actionKey = left.ToString(); break;
            case "right": actionKey = right.ToString(); break;
            case "axe": actionKey = axe.ToString(); break;
            case "bow": actionKey = bow.ToString(); break;
            case "hand": actionKey = hand.ToString(); break;
            case "skillTree": actionKey = skillTree.ToString(); break;
            case "inventory": actionKey = inventoryKey.ToString(); break;
            case "map": actionKey = map.ToString(); break;
        }

        return actionKey;
    }
}
