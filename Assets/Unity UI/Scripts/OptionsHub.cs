// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using static GameStates;

/// <summary>
/// OptionsHub is a MonoBehavior that controls the Options Hub menu and has methods 
/// that are called when the menu's buttons are pressed. 
/// </summary>
public class OptionsHub : MonoBehaviour
{
    private GameState callingScreen;

    private void OnEnable()
    {
        //Find what Menu opened this and save which did so Back knows where to go back to
        if (previousGameState == GameState.Main ||
            previousGameState == GameState.Paused)
        {
            callingScreen = previousGameState;
        }
    }

    /// <summary>
    /// Method the Game Options button calls, change the screen to the Optons Game screen
    /// </summary>
    public void gameOptions()
    {
        gameState = GameState.OptionsGame;
    }

    /// <summary>
    /// Method the Player 1 Controls button calls, changes the screen to the Controls Player 1 screen
    /// </summary>
    public void controls1()
    {
        gameState = GameState.OptionsPlayer1;
    }

    /// <summary>
    /// Method the Player 2 Controls button calls, changes the screen to the Controls Player 2 screen
    /// </summary>
    public void controls2()
    {
        gameState = GameState.OptionsPlayer2;
    }

    /// <summary>
    /// Method the Audio/Video button calls, changes the screen to the Audio screen
    /// </summary>
    public void audioVideo()
    {
        gameState = GameState.OptionsAudVid;
    }

    /// <summary>
    /// Method the Back button calls, changes the screen to the screen that opened this one
    /// </summary>
    public void back()
    {
        gameState = callingScreen;
    }
}
