using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static GameStates;

public class PauseGame : MonoBehaviour, IErrorPanel {

    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    private GameState callingScreen;

    private void OnEnable()
    {
        if (previousGameState == GameState.Replay ||
            previousGameState == GameState.Playing)
        {
            callingScreen = previousGameState;
        }
    }

    public void Unpause() // method used by the resume button on the pause menu to dismiss the menu and continue playing
    {
        gameState = callingScreen; //set gamestate to last gamestate, which removes pause interface
	}

	public void Restart() // method used by the restart button on the pause menu to set level to its beginning
    {
        if (Level.current == null) //if there is an error with the current level, throw exception and display error message
        {
            showErrorMenu("Current Level is null, can't restart");
        }
        else //else set level to the beginning state and call the unpause method to remove the interface 
        {
            Level.current.restartLevel();
            Unpause();
        }
	}

	public void Options() // method used by the pause menu to display the options sub-menu
    {
        gameState = GameState.OptionsHub; //set gamestate to options to display options interface
	}

	public void Exit() // method used by the pause menu to set the gamestate to the main menu on exit button click
    {
        if (Level.current != null)
        {
            Destroy(Level.current.gameObject);
        }
        gameState = GameState.Main; // sets gamestate back to main menu
    }

    public bool HasError()
    {
        throw new NotImplementedException();
    }

    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
