// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// OptionsGame is a MonoBehavior that controls the Control Player 1 and Control Player 2 menus and has methods
/// that are called when its input UI items are changed that change input setting and has methods that are called when 
/// a button for a certian control is called that change the Key for the corrosponding control
/// </summary>
public class ModifyControls : MonoBehaviour
{
    //initilzed in editor
    public Text forward;
    public Text backward;
    public Text left;
    public Text right;
    public Text turnUp;
    public Text turnDown;
    public Text turnLeft;
    public Text turnRight;
    public Text[] items;
    public Text pickupDrop;
    public Text shoot;
    public Text pause;
    public Text zoomIn;
    public Text zoomOut;
    public Toggle relative;
    public Toggle turn;
    public Toggle mouse;
    public Text chooseInput;
    public int playerNumber = 0;

    private PlayerControls playerControls;
    private Key keyToModify;

    /// <summary>
    /// Set values in the menu to the values in the PlayerControls
    /// </summary>
    private void refresh()
    {
        //sets this menu's PlayerContorls the PlayerControls for the playerNumber set
        if (playerNumber >= 0 && playerNumber < Controls.MAX_PLAYERS)
            playerControls = Controls.get().players[playerNumber];
        else
            throw new System.Exception("Player Numbe in ModifyControls invalid: " + playerNumber);

        forward.text = playerControls.forwardKey.ToString();
        backward.text = playerControls.backwardKey.ToString();
        left.text = playerControls.straifLKey.ToString();
        right.text = playerControls.straifRKey.ToString();
        turnUp.text = playerControls.turnUpKey.ToString();
        turnDown.text = playerControls.turnDownKey.ToString();
        turnRight.text = playerControls.turnRKey.ToString();
        turnLeft.text = playerControls.turnLKey.ToString();
        if (items.Length != playerControls.itemKeys.Length)
        {
            throw new System.Exception("Wrong number of Item keys in ModifyControls");
        }
        for (int i = 0; i < items.Length; i++)
        {
            items[i].text = playerControls.itemKeys[i].ToString();
        }
        pickupDrop.text = playerControls.pickupDropKey.ToString();
        shoot.text = playerControls.shootKey.ToString();
        pause.text = playerControls.pauseKey.ToString();
        zoomIn.text = playerControls.zoomInKey.ToString();
        zoomOut.text = playerControls.zoomOutKey.ToString();

        relative.isOn = playerControls.relativeMovement;
        turn.isOn = playerControls.turns;
        mouse.isOn = playerControls.mouseTurn;
    }

    void Start()
    {
        refresh();
    }

    private void OnEnable()
    {
        refresh();
    }

    void Update()
    {
        //if there is a key that needs to be modified, ask the user to activate
        //an input unil he does. Once he does, set the input to the key being modified
        if (keyToModify != null)
        {
            //display message
            chooseInput.enabled = true;

            //see if an input is activated
            Key temp = Key.activatedKey();

            //if one is, set the key being modifed to it
            if (temp != null)
            {
                keyToModify.changeValue(temp);
                keyToModify = null;
                refresh();
            }
        }
        else
        {
            //hide message
            chooseInput.enabled = false;
        }
    }

    //The following methods are called when the corrosponding controls button is pressed. 
    //It then sets the corrosponding Key to be modifed, which will be done in Update().
    public void Forward()
    {
        keyToModify = playerControls.forwardKey;
    }
    public void Backward()
    {
        keyToModify = playerControls.backwardKey;
    }
    public void Left()
    {
        keyToModify = playerControls.straifLKey;
    }
    public void Right()
    {
        keyToModify = playerControls.straifRKey;
    }
    public void TurnUp()
    {
        keyToModify = playerControls.turnUpKey;
    }
    public void TurnDown()
    {
        keyToModify = playerControls.turnDownKey;
    }
    public void TurnLeft()
    {
        keyToModify = playerControls.turnLKey;
    }
    public void TurnRight()
    {
        keyToModify = playerControls.turnRKey;
    }
    public void Item1()
    {
        keyToModify = playerControls.itemKeys[0];
    }
    public void Item2()
    {
        keyToModify = playerControls.itemKeys[1];
    }
    public void Item3()
    {
        keyToModify = playerControls.itemKeys[2];
    }
    public void Item4()
    {
        keyToModify = playerControls.itemKeys[3];
    }
    public void Item5()
    {
        keyToModify = playerControls.itemKeys[4];
    }
    public void Item6()
    {
        keyToModify = playerControls.itemKeys[5];
    }
    public void Item7()
    {
        keyToModify = playerControls.itemKeys[6];
    }
    public void Item8()
    {
        keyToModify = playerControls.itemKeys[7];
    }
    public void Item9()
    {
        keyToModify = playerControls.itemKeys[8];
    }
    public void Item10()
    {
        keyToModify = playerControls.itemKeys[9];
    }
    public void PickupDrop()
    {
        keyToModify = playerControls.pickupDropKey;
    }
    public void Shoot()
    {
        keyToModify = playerControls.shootKey;
    }
    public void Pause()
    {
        keyToModify = playerControls.pauseKey;
    }
    public void ZoomIn()
    {
        keyToModify = playerControls.zoomInKey;
    }
    public void ZoomOut()
    {
        keyToModify = playerControls.zoomOutKey;
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Controls Options value.
    public void RelativeDirect()
    {
        playerControls.setRelativeMovement(relative.isOn);
        //refresh();
    }
    public void TurnPoint()
    {
        playerControls.setTurns(turn.isOn);
        //refresh();
    }
    public void Mouse()
    {
        playerControls.mouseTurn = mouse.isOn;
        refresh();
    }

    /// <summary>
    /// Method called by the Reset button, resets the controls to their default values and 
    /// refreshes this menu
    /// </summary>
    public void ResetButton()
    {
        Controls.get().setDefaultControls();
        refresh();
    }

    /// <summary>
    /// Method called by the Back button, changes the screen to OptionsHub screen
    /// </summary>
    public void Back()
    {
        GameStates.gameState = GameStates.GameState.OptionsHub;
    }

    /// <summary>
    /// Method called by the Player1 or Player2 buttons, changes the screen to the other 
    /// Player's modify controls menu.
    /// </summary>
    public void PlayerOther()
    {
        if (GameStates.gameState == GameStates.GameState.OptionsPlayer1)
        {
            GameStates.gameState = GameStates.GameState.OptionsPlayer2;
        }
        else
        {
            GameStates.gameState = GameStates.GameState.OptionsPlayer1;
        }
    }
}
