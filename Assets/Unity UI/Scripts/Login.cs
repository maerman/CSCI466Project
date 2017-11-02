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

public class Login : MonoBehaviour, IErrorPanel {

    public Button loginBtn;
    public InputField userName;
    public InputField password;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    private void Start()
    {      
        loginActionComplete += loginComplete; //attach this to the Delegate loginActionComplete, which will get called from wherever
    }
 
    public void loginComplete() 
    {
        string ErrorMsg = "";
            if (HasError())
            {
                switch(loginState)
            {
                case LoginState.UserNotFound:
                    ErrorMsg = "Error! There was no user account found with the details entered!";
                    break;
                case LoginState.LoginError:
                    ErrorMsg = "Error! There was a login error.  Please wait a few seconds and try again.";
                    break;
            }
                showErrorMenu(ErrorMsg);
                return;
            }
            Debug.Assert(user.username != null, "Player is not logged in!"); //player should be logged in here
            gameState = GameState.PlayingFull;
    }

    //User attempts to login to an existing account
    public void UserLogin()
    {
        loginState = LoginState.Login;
        if (HasError()) {
            showErrorMenu("Error! You must have a valid username and password to sign in!");
            return;
        };

        crud.GetUser(userName.text, password.text);
    }

    public bool HasError()
    {
        Boolean hasError = false;
        switch (loginState)
        {
            case LoginState.Login:
                if (userName.text == "" || password.text == "") hasError = true;
                break;
            case LoginState.UserNotFound:
                if (user.username == null) hasError = true;
                break;
        }
        return hasError;
    }

    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
