using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public bool paused = false; //set pause menu to false so it does not appear by default

    void Start ()
    {
        
	}

	void Update()
    {
        
	}

	public void Unpause() // method used by the resume button on the pause menu to dismiss the menu and continue playing
    {
        GameStates.gameState = GameStates.previousGameState; //set gamestate to last gamestate, which removes pause interface
	}

	public void Restart() // method used by the restart button on the pause menu to set level to its beginning
    {
        if (Level.currentLevel == null) //if there is an error with the current level, throw exception and display error message
        {
            throw new System.Exception("Current Level is null, can't restart");
        }
        else //else set level to the beginning state and call the unpause method to remove the interface 
        {
            Level.currentLevel.restartLevel();
            Unpause();
        }
	}

	public void Options() // method used by the pause menu to display the options sub-menu
    {
        GameStates.gameState = GameStates.GameState.Options; //set gamestate to options to display options interface
	}

	public void Exit() // method used by the pause menu to set the gamestate to the main menu on exit button click
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        GameStates.gameState = GameStates.GameState.Main; // sets gamestate back to main menu
    }
}
