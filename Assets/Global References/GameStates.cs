using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    public static GameState gameState;

    public enum GameState { NotLoggedIn, LoggedIn, PlayingFull, PlayingDemo, Paused, GameOver} //this will be used to control the state of the game we are in.
}
