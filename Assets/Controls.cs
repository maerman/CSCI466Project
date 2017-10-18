using UnityEngine;
using System.Collections.Generic;

public class PlayerInput
{
    public const int NUM_ITEMS = 10;

    public float forward;
    public float backward;
    public float straifL;
    public float straifR;
    public float turnL;
    public float turnR;
    public bool[] items = new bool[NUM_ITEMS];
    public bool dropItem;
    public bool shoot;
    public bool pause;

    public bool relativeMovement;

    public PlayerInput() { }

    public PlayerInput(string inputInfo)
    {
        char[] seperators = new char[1];
        seperators[0] = ' ';

        string[] parts = inputInfo.Split(seperators);

        if (parts.Length == 20)
        {
            forward = System.Convert.ToSingle(parts[0]);
            backward = System.Convert.ToSingle(parts[1]);
            straifL = System.Convert.ToSingle(parts[2]);
            straifR = System.Convert.ToSingle(parts[3]);
            turnL = System.Convert.ToSingle(parts[4]);
            turnR = System.Convert.ToSingle(parts[5]);

            for (int i = 0; i < NUM_ITEMS; i++)
            {
                items[i] = System.Convert.ToBoolean(parts[i + 6]);
            }

            dropItem = System.Convert.ToBoolean(parts[16]);
            shoot = System.Convert.ToBoolean(parts[17]);
            pause = System.Convert.ToBoolean(parts[18]);
            relativeMovement = System.Convert.ToBoolean(parts[19]);
        }
        else
        {
            throw new System.Exception(parts.Length.ToString() + " is the wrong number of parts needed to make a PlayerInput: " + inputInfo);
        }
    }

    public override string ToString()
    {
        string toReturn = "";

        toReturn += forward.ToString() + " ";
        toReturn += backward.ToString() + " ";
        toReturn += straifL.ToString() + " ";
        toReturn += straifR.ToString() + " ";
        toReturn += turnL.ToString() + " ";
        toReturn += turnR.ToString() + " ";
        for (int i = 0; i < NUM_ITEMS; i++)
        {
            toReturn += items[i].ToString() + " ";
        }
        toReturn += dropItem.ToString() + " ";
        toReturn += shoot.ToString() + " ";
        toReturn += pause.ToString() + " ";
        toReturn += relativeMovement.ToString() + " ";

        return toReturn;
    }
}

public class PlayerControls
{
    //100,000 ~ 1 hour at 30fps
    private const int INITIAL_INPUT_SIZE = 100000;

    public Key forwardKey;
    public Key backwardKey;
    public Key straifLKey;
    public Key straifRKey;
    public Key turnLKey;
    public Key turnRKey;
    public Key[] itemKeys = new Key[PlayerInput.NUM_ITEMS];
    public Key dropItemKey;
    public Key shootKey;
    public Key pauseKey;

    private bool theRelativeMovement = false;
    public bool relativeMovement
    {
        get
        {
            return current.relativeMovement;
        }
    }

    public void setRelativeMovement(bool setTo)
    {
        theRelativeMovement = setTo;
    }

    #region Key Accessors
    //returns the value itself
    public float forward
    {
        get
        {
            return current.forward;
        }
    }
    public float backward
    {
        get
        {
            return current.backward;
        }
    }
    public float straifL
    {
        get
        {
            return current.straifL;
        }
    }
    public float straifR
    {
        get
        {
            return current.straifR;
        }
    }
    public float turnL
    {
        get
        {
            return current.turnL;
        }
    }
    public float turnR
    {
        get
        {
            return current.turnR;
        }
    }

    //if the key is pressed at all
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

    //returns the change in value
    public float Forward
    {
        get
        {
            return current.forward - previous.forward;
        }
    }
    public float Backward
    {
        get
        {
            return current.backward - previous.backward;
        }
    }
    public float StraifL
    {
        get
        {
            return current.straifL - previous.straifL;
        }
    }
    public float StraifR
    {
        get
        {
            return current.straifR - previous.straifR;
        }
    }
    public float TurnL
    {
        get
        {
            return current.turnL - previous.turnL;
        }
    }
    public float TurnR
    {
        get
        {
            return current.turnR - previous.turnR;
        }
    }

