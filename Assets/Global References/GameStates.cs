// written by: Metin Erman, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Metin Erman, Thomas Stewart

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    public static GameState gameState;
    public static GameState previousGameState;
    public static LoginErrors login;

    public enum GameState { LoggingIn, CreateAccount, Main, NewGame, LoadGame, Playing, Paused,
        LevelComplete, LostGame, WonGame, LoadReplay, OptionsHub, OptionsGame, OptionsAudVid,
        OptionsPlayer1, OptionsPlayer2, Replay, About, Exit} //this will be used to control the state of the game we are in.

    public enum LoginErrors{ UserNotFound, PreLogin, LoginError, LoginSuccess, Duplicate, CreationError}
    
}
