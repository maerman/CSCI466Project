using System.Collections;
using System.Collections.Generic;
using static GameStates;
using UnityEngine;
using System;

public class GameLoop : MonoBehaviour {
    private static GameLoop gameLoop;
    private GameState lastGameState;

    //initilzed in editor
    public GameObject loginMenu; 
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;

    bool inGame = true; 

    private void Awake() //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
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
        lastGameState = GameState.LoggingIn;

        while (gameState != GameState.Exit)
        {
            if (gameState != lastGameState)
            {
                previousGameState = lastGameState;
            }
            lastGameState = gameState;

            try
            {
                //set to 0, then set to 1 only when playing
                Time.timeScale = 0;

                //set them all to false, then in the switch, set only the correct one to true
                loginMenu.SetActive(false);
                //createAccountMenu.SetActive(false);
                //mainMenu.SetActive(false);
                //newGameMenu.SetActive(false);
                //loadGameMenu.SetActive(false);
                levelCompleteMenu.SetActive(false);
                pauseMenu.SetActive(false);
                //lostGameMenu.SetActive(false);
                //wonGameMenu.SetActive(false);
                //loadReplayMenu.SetActive(false);
                //optionsMenu.SetActive(false);
                //aboutMenu.SetActive(false);

                switch (gameState)
                {
                    case GameState.LoggingIn:
                        loginMenu.SetActive(true);
                        break;
                    case GameState.CreatAccount:
                        //createAccountMenu.SetActive(true);
                        break;
                    case GameState.Main:
                        //mainMenu.SetActive(true);
                        break;
                    case GameState.NewGame:
                        //newGameMenu.SetActive(true);
                        break;
                    case GameState.LoadGame:
                        //loadGameMenu.SetActive(true);
                        break;
                    case GameState.Playing:
                        Time.timeScale = 1;
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
                        pauseMenu.SetActive(true);
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
                        //lostGameMenu.SetActive(true);
                        break;
                    case GameState.WonGame:
                        //wonGameMenu.SetActive(true);
                        break;
                    case GameState.LoadReplay:
                        //loadReplayMenu.SetActive(true);
                        break;
                    case GameState.Replay:
                        Time.timeScale = 1;
                        foreach (PlayerControls item in Controls.get().players)
                        {
                            if (item.Pause)
                            {
                                gameState = GameState.Paused;
                                break;
                            }
                        }
                        break;
                    case GameState.Options:
                        //optionsMenu.SetActive(true);
                        break;
                    case GameState.About:
                        //aboutMenu.SetActive(true);
                        break;
                    case GameState.Exit:
                        Application.Quit();
                        break;
                    default:
                        throw new Exception("Invalid GameState: " + gameState.ToString());
                        break;
                }

            }
            catch (Exception e)
            {
                throw new Exception("There was an error in the GameState Loop! StackTrace: " + e.StackTrace +
                    " Message: " + e.Message);
            }
            yield return null; //return null to allow other things to continue
        }
        Application.Quit();
    }
}
        

