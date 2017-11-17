using UnityEngine;
using System.Collections;

public class AboutMenu : MonoBehaviour
{
    public void back() //method used by the new game menu to go back to the main menu
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
