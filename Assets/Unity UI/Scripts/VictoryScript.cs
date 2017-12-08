// written by: Shane Barry
// tested by: Michael Quinn
// debugged by: Shane Barry

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
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
        if (Level.current != null)
        {
            Destroy(Level.current.gameObject);
        }
        GameStates.gameState = GameStates.GameState.Main;
	}
}
