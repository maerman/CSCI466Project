using UnityEngine;
using System.Collections.Generic;

public class PlayerInputInstance
{
    public const int NUM_ITEMS = 10;

    public bool forward;
    public bool backward;
    public bool straifL;
    public bool straifR;
    public bool turnL;
    public bool turnR;
    public bool[] items = new bool[NUM_ITEMS];
    public bool dropItem;
    public bool shoot;
    public bool pause;

    public override string ToString()
    {
        //convert to string
        return "";
    }

    public static PlayerInputInstance fromString(string inputInfo)
    {
        PlayerInputInstance toReturn = new PlayerInputInstance();

        //convert from string

        return toReturn;
    }
}

public class PlayerControls
{
    //100,000 ~ 1 hour at 30fps
    private const int initalInputsSize = 100000;

    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode straifLKey;
    public KeyCode straifRKey;
    public KeyCode turnLKey;
    public KeyCode turnRKey;
    public KeyCode[] itemKeys = new KeyCode[PlayerInputInstance.NUM_ITEMS];
    public KeyCode dropItemKey;
    public KeyCode shootKey;
    public KeyCode pauseKey;

    #region Key Accessors
    //if the key is pressed at all
    public bool forward
    {
        get
        {
            return current.forward;
        }
    }
    public bool backward
    {
        get
        {
            return current.backward;
        }
    }
    public bool straifL
    {
        get
        {
            return current.straifL;
        }
    }
    public bool straifR
    {
        get
        {
            return current.straifR;
        }
    }
    public bool turnL
    {
        get
        {
            return current.turnL;
        }
    }
    public bool turnR
    {
        get
        {
            return current.turnR;
        }
    }
    public bool items(int index)
    {
        return current.items[index];
    }
    public bool dropItem
    {
        get
        {
            return current.dropItem;
        }
    }
    public bool shoot
    {
        get
        {
            return current.shoot;
        }
    }
    public bool pause
    {
        get
        {
            return current.pause;
        }
    }

    //if the key was just pressed (not held down)
    public bool Forward
    {
        get
        {
            return current.forward && !previous.forward;
        }
    }
    public bool Backward
    {
        get
        {
            return current.backward && !previous.backward;
        }
    }
    public bool StraifL
    {
        get
        {
            return current.straifL && !previous.straifL;
        }
    }
    public bool StraifR
    {
        get
        {
            return current.straifR && !previous.straifR;
        }
    }
    public bool TurnL
    {
        get
        {
            return current.turnL && !previous.turnL;
        }
    }
    public bool TurnR
    {
        get
        {
            return current.turnR && !previous.turnR;
        }
    }
    public bool Items(int index)
    {
        return current.items[index] && !previous.items[index];
    }
    public bool DropItem
    {
        get
        {
            return current.dropItem && !previous.dropItem;
        }
    }
    public bool Shoot
    {
        get
        {
            return current.shoot && !previous.shoot;
        }
    }
    public bool Pause
    {
        get
        {
            return current.pause && !previous.pause;
        }
    }
    #endregion

    private PlayerInputInstance current = new PlayerInputInstance(), previous = new PlayerInputInstance();

    private List<PlayerInputInstance> theInputs = new List<PlayerInputInstance>(initalInputsSize);

    public List<PlayerInputInstance> inputs
    {
        get
        {
            return theInputs;
        }
    }

    public void updateFromInput()
    {
        previous = current;
        current = new PlayerInputInstance();

        current.forward = Input.GetKey(forwardKey);
        current.backward = Input.GetKey(backwardKey);
        current.straifL = Input.GetKey(straifLKey);
        current.straifR = Input.GetKey(straifRKey);
        current.turnL = Input.GetKey(turnLKey);
        current.turnR = Input.GetKey(turnRKey);
        for (int i = 0; i < PlayerInputInstance.NUM_ITEMS; i++)
        {
            current.items[i] = Input.GetKey(itemKeys[i]);
        }
        current.dropItem = Input.GetKey(dropItemKey);
        current.shoot = Input.GetKey(shootKey);
        current.pause = Input.GetKey(pauseKey);

        inputs.Add(current);
    }

