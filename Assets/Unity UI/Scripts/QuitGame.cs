// written by: Metin Erman
// tested by: Michael Quinn
// debugged by: Metin Erman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStates;

public class Quit : MonoBehaviour {
    public Button quitBtn;

    public void QuitGame()
    {
        //don't quit the game from here, send it back to the state machine to allow any additional things we want to run
        gameState = GameState.Exit; 
    }

}
