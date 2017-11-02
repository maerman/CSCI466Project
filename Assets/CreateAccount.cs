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

public class CreateAccount : MonoBehaviour, IErrorPanel
{
    public Button createBtn;
    public InputField userName;
    public InputField password;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    private void Start()
    {
            
            creationActionComplete += CreationComplete; //attach this to the Delegate loginActionComplete, which will get called from wherever  
    }

    //User attempts to create a new account
    public void CreateUser()
    {
        loginState = LoginState.CreateAccount;
        if (HasError())
        {
            showErrorMenu("Error! You must have a valid username and password to sign in!");
            return;
        };

        crud.CreateNewUser(userName.text, password.text, null, 0);
    }

    public void CreationComplete()
    {
        string ErrorMsg = "";
        if (HasError())
        {
            switch(loginState)
            {
                case LoginState.CreationError:
                    ErrorMsg = "Error! There was a problem creating your account! Please try again.";
                    break;
                case LoginState.Duplicate:
                    ErrorMsg = "Error! This username already exists.  Please choose a unique username and try again.";
                    break;
            }
            showErrorMenu(ErrorMsg);
            return;
        }
        Debug.Assert(user.username != null, "The user was not created successfully!");
        gameState = GameState.PlayingFull;
    }

    //Implement Error Panel interfaces from IErrorPanel below
    public Boolean HasError()
    {
        Boolean hasError = false;
        switch (loginState)
        {
            case LoginState.CreateAccount:
                if (userName.text == "" || password.text == "") hasError = true;
                break;
            case LoginState.CreationError:
                if (user.username == null) hasError = true;
                break;
            case LoginState.Duplicate:
                hasError = true;
                break;
        }
        return hasError;
    }

    //we will show the error Menu and the appropriate text with it
    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }

}
