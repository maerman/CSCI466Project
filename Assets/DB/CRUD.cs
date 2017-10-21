using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static User;
using SimpleJSON;
using System;
using static GameStates;

public class CRUD : MonoBehaviour {

    public static CRUD crud;

    string baseUrl = "http://nebulawars.heliohost.org/phpDBScripts/";

    private void Awake() //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
    {
        if (crud == null)
        {
            crud = this;
        }
        else
        {
            Destroy(crud); //destroy if another one is attempted to be created.
        }
    }

    //GET ---gets a user who is logging in
    public void GetUser(string username, string password)
    {
        string url = baseUrl + "getUser.php?username=" + username + "&password=" + password;
        WWW www = new WWW(url);
       
        StartCoroutine(WaitForRequest(www));

        if (www.isDone && string.IsNullOrEmpty(www.error)) //we check for the www.to be done and that it hasn't returned an error
        {
            try
            {
                //TODO figure out why this is mapping to a dictionary and not a class
                var myObj = JSON.Parse(www.text); //parse the JSON and update the user record
                user.id = int.Parse(myObj[0]); //convert to an int
                user.username = myObj[1];
                user.password = myObj[2];
                user.email = myObj[3];
                user.isTrial = int.Parse(myObj[4]); //convert to an int
                
                playerState = PlayerState.LoggedIn; //change the game state to LoggedIn
                Debug.Log("Successfully Logged In as " + user.username);
            }
            catch(Exception e)
            {
                Debug.LogError("Unsuccessful conversion of JSON Object to user class! Error: " + e.Message + " : " + e.InnerException);
                throw new Exception("There was an error creating the user profile.  Data returned successfully. Error: " + e.Message);
            }
        }

    }
    //POST -Creates a new user
    public void CreateNewUser(string username, string password, string email, int isTrial) //user object should already be populated with values
    {
        string url = baseUrl + "createUser.php?username=" + username + "&password=" + password + "&email=" + email + "&isTrial=" + isTrial;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
        if(www.isDone)
        {
            if (!string.IsNullOrEmpty(www.error) )
            {
                ;
                user.id = int.Parse(www.text); //id number comes back in the response
                user.username = username;
                user.password = password;
                user.email = email;
                user.isTrial = isTrial;
            }
        }
    }

    //Waits for www to return
    private IEnumerator WaitForRequest(WWW www)
    {

        if (www.isDone && !string.IsNullOrEmpty(www.error)) //if it's finished and there is an error
        {
            Debug.LogError("There was an error loading the user profile!  Error: " + www.error);
            throw new Exception("There was an error logging into your account! " + www.error);
        }

        yield return www; //return the www object

    }   

}


