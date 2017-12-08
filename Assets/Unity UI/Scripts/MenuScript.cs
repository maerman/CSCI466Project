//Unity created file

using UnityEngine;

using System.Collections;

using UnityEngine.UI;



public class MenuScript : MonoBehaviour {



	Transform menuPanel;

	Event keyEvent;

	Text buttonText;

	KeyCode newKey;



	bool waitingForKey;





	void Start ()

	{

		//Assign menuPanel to the Panel object in our Canvas

		//Make sure it's not active when the game starts

		menuPanel = transform.FindChild("Panel");

		menuPanel.gameObject.SetActive(true);

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

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.forward.ToString();

			else if(menuPanel.GetChild(i).name == "BackwardKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.backward.ToString();

			else if(menuPanel.GetChild(i).name == "LeftKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();

			else if(menuPanel.GetChild(i).name == "RightKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();

			else if(menuPanel.GetChild(i).name == "tUpKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.turnup.ToString();
			
			else if(menuPanel.GetChild(i).name == "tDownKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.turndown.ToString();

			else if(menuPanel.GetChild(i).name == "tLeftKey")

				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.turnleft.ToString();

		}

	}





	void Update ()

	{

		//Escape key will open or close the panel

		if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)

			menuPanel.gameObject.SetActive(true);

		else if(Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)

			menuPanel.gameObject.SetActive(false);

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

			GameManager.GM.forward = newKey; //Set forward to new keycode

			buttonText.text = GameManager.GM.forward.ToString(); //Set button text to new key

			PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString()); //save new key to PlayerPrefs

			break;

		case "backward":

			GameManager.GM.backward = newKey; //set backward to new keycode

			buttonText.text = GameManager.GM.backward.ToString(); //set button text to new key

			PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString()); //save new key to PlayerPrefs

			break;

		case "left":

			GameManager.GM.left = newKey; //set left to new keycode

			buttonText.text = GameManager.GM.left.ToString(); //set button text to new key

			PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString()); //save new key to playerprefs

			break;

		case "right":

			GameManager.GM.right = newKey; //set right to new keycode

			buttonText.text = GameManager.GM.right.ToString(); //set button text to new key

			PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString()); //save new key to playerprefs

			break;

		case "turnup":

			GameManager.GM.turnup = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.turnup.ToString(); //set button text to new key

			PlayerPrefs.SetString("tUpKey", GameManager.GM.turnup.ToString()); //save new key to playerprefs

			break;

		case "turndown":

			GameManager.GM.turndown = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.turndown.ToString(); //set button text to new key

			PlayerPrefs.SetString("tDownKey", GameManager.GM.turndown.ToString()); //save new key to playerprefs

			break;

		case "turnleft":

			GameManager.GM.turnleft = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.turnleft.ToString(); //set button text to new key

			PlayerPrefs.SetString("tLeftKey", GameManager.GM.turndown.ToString()); //save new key to playerprefs

			break;

		case "turnright":

			GameManager.GM.turnright = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.turnright.ToString(); //set button text to new key

			PlayerPrefs.SetString("tRightKey", GameManager.GM.turndown.ToString()); //save new key to playerprefs

			break;

		case "item1":

			GameManager.GM.item1 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item1.ToString(); //set button text to new key

			PlayerPrefs.SetString("item1Key", GameManager.GM.item1.ToString()); //save new key to playerprefs

			break;

		case "item2":

			GameManager.GM.item2 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item2.ToString(); //set button text to new key

			PlayerPrefs.SetString("item2Key", GameManager.GM.item2.ToString()); //save new key to playerprefs

			break;

		case "item3":

			GameManager.GM.item3 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item3.ToString(); //set button text to new key

			PlayerPrefs.SetString("item3Key", GameManager.GM.item3.ToString()); //save new key to playerprefs

			break;

		case "item4":

			GameManager.GM.item4 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item4.ToString(); //set button text to new key

			PlayerPrefs.SetString("item4Key", GameManager.GM.item4.ToString()); //save new key to playerprefs

			break;

		case "item5":

			GameManager.GM.item5 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item5.ToString(); //set button text to new key

			PlayerPrefs.SetString("item5Key", GameManager.GM.item5.ToString()); //save new key to playerprefs

			break;

		case "item6":

			GameManager.GM.item6 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item6.ToString(); //set button text to new key

			PlayerPrefs.SetString("item6Key", GameManager.GM.item6.ToString()); //save new key to playerprefs

			break;

		case "item7":

			GameManager.GM.item7 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item7.ToString(); //set button text to new key

			PlayerPrefs.SetString("item7Key", GameManager.GM.item7.ToString()); //save new key to playerprefs

			break;

		case "item8":

			GameManager.GM.item8 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item8.ToString(); //set button text to new key

			PlayerPrefs.SetString("item1Key", GameManager.GM.item8.ToString()); //save new key to playerprefs

			break;

		case "item9":

			GameManager.GM.item9 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item9.ToString(); //set button text to new key

			PlayerPrefs.SetString("item9Key", GameManager.GM.item9.ToString()); //save new key to playerprefs

			break;

		case "item10":

			GameManager.GM.item10 = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.item10.ToString(); //set button text to new key

			PlayerPrefs.SetString("item10Key", GameManager.GM.item10.ToString()); //save new key to playerprefs

			break;

		case "pickupdrop":

			GameManager.GM.pickupdrop = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.pickupdrop.ToString(); //set button text to new key

			PlayerPrefs.SetString("PickupDropKey", GameManager.GM.pickupdrop.ToString()); //save new key to playerprefs

			break;

		case "shoot":

			GameManager.GM.shoot = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.shoot.ToString(); //set button text to new key

			PlayerPrefs.SetString("shootKey", GameManager.GM.shoot.ToString()); //save new key to playerprefs

			break;

		case "pause":

			GameManager.GM.pause = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.pause.ToString(); //set button text to new key

			PlayerPrefs.SetString("pauseKey", GameManager.GM.pause.ToString()); //save new key to playerprefs

			break;

		case "zoomin":

			GameManager.GM.zoomin = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.zoomin.ToString(); //set button text to new key

			PlayerPrefs.SetString("zoomInKey", GameManager.GM.zoomin.ToString()); //save new key to playerprefs

			break;

		case "zoomout":

			GameManager.GM.zoomout = newKey; //set jump to new keycode

			buttonText.text = GameManager.GM.zoomout.ToString(); //set button text to new key

			PlayerPrefs.SetString("zoomOutKey", GameManager.GM.zoomout.ToString()); //save new key to playerprefs

			break;
		}



		yield return null;

	}

}