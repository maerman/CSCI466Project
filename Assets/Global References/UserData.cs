// written by: Metin Erman
// tested by: Michael Quinn
// debugged by: Metin Erman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MonoBehaviour {

    public static UserData userData;

    public int id { get; set; }
    public int accountId { get; set; }
    public DateTime dateTimeAdded { get; set; }
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
