// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStates;

/// <summary>
/// NewGame is a MonoBehavior that controls the New Game menu and has methods 
/// that are called when the menu's buttons are pressed. 
/// </summary>
public class NewGame : MonoBehaviour
{
    //initilized in editor
    public Toggle singlePlayer;
    public Toggle cooperative;
    public Toggle competative;
    public Toggle campaign;
    public Toggle practice;
    public Toggle survival;
    public Toggle easy;
    public Toggle medium;
    public Toggle hard;

	void Start ()
    {
        //remove later once this feature is implimented
        practice.interactable = false;
	}

	void Update ()
    {
        //disable these features if the user is playing a trial verson of the game
		if (User.user.isTrial) 
        {
            cooperative.interactable = false;
            competative.interactable = false;
            practice.interactable = false;
            survival.interactable = false;
            hard.interactable = false;
        }
        //don't disable these featuers if the user is not playign a trial version of the game
        else 
        {
            cooperative.interactable = true;
            competative.interactable = true;
            //practice.interactable = true;
            survival.interactable = true;
            hard.interactable = true;
        }
	}

    /// <summary>
    /// Method called by the Start button, creates a new Level with the settings selected by the user
    /// </summary>
    public void start()
    {
        int numPlayers = 1;
        bool pvp = false;
        float difficulty = 2;

        //set numPlayers and pvp depending on player setting
        if (singlePlayer.isOn) 
        {
            numPlayers = 1;
            pvp = false;
        }
        else if (cooperative.isOn)
        {
            numPlayers = 2;
            pvp = false;
        }
        else if (competative.isOn)
        {
            numPlayers = 2;
            pvp = true;
        }
        else
        {
            throw new System.Exception("No player setting was selected when trying to start a new game.");
        }

        //set difficulty depending on difficulty setting
        if (easy.isOn)
        {
            difficulty = 1;
        }
        else if (medium.isOn)
        {
            difficulty = 2;
        }
        else if (hard.isOn)
        {
            difficulty = 3;
        }
        else
        {
            throw new System.Exception("No difficulty setting was selected when trying to start a new game.");
        }

        //if campaign is selected, Create Level1 of the campain
        if (campaign.isOn) 
        {
            Level level1 = Level.getLevel(1);
            level1.create(numPlayers, difficulty, (int)System.DateTime.Now.Ticks, pvp);
        }
        else if (practice.isOn)
        {
            //to impliment later
        }
        //if survival is selected, create LevelSurvival
        else if (survival.isOn)
        {
            Level levelSurvival = (Instantiate(Resources.Load(Level.LEVEL_PATH + "LevelSurvivalPF"))
                as GameObject).GetComponent<Level>();
            levelSurvival.create(numPlayers, difficulty, (int)System.DateTime.Now.Ticks, pvp);
        }
        else
        {
            throw new System.Exception("No game type was selected when trying to start a new game.");
        }

        gameState = GameState.Playing;
    }

    /// <summary>
    /// method the back button calls, changes the screen to the Main menu
    /// </summary>
    public void back()
    {
        gameState = GameState.Main;
    }
}
