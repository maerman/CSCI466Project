// written by: Shane Barry, Thomas Stewart, Metin Erman
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// GameOver is a MonoBehavior that controls the Game Complete and Game Over menus
/// has methods that are called when the menus' buttons are pressed. The Game Complete menu
/// doesn't use the Restart() method.
/// </summary>
public class GameOver : MonoBehaviour, IErrorPanel
{
    //initilized in editor
    public InputField replayName;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    /// <summary>
    /// Method called by the Restart button on the gameover menu to set level to its beginning
    /// </summary>
    public void restart() 
    {
        //if there is not a current Level, display error message
        if (Level.current == null) 
        {
            showErrorMenu("Can't restart level, current level is set to null");
        }
        else
        {
            Level.current.restartLevel();
            GameStates.gameState = GameStates.GameState.Playing;
        }
	}

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
        throw new NotImplementedException();
    }

    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
