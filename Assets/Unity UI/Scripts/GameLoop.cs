// written by: Metin Erman, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Metin Erman

using System.Collections;
using System.Collections.Generic;
using static GameStates;
using UnityEngine;
using System;

/// <summary>
/// GameLoop is a MonoBehaviour that controls the state the of game. It switches the screen
/// the program is on depending on enumerator GameState. So, other classes can simply change the 
/// GameState to switch between screens.
/// </summary>
public class GameLoop : MonoBehaviour
{
    private static GameLoop gameLoop;
    private GameState lastGameState;
    
    //initilzed in editor
    public GameObject loginMenu;
    public GameObject createAccountMenu;
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject loadGameMenu;
    public GameObject ingameInterface;
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;
    public GameObject gameOverMenu;
    public GameObject gameCompleteMenu;
    public GameObject loadReplayMenu;
    public GameObject optionsHubMenu;
    public GameObject optionsGameMenu;
    public GameObject optionsAudVidMenu;
    public GameObject optionsPlayer1Menu;
    public GameObject optionsPlayer2Menu;
    public GameObject aboutMenu;

    //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
    private void Awake() 
    {
        if (gameLoop == null)
        {
            gameLoop = this;
            DontDestroyOnLoad(gameLoop);
        }
        else
        {
            Destroy(this); //destroy if another one is attempted to be created.
        }
    }

    // Use this for initialization
    //start the state machine
    void Start()
    {
       
        StartCoroutine("StateMachine"); //Start the GameLoop
    }

    IEnumerator StateMachine()
    {
        gameState = GameState.LoggingIn; //initial game state
        previousGameState = GameState.LoggingIn;
        lastGameState = GameState.Exit;

        //keep repeting this loop to switch between screens and control the program until GameState
        //is set to Exit, which will cause the program to be closed
        while (gameState != GameState.Exit)
        {
            try
            {
                //if the gamestate has changed disable all menus then in the switch below,
                //only enable the one that it is set to
                if (gameState != lastGameState)
                {
                    loginMenu.SetActive(false);
                    createAccountMenu.SetActive(false);
                    mainMenu.SetActive(false);
                    newGameMenu.SetActive(false);
                    loadGameMenu.SetActive(false);
                    ingameInterface.SetActive(false);
                    levelCompleteMenu.SetActive(false);
                    pauseMenu.SetActive(false);
                    gameOverMenu.SetActive(false);
                    gameCompleteMenu.SetActive(false);
                    loadReplayMenu.SetActive(false);
                    optionsHubMenu.SetActive(false);
                    optionsGameMenu.SetActive(false);
                    optionsAudVidMenu.SetActive(false);
                    optionsPlayer1Menu.SetActive(false);
                    optionsPlayer2Menu.SetActive(false);
                    aboutMenu.SetActive(false);
                    previousGameState = lastGameState;
                }
                lastGameState = gameState;

                //set to 0, then set to 1 only when playing
                Time.timeScale = 0;

                //enable the menu that the screen is currently set to and 
                //set the timeScale to 1 if the Level is being played or replayed
                switch (gameState)
                {
                    case GameState.LoggingIn:
                        loginMenu.SetActive(true);
                        break;
                    case GameState.CreateAccount:
                        createAccountMenu.SetActive(true);
                        break;
                    case GameState.Main:
                        mainMenu.SetActive(true);
                        break;
                    case GameState.NewGame:
                        newGameMenu.SetActive(true);
                        break;
                    case GameState.LoadGame:
                        loadGameMenu.SetActive(true);
                        break;
                    case GameState.Playing:
                        loginMenu.SetActive(false);
                        ingameInterface.SetActive(true);
                        Time.timeScale = 1;

                        //pause the game if the pause key is pressed by the user
                        foreach (PlayerControls item in Controls.get().players)
                        {
                            if (item.Pause)
                            {
                                gameState = GameState.Paused;
                                break;
                            }
                        }

                        break;
                    case GameState.Paused:
                        ingameInterface.SetActive(true);
                        pauseMenu.SetActive(true);

                        //unpause the game and set the gameState to its prevous one
                        //(Paying or Replay) if the pause key is pressed by the user
                        foreach (PlayerControls item in Controls.get().players)
                        {
                            if (item.Pause)
                            {
                                gameState = previousGameState;
                                break;
                            }
                        }
                        break;
                    case GameState.LevelComplete:
                        levelCompleteMenu.SetActive(true);
                        break;
                    case GameState.LostGame:
                        gameOverMenu.SetActive(true);
                        break;
                    case GameState.WonGame:
                        gameCompleteMenu.SetActive(true);
                        break;
                    case GameState.LoadReplay:
                        loadReplayMenu.SetActive(true);
                        break;
                    case GameState.Replay:
                        ingameInterface.SetActive(true);
                        Time.timeScale = 1;

                        //pause the game if the pause key is pressed by the user
                        foreach (PlayerControls item in Controls.get().players)
                        {
                            if (item.Pause)
                            {
                                gameState = GameState.Paused;
                                break;
                            }
                        }
                        break;
                    case GameState.OptionsHub:
                        optionsHubMenu.SetActive(true);
                        break;
                    case GameState.OptionsGame:
                        optionsGameMenu.SetActive(true);
                        break;
                    case GameState.OptionsAudVid:
                        optionsAudVidMenu.SetActive(true);
                        break;
                    case GameState.OptionsPlayer1:
                        optionsPlayer1Menu.SetActive(true);
                        break;
                    case GameState.OptionsPlayer2:
                        optionsPlayer2Menu.SetActive(true);
                        break;
                    case GameState.About:
                        aboutMenu.SetActive(true);
                        break;
                    case GameState.Exit:
                        //loop should break automatically next iteration
                        break;
                    default:
                        throw new Exception("Invalid GameState: " + gameState.ToString());
                }
            }
            catch (Exception e)
            {
                throw new Exception("There was an error in the GameState Loop! StackTrace: " + e.StackTrace +
                    " Message: " + e.Message);
            }
            yield return null; //return null to allow other things to continue
        }

        //save Controls and Options settings to their default files
        Controls.get().saveControls();
        Options.get().saveOptions();

        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}
        

