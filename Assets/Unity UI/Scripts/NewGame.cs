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
		if (GameStates.isDemo)
        {
            cooperative.interactable = false;
            competative.interactable = false;
            practice.interactable = false;
            survival.interactable = false;
            hard.interactable = false;
        }
        else
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

        if (singlePlayer.isOn)
        {
            players = 1;
            pvp = false;
        }
        else if (cooperative.isOn)
        {
            players = 2;
            pvp = false;
        }
        else if (competative.isOn)
        {
            players = 2;
            pvp = false;
        }
        else
        {
            throw new System.Exception("No player setting was selected when trying to start a new game.");
        }

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

        if (campaign.isOn)
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

    public void back()
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
