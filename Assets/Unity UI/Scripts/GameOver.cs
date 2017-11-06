using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public UnityEngine.UI.InputField replayNameInputField; //initilized in editor

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

	public void restart() // method used by the restart button on the gameover menu to set level to its beginning
    {
        if (Level.currentLevel == null) //if there is an error with the current level, throw exception and display error message
        {
            throw new System.Exception("Can't restart level, current level is set to null");
        }
        else //else set level to its beginning state
        {
            Level.currentLevel.restartLevel();
            GameStates.gameState = GameStates.GameState.Playing;
        }
	}

    public void saveReplay() // method used by the Gameover menu to create a save file
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            throw new System.Exception("CurrentLevel is null when trying to saveReplay");
        }
        else if (replayNameInputField == null || replayNameInputField.text == null) //if there is an error with the replay file name then throw an exception and display an error message
        {
            throw new System.Exception("Problem with replayName InputField");
        }
        else if (replayNameInputField.text == "") //if user does not enter name in input field
        {
            //display error that need a name to save replay
        }
        else //else create replay recording on user's hardrive using the input field text
        {
            if (Level.currentLevel.saveReplay(replayNameInputField.text))
            {
                //display that replay was saved
            }
            else
            {
                //display that there was a problem saving the replay
            }
        }
    }

    public void quit() // method used by the gameover menu to set gamestate to main menu
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        GameStates.gameState = GameStates.GameState.Main; //set gamestate to main menu
	}
}