    //if the key was just pressed (not held down)
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

    private PlayerInput current = new PlayerInput(), previous = new PlayerInput();

    private List<PlayerInput> theInputs = new List<PlayerInput>(INITIAL_INPUT_SIZE);

    public List<PlayerInput> inputs
    {
        get
        {
            return theInputs;
        }
    }

    public void updateFromInput()
    {
        previous = current;
        current = new PlayerInput();
        current.forward = forwardKey.getAxis();
        current.backward = backwardKey.getAxis();
        current.straifL = straifLKey.getAxis();
        current.straifR = straifRKey.getAxis();
        current.turnL = turnLKey.getAxis();
        current.turnR = turnRKey.getAxis();

        for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
        {
            current.items[i] = itemKeys[i].isPressed();
        }
        current.dropItem = dropItemKey.isPressed();
        current.shoot = shootKey.isPressed();
        current.pause = pauseKey.isPressed();

        current.relativeMovement = theRelativeMovement;

        inputs.Add(current);
    }

    public void updateFromString(string updateInfo)
    {
        previous = current;
        current = new PlayerInput(updateInfo);

        inputs.Add(current);
    }

    public void clearInputs()
    {
        theInputs = new List<PlayerInput>(INITIAL_INPUT_SIZE);
        previous = new PlayerInput();
        current = new PlayerInput();
    }

    public void setFromString(string settings)
    {
        char[] seperators = new char[1];
        seperators[0] = ' ';

        string[] parts = settings.Split(seperators);

        if (parts.Length == 20)
        {
            forwardKey = new Key(System.Convert.ToInt32(parts[0]));
            backwardKey = new Key(System.Convert.ToInt32(parts[1]));
            straifLKey = new Key(System.Convert.ToInt32(parts[2]));
            straifRKey = new Key(System.Convert.ToInt32(parts[3]));
            turnLKey = new Key(System.Convert.ToInt32(parts[4]));
            turnRKey = new Key(System.Convert.ToInt32(parts[5]));

            for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
            {
                itemKeys[i] = new Key(System.Convert.ToInt32(parts[i + 6]));
            }

            dropItemKey = new Key(System.Convert.ToInt32(parts[16]));
            shootKey = new Key(System.Convert.ToInt32(parts[17]));
            pauseKey = new Key(System.Convert.ToInt32(parts[18]));
            theRelativeMovement = System.Convert.ToBoolean(parts[19]);
        }
        else
        {
            throw new System.Exception(parts.Length.ToString() + " is the wrong number of parts needed to set PlayerControls: " + settings);
        }
    }

    public override string ToString()
    {
        string toReturn = "";

        toReturn += forwardKey.getValue() + " ";
        toReturn += backwardKey.getValue() + " ";
        toReturn += straifLKey.getValue() + " ";
        toReturn += straifRKey.getValue() + " ";
        toReturn += turnLKey.getValue() + " ";
        toReturn += turnRKey.getValue() + " ";
        for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
        {
            toReturn += itemKeys[i].getValue() + " ";
        }
        toReturn += dropItemKey.getValue() + " ";
        toReturn += shootKey.getValue() + " ";
        toReturn += pauseKey.getValue() + " ";
        toReturn += relativeMovement.ToString() + " ";

        return toReturn;
    }
}

public class Controls
{
    public const int MAX_PLAYERS = 2;
    public const string CONTROLS_FILE = "\\Controls.options";

    private PlayerControls[] thePlayers = new PlayerControls[MAX_PLAYERS];

    public PlayerControls[] players
    {
        get
        {
            return thePlayers;
        }
    }

    private Controls()
    {
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            thePlayers[i] = new PlayerControls();
        }

