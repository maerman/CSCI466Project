using System.Collections;
using System.Collections.Generic;
using static GameStates;
using UnityEngine;
using System;

public class GameLoop : MonoBehaviour {
    private static GameLoop gameLoop;
    private GameState lastGameState;
    //public GameObject Level1;     //you can get the current Level from Level.currentLevel but it might be null if the game is not currently being played
    public GameObject loginMenu; //initilzed in editor
    public GameObject levelCompleteMenu; //initilized in editor
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

        while (inGame)
        {
            if (gameState != lastGameState)
            {
                previousGameState = lastGameState;
            }

            try
            {
                //set to 0, then set to 1 only when playing
                Time.timeScale = 0;

                //set them all to false, then in the switch, set only the correct one to true
                loginMenu.SetActive(false);
                //createAccountMenu.enabled = false;
                //mainMenu.enabled = false;
                //newGameMenu.enabled = false;
                //loadGameMenu.enabled = false;
                levelCompleteMenu.SetActive(false);
                //pasueGameMenu.enabled = false;
                //lostGameMenu.enabled = false;
                //wonGameMenu.enabled = false;
                //loadReplayMenu.enabled = false;
                //aboutMenu.enabled = false;

                switch (gameState)
                {
                    case GameState.LoggingIn:
                        loginMenu.SetActive(true);
                        break;
                    case GameState.CreatAccount:
                        //createAccountMenu.enabled = true;
                        break;
                    case GameState.Main:
                        //mainMenu.enabled = true;
                        break;
                    case GameState.NewGame:
                        //newGameMenu.enabled = true;
                        break;
                    case GameState.LoadGame:
                        //loadGameMenu.enabled = true;
                        break;
                    case GameState.Playing:
                        Time.timeScale = 1;
                        break;
                    case GameState.Paused:
                        //pasueGameMenu.enabled = true;
                        break;
                    case GameState.LevelComplete:
                        levelCompleteMenu.SetActive(true);
                        break;
                    case GameState.LostGame:
                        //lostGameMenu.enabled = true;
                        break;
                    case GameState.WonGame:
                        //wonGameMenu.enabled = true;
                        break;
                    case GameState.LoadReplay:
                        //loadReplayMenu.enabled = true;
                        break;
                    case GameState.Replay:
                        Time.timeScale = 1;
                        break;
                    case GameState.About:
                        //aboutMenu.enabled = true;
                        break;
                    case GameState.Exit:
                        Application.Quit();
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
    }
}
        

