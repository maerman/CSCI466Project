using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameStates;

public class Delegates : MonoBehaviour {
    public delegate void LoginActionComplete();
    public static LoginActionComplete loginActionComplete;
    public delegate void CreationActionComplete();
    public static CreationActionComplete creationActionComplete;
    public delegate void PlayerActionComplete();
    public static PlayerActionComplete playerActionComplete;
    public delegate void GameActionComplete();
    public static GameActionComplete gameActionComplete;
    public delegate void DataSaveActionComplete();
    public static DataSaveActionComplete dataSaveActionComplete;
}
