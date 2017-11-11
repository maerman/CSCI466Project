using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MonoBehaviour {

    public static UserData userData;

    public int id { get; set; }
    public int accountId { get; set; }
    public DateTime dateTimeAdded { get; set; }
    public int score { get; set; }
    public int enemiesKilled { get; set; }
    public int currentLevel { get; set; }
    public int timeAlive { get; set; }
    public int timesDied { get; set; }
    public int playerMode { get; set; }
    public float difficulty { get; set; }
    public bool isTrial { get; set; }

    private void Awake()
    {
        if(userData == null)
        {
            userData = this;
            DontDestroyOnLoad(userData);
        }else
        {
            Destroy(userData);
        }
    }
}
