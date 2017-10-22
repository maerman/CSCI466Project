using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameStates;

public class StartDemo : MonoBehaviour {

    public Button PlayDemo;

    public void StartDemoGame()
    {
        Level lvl1 = Level.getLevel(1);
        lvl1.initilize(2, 1, (int)System.DateTime.Now.Ticks);

        gameState = GameState.PlayingDemo;
    }
}
