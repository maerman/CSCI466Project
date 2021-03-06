﻿// written by: Shane Barry, Metin Erman, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Metin Erman

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static CRUD;
using static User;
using static GameStates;
using static Delegates;

/// <summary>
/// Login is a MonoBehavior that controls the Login menu and has method that buttons 
/// in the menu call. 
/// </summary>
public class Login : MonoBehaviour, IErrorPanel
{
    //initilized in editor
    public Button loginBtn;
    public InputField userName;
    public InputField password;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    private void Start()
    {
        DOTween.SetTweensCapacity(5, 5);
        loginActionComplete += loginComplete; //attach this to the Delegate loginActionComplete, which will get called from wherever
    }
 
    public void loginComplete() 
    {
        string ErrorMsg = "";
            if (HasError()) //calls method to determine if credentials are invalid
            {
                switch(login)
            {
                case LoginErrors.UserNotFound:
                    ErrorMsg = "Error! There was no user account found with the details entered!";
                    break;
                case LoginErrors.LoginError:
                    ErrorMsg = "Error! There was a login error.  Please wait a few seconds and try again.";
                    break;
            }
                showErrorMenu(ErrorMsg);
                return;
            }
            Debug.Assert(user.username != null, "Player is not logged in!"); //player should be logged in here
        gameState = GameState.Main;
    }

    //User attempts to login to an existing account
    public void UserLogin()
    {
        gameState = GameState.LoggingIn;

        if (userName == null || password == null)
        {
            throw new Exception("Username or password InputField set to null when trying to login.");
        }
        if (userName.text == "" || password.text == "")
        {
            showErrorMenu("Error! You must have a valid username and password to sign in!");
            return;
        };

        crud.GetUser(userName.text, password.text);
    }

    public bool HasError() //determines if input fields are invalid
    {
        Boolean hasError = false;

        if (gameState == GameState.LoggingIn) hasError = userName.text == "" || password.text == "" ? true : false;
        if (login == LoginErrors.UserNotFound) hasError = user.username == null ? true : false;

        return hasError;
    }

    public void showErrorMenu(string errorMsg) //displays error message
    {
        
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }

    /// <summary>
    /// Called by the Create Account button, sets the screen to the Create Account menu
    /// </summary>
    public void CreateAccount()
    {
        gameState = GameState.CreateAccount;
    }

    /// <summary>
    /// Called by the Play Demo button, sets the scren to the Main menu and 
    /// sets the user's account to a trial account
    /// </summary>
    public void PlayDemo() //sets gamestate to the main menu and variable that signifies demo version use to true
    {
        User.user.isTrial = true;
        gameState = GameState.Main;
    }

    /// <summary>
    /// Called by the Quit button, sets the GameState to Exit which will cause
    /// GameLoop the close the program. 
    /// </summary>
    public void Quit()
    {
        gameState = GameState.Exit;
    }
}
