using UnityEngine;
using System.Collections;
using static GameStates;

public class OptionsHub : MonoBehaviour
{
    private GameState callingScreen;

    private void OnEnable()
    {
        if (previousGameState == GameState.Main ||
            previousGameState == GameState.Paused)
        {
            callingScreen = previousGameState;
        }
    }

    public void gameOptions()
    {
        gameState = GameState.OptionsGame;
    }

    public void controls1()
    {
        gameState = GameState.OptionsPlayer1;
    }

    public void controls2()
    {
        gameState = GameState.OptionsPlayer2;
    }

    public void audioVideo()
    {
        gameState = GameState.OptionsAudVid;
    }

    public void back()
    {
        gameState = callingScreen;
    }
}
