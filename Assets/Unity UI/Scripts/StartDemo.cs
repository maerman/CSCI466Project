using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStates;

public class StartDemo : MonoBehaviour {

    public Button PlayDemo;

    public void StartDemoGame()
    {
        gameState = GameState.PlayingDemo;
    }
}
