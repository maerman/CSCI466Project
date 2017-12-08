// written by: Shane Barry, Thomas Stewart, Metin Erman
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static CRUD;
using static UserData;

/// <summary>
/// LevelComplete is a MonoBehavior that controls the Level Complete menu and
/// has methods that are called when the menus' buttons are pressed.
/// </summary>
public class LevelComplete : MonoBehaviour, IErrorPanel //add the error panel interface to dislpay error messages and implement it's methods
{
    public UnityEngine.UI.InputField saveNameInputField; //initilzied in editor
    public UnityEngine.UI.InputField replayName; //initilized in editor
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

    /// <summary>
    /// Method called by the Continue button, creates the next Level and sets the screen to Playing
    /// </summary>
    public void conntiue()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            showErrorMenu("CurrentLevel is null when trying to go to next Level");
        //if there is a problem loading the next Level, display an error
        if (Level.current.nextLevel() == null)
            showErrorMenu("Problem loading next Level");
            
        GameStates.gameState = GameStates.GameState.Playing;
        
    }

    /// <summary>
    /// Method called by the Save Game button, saves the information about the current Level so 
    /// it can be loaded later to bring the user back to the same point in the game
    /// </summary>
    public void saveGame()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            showErrorMenu("CurrentLevel is null when trying to save");
        //if there is an error with the replay file name then throw an exception
        else if (saveNameInputField == null || saveNameInputField.text == null)
            throw new Exception("Problem with SaveName InputField");
        //if user does not enter name in input field then desplay a message to him
        else if (saveNameInputField.text == "")
            showErrorMenu("You must enter a name to create a save game"); 
        //create a save
        else
        {
            if (Level.current.save(saveNameInputField.text))
                showErrorMenu("Save game created successfully!");
            else
                showErrorMenu("There was an error creating the save game...");
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

    /// <summary>
    /// Method called by the Save Replay button, saves a replay of the current Level
    /// to a file of the replayName.
    /// </summary>
    public void saveReplay()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            showErrorMenu("CurrentLevel is null when trying to saveReplay");
        //if there is an error with the replay file name then throw an exception
        else if (replayName == null || replayName.text == null)
            throw new Exception("Problem with replayName InputField");
        //if user does not enter name in input field then desplay a message to him
        else if (replayName.text == "")
            showErrorMenu("You need to enter a name before saving the replay");
        //else create save the replay
        else
        {
            if (Level.current.saveReplay(replayName.text))
                showErrorMenu("Replay has been saved successfully!");
            else
                showErrorMenu("There was a problem saving the replay!");
        }
    }

    /// <summary>
    /// Method called by the Quit button, destroys the current Level and changes the screen to the Main menu 
    /// </summary>
    public void quit()
    {
        if (Level.current != null)
        {
            Destroy(Level.current.gameObject);
        }
        GameStates.gameState = GameStates.GameState.Main;
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