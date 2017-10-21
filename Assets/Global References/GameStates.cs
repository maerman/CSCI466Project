using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    public static GameState gameState;
    public static PlayerState playerState;

    public enum GameState { Loading, PlayingFull, PlayingDemo, Paused, GameOver, Exit} //this will be used to control the state of the game we are in.
    public enum PlayerState { NotLoggedIn, LoggedIn, Killed, Victorious} //PlayerState
}
