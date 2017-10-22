using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public GameObject canvas;
	public bool paused = false;
    public Canvas pauseMenu; //initilized in editor

    void Start ()
    {
		canvas.SetActive(false);
	}

	void Update()
    {
		if (Input.GetButtonDown("Pause"))
        {
			paused = !paused;
		}
        foreach (PlayerControls item in Controls.get().players)
        {
            if (item.Pause)
            {
                paused = !paused;
                break;
            }
        }
		if (paused)
        {
            pauseMenu.enabled = true;
			canvas.SetActive(true);
			Time.timeScale = 0;
		}
		if (!paused)
        {
            pauseMenu.enabled = false;
			canvas.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void Unpause()
    {
		Time.timeScale = 1;
		canvas.SetActive(false);
		paused = false;
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
        //open options menu
	}

	public void Exit()
    {
        if (Level.currentLevel != null)
        {
            Destroy(Level.currentLevel);
        }
        //open main menu
        //Application.LoadLevel (1); //Why was this here?
    }
}
