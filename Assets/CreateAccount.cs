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
        DOTween.SetTweensCapacity(5, 5);
        creationActionComplete += CreationComplete; //attach this to the Delegate loginActionComplete, which will get called from wherever  
    }

    //User attempts to create a new account
    public void CreateUser()
    {
        //login = LoginErrors.PreLogin;
        gameState = GameState.LoggingIn;
        if (HasError())
        {
            showErrorMenu("Error! You must have a valid username and password to create an account!");
            return;
        };
       
        crud.CreateNewUser(userName.text, password.text, null, false);
    }

    public void CreationComplete()
    {
        string ErrorMsg = "";
        if (HasError())
        {
            switch(login)
            {
                case LoginErrors.CreationError:
                    ErrorMsg = "Error! There was a problem creating your account! Please try again.";
                    break;
                case LoginErrors.Duplicate:
                    ErrorMsg = "Error! This username already exists.  Please choose a unique username and try again.";
                    break;
            }
            showErrorMenu(ErrorMsg);
            return;
        }       
        Debug.Assert(user.username != null, "The user was not created successfully!");
        showErrorMenu("Account has been created successfully!");
        gameState = GameState.Main;
    }

    //Implement Error Panel interfaces from IErrorPanel below
    public Boolean HasError()
    {
        Boolean hasError = false;

        if (gameState == GameState.LoggingIn)
        hasError = userName.text == "" || password.text == "" ? true : false; 
        
        switch (login)
        {

            case LoginErrors.CreationError:
                if (user.username == null) hasError = true;
                break;
            case LoginErrors.Duplicate:
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
