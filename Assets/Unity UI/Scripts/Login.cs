using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CRUD;
using static User;
using static GameStates;

public class Login : MonoBehaviour {

    public Button loginBtn;
    public InputField userName;
    public InputField password;

    public void UserLogin()
    {
        crud.GetUser(userName.text, password.text); //log the user in
        Debug.Assert(playerState == PlayerState.LoggedIn, "Player is not logged in!"); //player should be logged in here
        gameState = GameState.PlayingFull;
    }

    public void CreateAccount()
    {
       crud.CreateNewUser(userName.text, password.text, null, 0);
       Debug.Assert(user.username != null, "The user was not created successfully!");
       gameState = GameState.PlayingFull;
    }
}
