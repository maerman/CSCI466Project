using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public GameObject canvas;
	public bool paused = false;

	void Start () {
		canvas.SetActive(false);
	}

	void Update(){
		if (Input.GetButtonDown("Pause")) {
			paused = !paused;
		} 
		if (paused) {
			canvas.SetActive(true);
			Time.timeScale = 0;
		}
		if (!paused) {
			canvas.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void Unpause() {
		Time.timeScale = 1;
		canvas.SetActive(false);
		paused = false;
	}

	public void Restart() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Options() {

	}

	public void Exit() {
		Application.LoadLevel (1);
	}
}
