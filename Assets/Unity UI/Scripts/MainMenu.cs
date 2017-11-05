using UnityEngine;
using System.Collections;
using static GameStates;

public class MainMenu : MonoBehaviour
{
    //initilized in editor
    public UnityEngine.UI.Button loadGame;
    public UnityEngine.UI.Button watchReplay;

    void Start()
    {

    }

    void Update()
    {
        if (GameStates.isDemo)
        {
            loadGame.interactable = false;
            watchReplay.interactable = false;
        }
        else
        {
            loadGame.interactable = true;
            watchReplay.interactable = true;
        }
    }

    public void NewGame()
    {
        gameState = GameState.NewGame;
    }

    public void LoadGame()
    {
        gameState = GameState.LoadGame;
    }

    public void WatchReplay()
    {
        gameState = GameState.LoadReplay;
    }

    public void Leaderboard()
    {
        //open website
    }

    public void Options()
    {
        gameState = GameState.Options;
    }

    public void About()
    {
        gameState = GameState.About;
    }

    public void Quit()
    {
        gameState = GameState.Exit;
    }
}
