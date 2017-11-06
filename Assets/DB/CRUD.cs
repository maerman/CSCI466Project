using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static User;
using static UserData;
using static JSONObject;
using System;
using static GameStates;
using static Delegates;

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
        Debug.Log("Getting User information");
        //var complete = false; //return complete to the calling function
        string url = baseUrl + "getUser.php?username=" + username + "&password=" + password;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
        Debug.Log("Request not complete yet");
        while(!www.isDone)
        {
            new WaitForSeconds(0.5f);
        }

        //StopCoroutine("WaitForRequest");
        //gameState = GameState.LoggingIn;
        if (www.isDone)
        {
            Debug.Log("DB call completed");
            //Debug.Log(www.text);
            if (string.IsNullOrEmpty(www.error)) //we check for the www.to be done and that it hasn't returned an error
            {
                if (www.text.Contains("null")) //no user was found
                {
                    login = LoginErrors.UserNotFound; //no user was found;
                }
                else
                {
                    try
                    {
                       // myText = myText.Contains("\n") ? myText.Remove( : myText;
                        Debug.Log("Data returned successfully!");
                        //TODO figure out why this is mapping to a dictionary and not a class
                        //var myarr = www.text.Split('\n');
                        //Debug.Log(myarr);
                        var myObj = new JSONObject(www.text); //parse the JSON and update the user record
                        //JsonUtility.FromJsonOverwrite(www.text, user);
                        user.id = int.Parse(myObj.list[0].ToString().Replace("\"", "")); //convert to an int
                        userData.accountId = user.id;
                        user.username = myObj.list[1].ToString();
                        user.password = myObj.list[2].ToString();
                        user.email = myObj.list[3].ToString();
                        user.isTrial = myObj.list[4]; //convert to an int
                        Debug.Log("Successfully Logged In as " + user.username);
                        Debug.Log("Id: " + user.id);
                        Debug.Log("password" + user.password);
                        Debug.Log("email:" + user.email);
                        Debug.Log("Is Trial?:"  + user.isTrial);
                        gameState = GameState.Main; //change the game state to Main
                        login = LoginErrors.LoginSuccess;
                        
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Unsuccessful conversion of JSON Object to user class! Error: " + e.Message + " : " + e.InnerException);
                        throw new Exception("There was an error creating the user profile.  Data returned successfully. Error: " + e.Message);
                    }
                }
            }
            else if (www.error == "null")
            {
                login = LoginErrors.UserNotFound; //no user was found;
            }
            else
            {
                login = LoginErrors.LoginError;
            }
            Debug.Log("Calling Login Delegate");
            loginActionComplete(); //fire the delegate!          
            return;
        }

            
       // return complete;
    }
    //POST -Creates a new user
    public void CreateNewUser(string username, string password, string email, bool isTrial) //user object should already be populated with values
    {
        string url = baseUrl + "createUser.php?username=" + username + "&password=" + password + "&email=" + email + "&isTrial=" + isTrial;
        WWW www = new WWW(url);

        StartCoroutine(WaitForRequest(www));
        Debug.Log("Create User CoRoutine started");
        while(!www.isDone)
        {
            new WaitForSeconds(0.5f);
        }
        if(www.isDone)
        {
            Debug.Log("Create User www complete");
            StopCoroutine("WaitForRequest");
            if (www.text.Contains("Duplicate"))
            {
                login = LoginErrors.Duplicate;
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(www.error))
                    {
                        user.id = int.Parse(www.text); //id number comes back in the response
                        user.username = username;
                        user.password = password;
                        user.email = email;
                        user.isTrial = false;
                        login = LoginErrors.LoginSuccess;
                    }
                    else if(www.error.Contains("Duplicate"))
                        {
                        login = LoginErrors.Duplicate;
                        }
                    else {
                        login = LoginErrors.CreationError;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Unsuccessful conversion of JSON Object to user class! Error: " + e.Message + " : " + e.InnerException);
                    throw new Exception("There was an error creating the user profile.  Data returned successfully. Error: " + e.Message);
                }
            }
            creationActionComplete(); //fire the delegate
            return;
        }

        
    }

    public void SaveUserData()
    {
        string url = baseUrl + "saveData.php?accountId=" + userData.accountId + "&score=" + userData.score + "&enemiesKilled=" + userData.enemiesKilled + "&level=" + userData.currentLevel + "&timeAlive=" + userData.timeAlive + 
            "&timesDied=" + userData.timesDied + "&playerMode=" + userData.playerMode + "&difficulty=" + userData.difficulty;

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));

        while(!www.isDone)
        {
            new WaitForSeconds(0.5f);
        }

        if (www.isDone)
        {

            StopCoroutine("WaitForRequest");
            if (www.text.Contains("Duplicate"))
            {
                login = LoginErrors.Duplicate;
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(www.error))
                    {
                        userData.accountId = int.Parse(www.text); //id number comes back in the response
                    }
                    else if (www.error.Contains("Duplicate"))
                    {
                        login = LoginErrors.Duplicate;
                    }
                    else
                    {
                        login = LoginErrors.CreationError;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Unsuccessful conversion of JSON Object to user class! Error: " + e.Message + " : " + e.InnerException);
                    throw new Exception("There was an error creating the user profile.  Data returned successfully. Error: " + e.Message);
                }
            }
            dataSaveActionComplete(); //fire the delegate
            return;
        }

    }

    //Waits for www to return
    private IEnumerator WaitForRequest(WWW www)
    {

        Debug.Log("Waiting for DB request to complete");

        if (www.isDone && !string.IsNullOrEmpty(www.error)) //if it's finished and there is an error
        {
            Debug.LogError("There was an error loading the user profile!  Error: " + www.error);
            throw new Exception("There was an error logging into your account! " + www.error);
        }

        yield return new WaitForSeconds(0.5f);

    }   

}


