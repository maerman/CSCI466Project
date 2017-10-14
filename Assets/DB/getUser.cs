using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static User;
using SimpleJSON;
using System;
using System.Web;

public class getUser : MonoBehaviour {

    public static getUser GetUser;
    
    private void Awake() //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
    {
        if (GetUser == null)
        {
            GetUser = this;
        }
        else
        {
            Destroy(GetUser); //destroy if another one is attempted to be created.
        }
    }
    public void queryUser(string username, string password)
    {
        string url = "http://nebulawars.heliohost.org/phpDBScripts/getUser.php?username=" + username + "&password=" + password;
        WWW www = new WWW(url);
       
        StartCoroutine(WaitForRequest(www));
    }

    public  IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        //check for errors
        if (www.error == "")
        {         

            var myObj = JSON.Parse(www.text); //converts to a dictionary object---
            user.id = myObj[0];
            user.username = myObj[1];
            user.password = myObj[2];
            user.isTrial = myObj[3];
            user.email = myObj[4];
            
            Debug.Log(user.id);
            Debug.Log(user.username);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }   

}


