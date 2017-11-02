using System.Collections;
using System.Collections.Generic;
using static GameStates;
using static User;
using static Login;
using UnityEngine;
using System;

public class GameLoop : MonoBehaviour {
    private GameLoop gameLoop;
    //public GameObject Level1;     //you can get the current Level from Level.currentLevel but it might be null if the game is not currently being played
    public Canvas LoginMenu; //initilzed in editor
    public Canvas LevelCompleteMenu;
    public Canvas PauseMenu;
    //public Canvas GameOverMenu;

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
            Destroy(gameLoop); //destroy if another one is attempted to be created.
        }
    }
    // Use this for initialization
    //start the state machine
    void Start() {
       
        StartCoroutine("StateMachine"); //Start the GameLoop
    }

    IEnumerator StateMachine()
    {
        gameState = GameState.Loading; //initial game state
        //Level1.SetActive(false); //disable Level1 Menu
        LoginMenu.enabled = true; //enable the main menu

        playerState = PlayerState.NotLoggedIn; //initial player state

            while (inGame)
            {
            try
            {
                switch (gameState)
                {
                    case (GameState.Loading):
                        break;
                    case (GameState.PlayingDemo):
                        LoginMenu.enabled = false;
                        LevelCompleteMenu.enabled = false;
                        //Level.SetActive(true);
                        break;
                    case GameState.PlayingFull:
                        LoginMenu.enabled = false;
                        LevelCompleteMenu.enabled = false;
                        //Level1.SetActive(true);
                        break;
                    case GameState.LevelComplete:
                        LevelCompleteMenu.enabled = true;                      
                        break;
                    case GameState.Paused:
                        break;
                    case GameState.GameOver:
                        if (playerState == PlayerState.Killed)
                        {

                        }
                        else if (playerState == PlayerState.Victorious)
                        {

                        }
                        else
                        {
                            Debug.Log("gameState is GameOver but the playerState is NOT in an appropriate state! " +
                                "playerState should either be 'Killed or 'Victorious' but is currently '" + playerState.ToString() + "'");
                        }
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
        

