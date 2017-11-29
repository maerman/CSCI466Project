using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private void refresh()
    {
        playerControls = Controls.get().players[playerNumber];

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
            throw new System.Exception("wrong number of Item keys in ModifyControls");
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
        if (keyToModify != null)
        {
            chooseInput.enabled = true;
            Key temp = Key.activatedKey();

            if (temp != null)
            {
                keyToModify.changeValue(temp);
                keyToModify = null;
                refresh();
            }
        }
        else
        {
            chooseInput.enabled = false;
        }
    }

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

    public void ResetButton()
    {
        Controls.get().setDefaultControls();
        refresh();
    }

    public void Back()
    {
        GameStates.gameState = GameStates.GameState.OptionsHub;
    }

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
