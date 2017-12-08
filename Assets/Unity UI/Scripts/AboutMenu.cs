// written by: Shane Barry
// tested by: Michael Quinn
// debugged by: Shane Barry

using UnityEngine;
using System.Collections;

/// <summary>
/// AboutMenu is a MonoBehavior that controls the About menu and has methods 
/// that are called when the menu's buttons are pressed. 
/// </summary>
public class AboutMenu : MonoBehaviour
{
    /// <summary>
    /// method the back button calls, changes the screen to the Main menu
    /// </summary>
    public void back() 
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
