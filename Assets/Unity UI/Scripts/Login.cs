using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static CRUD;
using static User;
using static GameStates;

public class Login : MonoBehaviour {

    public Button loginBtn;
    public InputField userName;
    public InputField password;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;
    private enum LoginState { Login, CreateAccount, UserNotFound, CreationError }
    private LoginState logState = new LoginState();

    public void UserLogin()
    {
        if (loginError(LoginState.Login)) {
            showErrorMenu("Error! You must have a valid username and password to sign in!");
            return;
        };
        crud.GetUser(userName.text, password.text); //log the user in
        if (loginError(LoginState.UserNotFound))
        {
            showErrorMenu("Error! There was no user account found with the details entered!");
            return;
        }
        //Debug.Assert(playerState == PlayerState.LoggedIn, "Player is not logged in!"); //player should be logged in here
        gameState = GameState.Main;

    }

    public void CreateAccount()
    {
        if (loginError(LoginState.CreateAccount))
        {
            showErrorMenu("Error! You must have a valid username and password to create an account!");
            return;
        }

        crud.CreateNewUser(userName.text, password.text, null, false);

        if (loginError(LoginState.CreationError))
        {
            showErrorMenu("Error! There was a problem creating your account! Please try again.");
            return;
        }
        Debug.Assert(user.username != null, "The user was not created successfully!");
        gameState = GameState.Main;

    }

    private Boolean loginError(LoginState loginState)
    {
        Boolean hasError = false;
        switch(loginState)
        {

            case LoginState.CreateAccount:
            case LoginState.Login:
                if (userName.text == "" || password.text == "") hasError = true;
                break;
            case LoginState.CreationError:
            case LoginState.UserNotFound:
                if (user.username == null) hasError = true;
                break;
         
        }

        return hasError;
    }
    //we will show the error Menu and the appropriate text with it
    private void showErrorMenu(string text)
    {
        errorText.text = text;
        errorPanel.SetActive(true);       
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
