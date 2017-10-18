using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User : MonoBehaviour {
    public static User user; //declare the static object---there is only one user

    public string id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string isTrial { get; set; }

    private void Awake() //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
    {
        if (user == null)
        {

            user = this;
            DontDestroyOnLoad(user); //this ensures it remains in the game and avaialble at all times
        } else
        {
            Destroy(user); //destroy if another one is attempted to be created.
        }
    }

}
