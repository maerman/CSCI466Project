using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public bool paused = false;

    void Start ()
    {
        
	}

	void Update()
    {
        
	}

	public void Unpause()
    {
        GameStates.gameState = GameStates.previousGameState;
	}

	public void Restart()
    {
        if (Level.currentLevel == null)
        {
            throw new System.Exception("Current Level is null, can't restart");
        }
        else
        {
            Level.currentLevel.restartLevel();
            Unpause();
        }
	}

	public void Options()
    {
        GameStates.gameState = GameStates.GameState.Options;
	}

	public void Exit()
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        GameStates.gameState = GameStates.GameState.Main;
    }
}
