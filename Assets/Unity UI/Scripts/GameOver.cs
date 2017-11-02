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

	public void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Exit() {
		Application.LoadLevel (1);
	}
}
