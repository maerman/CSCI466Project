using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
    {
		if (Level.currentLevel == null)
        {
            throw new System.Exception("Can't restart level, current level is set to null");
        }
        Level.currentLevel.restartLevel();
        GameStates.gameState = GameStates.GameState.Playing;
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
