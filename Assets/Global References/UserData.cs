using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MonoBehaviour {

        public static UserData userData;

        int id { get; set; }
        int accountId { get; set; }
        DateTime dateTimeAdded { get; set; }
        int score { get; set; }
        int enemiesKilled { get; set; }
        int highestLevel { get; set; }
        int timeAlive { get; set; }
        int timesDied { get; set; }
        string playerMode { get; set; }
        string difficulty { get; set; }

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
