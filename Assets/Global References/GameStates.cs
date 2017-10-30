using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour {

    public static GameState gameState;
    public static GameState previousGameState;

    public enum GameState { LoggingIn, CreatAccount, Main, NewGame, LoadGame, Playing, Paused,
        LevelComplete, LostGame, WonGame, LoadReplay, Options, Replay, About, Exit} //this will be used to control the state of the game we are in.
    
}