    public void updateFromString(string updateInfo)
    {
        previous = current;
        current = PlayerInputInstance.fromString(updateInfo);

        inputs.Add(current);
    }

    public void clearInputs()
    {
        theInputs = new List<PlayerInputInstance>(initalInputsSize);
        previous = new PlayerInputInstance();
        current = new PlayerInputInstance();
    }
}

public static class Controls
{
    public const int MAX_PLAYERS = 2;

    private static PlayerControls[] thePlayers = new PlayerControls[MAX_PLAYERS];

    public static PlayerControls[] players
    {
        get
        {
            return thePlayers;
        }
    }

    public static void setDefaultControls()
    {
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            thePlayers[i] = new PlayerControls();
        }

        players[0].forwardKey = KeyCode.W;
        players[0].backwardKey = KeyCode.S;
        players[0].straifLKey = KeyCode.LeftArrow;
        players[0].straifRKey = KeyCode.RightArrow;
        players[0].turnLKey = KeyCode.A;
        players[0].turnRKey = KeyCode.D;
        players[0].itemKeys[0] = KeyCode.Keypad0;
        players[0].itemKeys[1] = KeyCode.Keypad1;
        players[0].itemKeys[2] = KeyCode.Keypad2;
        players[0].itemKeys[3] = KeyCode.Keypad3;
        players[0].itemKeys[4] = KeyCode.Keypad4;
        players[0].itemKeys[5] = KeyCode.Keypad5;
        players[0].itemKeys[6] = KeyCode.Keypad6;
        players[0].itemKeys[7] = KeyCode.Keypad7;
        players[0].itemKeys[8] = KeyCode.Keypad8;
        players[0].itemKeys[9] = KeyCode.Keypad9;
        players[0].dropItemKey = KeyCode.LeftAlt;
        players[0].shootKey = KeyCode.Space;
        players[0].pauseKey = KeyCode.Escape;

        if (MAX_PLAYERS > 1)
        {
            //need to figureout what to do with controller axises
            players[1].forwardKey = KeyCode.None;
            players[1].backwardKey = KeyCode.None;
            players[1].straifLKey = KeyCode.None;
            players[1].straifRKey = KeyCode.None;
            players[1].turnLKey = KeyCode.None;
            players[1].turnRKey = KeyCode.None;
            players[1].itemKeys[0] = KeyCode.Joystick1Button0;
            players[1].itemKeys[1] = KeyCode.Joystick1Button1;
            players[1].itemKeys[2] = KeyCode.Joystick1Button2;
            players[1].itemKeys[3] = KeyCode.Joystick1Button3;
            players[1].itemKeys[4] = KeyCode.None;
            players[1].itemKeys[5] = KeyCode.None;
            players[1].itemKeys[6] = KeyCode.None;
            players[1].itemKeys[7] = KeyCode.None;
            players[1].itemKeys[8] = KeyCode.None;
            players[1].itemKeys[9] = KeyCode.None;
            players[1].dropItemKey = KeyCode.Joystick1Button5;
            players[1].shootKey = KeyCode.Joystick1Button4;
            players[1].pauseKey = KeyCode.Joystick1Button7;
        }
    }

    public static void updateFromInput()
    {
        foreach (PlayerControls item in players)
        {
            if (item != null)
            {
                item.updateFromInput();
            }
        }
    }

    public static void updateFromFile(System.IO.StreamReader file)
    {
        if (file == null)
        {
            //throw exception
            return;
        }

        string current;

        foreach (PlayerControls item in players)
        {
            current = file.ReadLine();
            //read next line from file into 'current'
            item.updateFromString(current);
        }
    }

    public static void saveInputsToFile(System.IO.StreamWriter file)
    {
        if (file == null)
        {
            //throw exception
            return;
        }

        for (int i = 0; i < players[0].inputs.Count; i++)
        {
            for (int j = 0; j < MAX_PLAYERS; j++)
            {
                file.WriteLine(players[j].inputs[i].ToString());
            }
        }
    }

    public static void setControlsFromFile(System.IO.StreamReader file)
    {
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            thePlayers[i] = new PlayerControls();
        }
        //set controls from file
    }

    public static void saveControlsToFile(System.IO.StreamWriter file)
    {
        //save controls to file
    }
}
