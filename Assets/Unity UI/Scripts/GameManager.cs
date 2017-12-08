// written by: Shane Barry
// tested by: Michael Quinn
// debugged by: Shane Barry

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Used for singleton
	public static GameManager GM;

	//Create Keycodes that will be associated with each of our commands.
	//These can be accessed by any other script in our game
	public KeyCode forward {get; set;}
	public KeyCode backward {get; set;}
	public KeyCode left {get; set;}
	public KeyCode right {get; set;}
	public KeyCode turnup {get; set;}
	public KeyCode turndown {get; set;}
	public KeyCode turnleft {get; set;}
	public KeyCode turnright {get; set;}
	public KeyCode item1 {get; set;}
	public KeyCode item2 {get; set;}
	public KeyCode item3 {get; set;}
	public KeyCode item4 {get; set;}
	public KeyCode item5 {get; set;}
	public KeyCode item6 {get; set;}
	public KeyCode item7 {get; set;}
	public KeyCode item8 {get; set;}
	public KeyCode item9 {get; set;}
	public KeyCode item10 {get; set;}
	public KeyCode pickupdrop {get; set;}
	public KeyCode shoot {get; set;}
	public KeyCode pause {get; set;}
	public KeyCode zoomin {get; set;}
	public KeyCode zoomout {get; set;}



	void Awake()
	{
		//Singleton pattern
		if(GM == null)
		{
			DontDestroyOnLoad(gameObject);
			GM = this;
		}
		else if(GM != this)
		{
			Destroy(gameObject);
		}
		/*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game,
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
		forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
		backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
		left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		turnup = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tUpKey", "F"));
		turndown = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tDownKey", "G"));
		turnleft = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tLeftKey", ""));
		turnright = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("tRightKey", ""));
		item1 = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("item1Key", ""));

	}

	void Start ()
	{

	}

	void Update ()
	{

	}
}