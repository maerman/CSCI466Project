using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //remove later once these features are implimented
        practice.interactable = false;
        survival.interactable = false;
	}

	void Update ()
    {
		if (GameStates.isDemo) //determines if the user is playing without an account i.e. playing the demo version
        {
            cooperative.interactable = false; //if they are then the following features are disabled for them
            competative.interactable = false;
            practice.interactable = false;
            survival.interactable = false;
            hard.interactable = false;
        }
        else //else they can use these features on the new game menu
        {
            cooperative.interactable = true;
            competative.interactable = true;
            practice.interactable = true;
            survival.interactable = true;
            hard.interactable = true;
        }
	}

    public void start()
    {
        int players = 1;
        bool pvp = false;
        float difficulty = 2;

        if (singlePlayer.isOn) //if singleplayer is toggled, set players to 1
        {
            players = 1;
            pvp = false;
        }
        else if (cooperative.isOn) //if cooperative is toggled, set players to 2
        {
            players = 2;
            pvp = false;
        }
        else if (competative.isOn) //if competitive is toggled, set players to 2
        {
            players = 2;
            pvp = false;
        }
        else
        {
            throw new System.Exception("No player setting was selected when trying to start a new game.");
        }

        if (easy.isOn) //if easy is toggled, set difficulty to 1
        {
            difficulty = 1;
        }
        else if (medium.isOn) //if medium is toggled, set difficulty to 2
        {
            difficulty = 2;
        }
        else if (hard.isOn) //if hard is toggled, set difficulty to 3
        {
            difficulty = 3;
        }
        else
        {
            throw new System.Exception("No difficulty setting was selected when trying to start a new game.");
        }

        if (campaign.isOn) //if campaign is toggled, set level to level 1
        {
            Level level1 = Level.getLevel(1);
            level1.create(players, difficulty, (int)System.DateTime.Now.Ticks, pvp);
        }
        else if (practice.isOn)
        {
            //to impliment later
        }
        else if (survival.isOn)
        {
            //to impliment later
        }
        else
        {
            throw new System.Exception("No game type was selected when trying to start a new game.");
        }

        GameStates.gameState = GameStates.GameState.Playing;
    }

    public void back() //method used by the new game menu to go back to the main menu
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
