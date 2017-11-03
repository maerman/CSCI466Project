using UnityEngine;
using System.Collections;
using static GameStates;

public class MainMenu : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

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
