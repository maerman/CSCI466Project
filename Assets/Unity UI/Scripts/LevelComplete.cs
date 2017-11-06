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

    public void conntiue() // method used by the level complete menu to load the gamestate for next level
    {
        if (Level.currentLevel == null) /if there is an error with the current level then throw an exception and an error message is displayed
        {
            throw new System.Exception("CurrentLevel is null when trying to go to next Level");
        }
        else //else call the gamestate to set it to the next level
        {
            if (Level.currentLevel.nextLevel() == null) //if next level cannot be loaded then throw an exception and display an error message
            {
            {
                throw new System.Exception("Problem loading next Level");
            }
            else //else set gamestate to the next level
            {
                GameStates.gameState = GameStates.GameState.Playing;
            }
        }
    }

    public void saveGame() // method used by the level complete menu to take user input from input field and create save file on user's device
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            throw new System.Exception("CurrentLevel is null when trying to save");
        }
        else if (saveNameInputField == null || saveNameInputField.text == null) //if there is an error with the save file name then throw an exception and display an error message
        {
            throw new System.Exception("Problem with SaveName InputField");
        }
        else if (saveNameInputField.text == "") //if user does not enter name in input field display message
        {
            //display error that need a name to save
        }
        else //else create save file on user's hardrive using input field text
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

    public void addToLeaderboard() // method used by the level complete menu to take user input from input field and create recording file on user's device
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            throw new System.Exception("CurrentLevel is null when trying to go to addToLeaderboard");
        }
        else if (addedToLeaderboard) //if score is already on the database
        {
            //display error that it was already added to leaderbaord
        }
        else //else save the score to the database
        {
            //add to leaderboard
            addedToLeaderboard = true;
        }
    }

    public void saveReplay() // method used by the level complete menu to create a save file
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            throw new System.Exception("CurrentLevel is null when trying to saveReplay");
        }
        else if (replayNameInputField == null || replayNameInputField.text == null) //if there is an error with the replay file name then throw an exception and display an error message
        {
            throw new System.Exception("Problem with replayName InputField");
        }
        else if (replayNameInputField.text == "") //if user does not enter name in input field display message
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

    public void quit() // method used by the level complete menu to set gamestate to main menu
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        GameStates.gameState = GameStates.GameState.Main; //set gamestate to main menu
    }
}