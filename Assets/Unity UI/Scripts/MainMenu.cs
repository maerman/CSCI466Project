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
        if (GameStates.isDemo) //determines if the user is playing without an account i.e. playing the demo version
        {
            loadGame.interactable = false; //if they are then these features are disabled for them
            watchReplay.interactable = false;
        }
        else //else they can use these features on the main menu
        {
            loadGame.interactable = true;
            watchReplay.interactable = true;
        }
    }

    public void NewGame() //method used by the main menu to start a start a gamestate
    {
        gameState = GameState.NewGame;
    }

    public void LoadGame() //method used by the main menu to load a gamestate from a save file
    {
        gameState = GameState.LoadGame;
    }

    public void WatchReplay() //method used by the main menu to watch a level replay from a save file
    {
        gameState = GameState.LoadReplay;
    }

    public void Leaderboard()
    {
        //open website
    }

    public void Options() //method used by the main menu to open the options menu
    {
        gameState = GameState.Options;
    }

    public void About() //method used by the main menu to open the about page
    {
        gameState = GameState.About;
    }

    public void Quit() //method used by the main menu to close the program
    {
        gameState = GameState.Exit;
    }
}
