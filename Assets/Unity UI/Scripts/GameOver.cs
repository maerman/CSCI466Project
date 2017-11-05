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

	public void restart()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("Can't restart level, current level is set to null");
        }
        else
        {
            Level.currentLevel.restartLevel();
            GameStates.gameState = GameStates.GameState.Playing;
        }
	}

    public void saveReplay()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("CurrentLevel is null when trying to saveReplay");
        }
        else if (replayNameInputField == null || replayNameInputField.text == null)
        {
            throw new System.Exception("Problem with replayName InputField");
        }
        else if (replayNameInputField.text == "")
        {
            //display error that need a name to save replay
        }
        else
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

    public void quit()
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        GameStates.gameState = GameStates.GameState.Main;
	}
}
