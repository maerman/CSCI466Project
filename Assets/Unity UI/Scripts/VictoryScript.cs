﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour {
	public GameObject canvas;
	// Use this for initialization
	void Start ()
    {
		canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void Exit()
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel.gameObject);
        }
        GameStates.gameState = GameStates.GameState.Main;
	}
}
