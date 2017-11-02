using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    public static GameState gameState;
    public static PlayerState playerState;
    public static LoginState loginState;

    public enum GameState { Loading, PlayingFull, PlayingDemo, Paused, GameOver, Exit, LevelComplete} //this will be used to control the state of the game we are in.
    public enum PlayerState { NotLoggedIn, LoggedIn, Killed, Victorious} //PlayerState
    public enum LoginState { Login, CreateAccount, UserNotFound, CreationError, Duplicate, Success, LoginError }
}
