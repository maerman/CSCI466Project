using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	public GameObject pauseMenu;
	public bool paused = false;

    void Start ()
    {
		pauseMenu.SetActive(false);
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
			pauseMenu.SetActive(true);
			Time.timeScale = 0;
		}
		else
        {
			pauseMenu.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void Unpause()
    {
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
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
