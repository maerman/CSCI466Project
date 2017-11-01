using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public UnityEngine.UI.InputField saveNameInputField; //initilzied in editor
    public UnityEngine.UI.InputField replayNameInputField; //initilized in editor
    private bool addedToLeaderboard = false;

	void Start ()
    {
        
	}
	
	void Update ()
    {
        //show current level number, Level.currentLevel.levelNumber
        //show level duration, Level.currentLevel.duration
        //show level difficulty, Level.currentLevel.difficulty
    }

    public void conntiue()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("CurrentLevel is null when trying to go to next Level");
        }
        else
        {
            if (Level.currentLevel.nextLevel() == null)
            {
                throw new System.Exception("Problem loading next Level");
            }
            else
            {
                GameStates.gameState = GameStates.GameState.Playing;
            }
        }
    }

    public void saveGame()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("CurrentLevel is null when trying to save");
        }
        else if (saveNameInputField == null || saveNameInputField.text == null)
        {
            throw new System.Exception("Problem with SaveName InputField");
        }
        else if (saveNameInputField.text == "")
        {
            //display error that need a name to save
        }
        else 
        {
            if (Level.currentLevel.save(saveNameInputField.text)) 
            {
                //display that game was saved successfuly
            }
            else
            {
                //display that there was a problem saving the game
            }

        }
    }

    public void addToLeaderboard()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("CurrentLevel is null when trying to go to addToLeaderboard");
        }
        else if (addedToLeaderboard)
        {
            //display error that it was already added to leaderbaord
        }
        else
        {
            //add to leaderboard
            addedToLeaderboard = true;
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
        GameStates.gameState = GameStates.GameState.Main;
    }
}