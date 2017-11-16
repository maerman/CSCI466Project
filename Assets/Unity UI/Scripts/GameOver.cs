
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOver : MonoBehaviour, IErrorPanel
{
    public InputField replayNameInputField; //initilized in editor
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    public void restart() // method used by the restart button on the gameover menu to set level to its beginning
    {
        if (Level.currentLevel == null) //if there is an error with the current level, throw exception and display error message
        {
            showErrorMenu("Can't restart level, current level is set to null");
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
            showErrorMenu("CurrentLevel is null when trying to saveReplay");
        }
        else if (replayNameInputField == null || replayNameInputField.text == null) //if there is an error with the replay file name then throw an exception and display an error message
        {
            showErrorMenu("Problem with replayName InputField");
        }
        else if (replayNameInputField.text == "") //if user does not enter name in input field
        {
            showErrorMenu("You need to enter a name before saving the replay"); //display error that need a name to save replay
        }
        else //else create replay recording on user's hardrive using the input field text
        {
            if (Level.currentLevel.saveReplay(replayNameInputField.text))
            {
                showErrorMenu("Replay has been saved successfully!"); //display that replay was saved
            }
            else
            {
                showErrorMenu("There was a problem saving the replay!"); //display that there was a problem saving the replay
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
