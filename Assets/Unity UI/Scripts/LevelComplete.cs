using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static CRUD;
using static UserData;

public class LevelComplete : MonoBehaviour, IErrorPanel //add the error panel interface to dislpay error messages and implement it's methods
{
    public UnityEngine.UI.InputField saveNameInputField; //initilzied in editor
    public UnityEngine.UI.InputField replayNameInputField; //initilized in editor
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

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
        //if there is an error with the current level then throw an exception and an error message is displayed
        if(Level.currentLevel == null)  showErrorMenu("CurrentLevel is null when trying to go to next Level");
        //if next level cannot be loaded then throw an exception and display an error message
        if (Level.currentLevel.nextLevel() == null) showErrorMenu("Problem loading next Level");
            
        GameStates.gameState = GameStates.GameState.Playing;
        
    }

    public void saveGame() // method used by the level complete menu to take user input from input field and create save file on user's device
    {
        if (Level.currentLevel == null) showErrorMenu("CurrentLevel is null when trying to save"); //if there is an error with the current level then throw an exception and an error message is displayed

        if (saveNameInputField == null || saveNameInputField.text == null)
        {
            showErrorMenu("Problem with SaveName InputField"); //if there is an error with the save file name then throw an exception and display an error message
        }
        else if (saveNameInputField.text == "")
        {
            showErrorMenu("You must enter a name to create a save game"); //if user does not enter name in input field display message display error that need a name to save
        }
        else //else create save file on user's hardrive using input field text
        {
            if (Level.currentLevel.save(saveNameInputField.text))
            {
                showErrorMenu("Save game created successfully!");
            }
            else
            {
                showErrorMenu("There was an error creating the save game...");
            }

        }
    }

    /* Not using, saving to leadboard automatically in Level.cs when the Level is completed. 
    public void addToLeaderboard() // method used by the level complete menu to take user input from input field and create recording file on user's device
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            showErrorMenu("CurrentLevel is null when trying to go to addToLeaderboard");
        }
        else if (addedToLeaderboard) //if score is already on the database
        {
            showErrorMenu("CurrentLevel was already added to the leaderboard");
        }
        else //else save the score to the database
        {
            userData.currentLevel = Level.currentLevel.levelNumber;
            userData.timeAlive = (int)Level.currentLevel.duration.TotalMilliseconds; //convert from double to int
            userData.difficulty = Level.currentLevel.difficulty;
            
            //userData.enemiesKilled = Level.currentLevel.
           

            //add to leaderboard
            crud.SaveUserData(); //save it to the backend DB...all of the data should already be stored in the userData class object.
            addedToLeaderboard = true;
        }
    }
    */

    public void saveReplay() // method used by the level complete menu to create a save file
    {
        if (Level.currentLevel == null) //if there is an error with the current level then throw an exception and an error message is displayed
        {
            showErrorMenu("CurrentLevel is null when trying to saveReplay");
        }
        else if (replayNameInputField == null || replayNameInputField.text == null) //if there is an error with the replay file name then throw an exception and display an error message
        {
            showErrorMenu("Problem with replayName InputField");
        }
        else if (replayNameInputField.text == "") //if user does not enter name in input field display message
        {
            showErrorMenu("You must enter a name to save a replay.");
        }
        else //else create replay recording on user's hardrive using the input field text
        {
            if (Level.currentLevel.saveReplay(replayNameInputField.text))
            {
                showErrorMenu("Replay created successfully!");
            }
            else
            {
                showErrorMenu("There was an error creating the replay...");
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

    public bool HasError()
    {
        Boolean hasError = false;


        return hasError;
    }

    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}