        try
        {
            setControlsFromFile();
        }
        catch
        {

            setDefaultControls();
        }
    }

    public void setDefaultControls()
    {     
        players[0].forwardKey = new Key(KeyCode.W);
        players[0].backwardKey = new Key(KeyCode.S);
        players[0].straifLKey = new Key(KeyCode.LeftArrow);
        players[0].straifRKey = new Key(KeyCode.RightArrow);
        players[0].turnLKey = new Key(KeyCode.A);
        players[0].turnRKey = new Key(KeyCode.D);
        players[0].itemKeys[0] = new Key(KeyCode.Keypad0);
        players[0].itemKeys[1] = new Key(KeyCode.Keypad1);
        players[0].itemKeys[2] = new Key(KeyCode.Keypad2);
        players[0].itemKeys[3] = new Key(KeyCode.Keypad3);
        players[0].itemKeys[4] = new Key(KeyCode.Keypad4);
        players[0].itemKeys[5] = new Key(KeyCode.Keypad5);
        players[0].itemKeys[6] = new Key(KeyCode.Keypad6);
        players[0].itemKeys[7] = new Key(KeyCode.Keypad7);
        players[0].itemKeys[8] = new Key(KeyCode.Keypad8);
        players[0].itemKeys[9] = new Key(KeyCode.Keypad9);
        players[0].dropItemKey = new Key(KeyCode.LeftAlt);
        players[0].shootKey = new Key(KeyCode.Space);
        players[0].pauseKey = new Key(KeyCode.Escape);
        players[0].setRelativeMovement(true);

        if (MAX_PLAYERS > 1)
        {
            players[1].forwardKey = new Key(1, 2, false);
            players[1].backwardKey = new Key(1, 2, true);
            players[1].straifLKey = new Key(1, 1, false);
            players[1].straifRKey = new Key(1, 1, true);
            players[1].turnLKey = new Key(1, 4, false);
            players[1].turnRKey = new Key(1, 4, true);
            players[1].itemKeys[0] = new Key(KeyCode.Joystick1Button0);
            players[1].itemKeys[1] = new Key(KeyCode.Joystick1Button1);
            players[1].itemKeys[2] = new Key(KeyCode.Joystick1Button2);
            players[1].itemKeys[3] = new Key(KeyCode.Joystick1Button3);
            players[1].itemKeys[4] = new Key(KeyCode.Joystick1Button4);
            players[1].itemKeys[5] = new Key(KeyCode.Joystick1Button5);
            players[1].itemKeys[6] = new Key(1, 6, false);
            players[1].itemKeys[7] = new Key(1, 6, true);
            players[1].itemKeys[8] = new Key(1, 7, false);
            players[1].itemKeys[9] = new Key(1, 7, true);
            players[1].dropItemKey = new Key(1, 3, false);
            players[1].shootKey = new Key(1, 3, true);
            players[1].pauseKey = new Key(KeyCode.Joystick1Button7);
            players[1].setRelativeMovement(false);
        }

        //add default controls for players 3 and 4
    }

    public void updateFromInput()
    {
        foreach (PlayerControls item in players)
        {
            if (item != null)
            {
                item.updateFromInput();
            }
        }
    }

    public void updateFromFile(System.IO.StreamReader file)
    {
        foreach (PlayerControls item in players)
        {
            item.updateFromString(file.ReadLine());
        }
    }

    public void saveInputsToFile(System.IO.StreamWriter file)
    {
        for (int i = 0; i < players[0].inputs.Count; i++)
        {
            for (int j = 0; j < MAX_PLAYERS; j++)
            {
                file.WriteLine(players[j].inputs[i].ToString());
            }
        }
    }

    public void setControlsFromFile()
    {
        setControlsFromFile(CONTROLS_FILE);
    }

    public void setControlsFromFile(string fileName)
    {
        System.IO.StreamReader file = new System.IO.StreamReader(fileName);

        foreach (PlayerControls item in thePlayers)
        {
            item.setFromString(file.ReadLine());
        }

        file.Close();
    }

    public void saveControlsToFile()
    {
        saveControlsToFile(CONTROLS_FILE);
    }

    public void saveControlsToFile(string fileName)
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, false);

        foreach (PlayerControls item in thePlayers)
        {
            file.WriteLine(item.ToString());
        }

        file.Close();
    }

    private static Controls singleton;

    public static Controls get()
    {
        if (singleton == null)
        {
            singleton = new Controls();
        }

        return singleton;
    }
}
