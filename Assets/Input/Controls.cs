using UnityEngine;
using System.Collections.Generic;

public class PlayerInput
{
    public const int NUM_ITEMS = 10;

    public float forward;
    public float backward;
    public float straifL;
    public float straifR;
    public float turnUp;
    public float turnDown;
    public float turnL;
    public float turnR;
    public bool[] items = new bool[NUM_ITEMS];
    public bool pickupDrop;
    public bool shoot;

    public bool relativeMovement;
    public bool turns;

    public PlayerInput() { }

    public PlayerInput(string inputInfo)
    {
        char[] seperators = new char[1];
        seperators[0] = ' ';

        string[] parts = inputInfo.Split(seperators);

        if (parts.Length == 23)
        {
            forward = System.Convert.ToSingle(parts[0]);
            backward = System.Convert.ToSingle(parts[1]);
            straifL = System.Convert.ToSingle(parts[2]);
            straifR = System.Convert.ToSingle(parts[3]);
            turnUp = System.Convert.ToSingle(parts[4]);
            turnDown = System.Convert.ToSingle(parts[5]);
            turnL = System.Convert.ToSingle(parts[6]);
            turnR = System.Convert.ToSingle(parts[7]);

            for (int i = 0; i < NUM_ITEMS; i++)
            {
                items[i] = System.Convert.ToBoolean(parts[i + 8]);
            }

            pickupDrop = System.Convert.ToBoolean(parts[18]);
            shoot = System.Convert.ToBoolean(parts[19]);
            relativeMovement = System.Convert.ToBoolean(parts[21]);
            turns = System.Convert.ToBoolean(parts[22]);
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
        toReturn += turnUp.ToString() + " ";
        toReturn += turnDown.ToString() + " ";
        toReturn += turnL.ToString() + " ";
        toReturn += turnR.ToString() + " ";
        for (int i = 0; i < NUM_ITEMS; i++)
        {
            toReturn += items[i].ToString() + " ";
        }
        toReturn += pickupDrop.ToString() + " ";
        toReturn += shoot.ToString() + " ";
        toReturn += relativeMovement.ToString() + " ";
        toReturn += turns.ToString() + " ";

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
    public Key turnUpKey;
    public Key turnDownKey;
    public Key turnLKey;
    public Key turnRKey;
    public Key[] itemKeys = new Key[PlayerInput.NUM_ITEMS];
    public Key pickupDropKey;
    public Key shootKey;
    public Key pauseKey;
    public Key zoomInKey;
    public Key zoomOutKey;
    public bool mouseTurn = false;

    private bool previousPause;
    private bool previousZoomIn;
    private bool previousZoomOut;

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

    private bool theTurns = false;
    public bool turns
    {
        get
        {
            return current.turns;
        }
    }
    public void setTurns(bool setTo)
    {
        theTurns = setTo;
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
    public float turnUp
    {
        get
        {
            return current.turnUp;
        }
    }
    public float turnDown
    {
        get
        {
            return current.turnDown;
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
    public bool pickupDrop
    {
        get
        {
            return current.pickupDrop;
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
            previousPause = pauseKey.isPressed();
            return pauseKey.isPressed();
        }
    }
    public bool zoomIn
    {
        get
        {
            previousZoomIn = zoomInKey.isPressed();
            return zoomInKey.isPressed();
        }
    }
    public bool zoomOut
    {
        get
        {
            previousZoomOut = zoomOutKey.isPressed();
            return zoomOutKey.isPressed();
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
    public float TurnUp
    {
        get
        {
            return current.turnUp - previous.turnUp;
        }
    }
    public float TurnDown
    {
        get
        {
            return current.turnDown - previous.turnDown;
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
    public bool PickupDrop
    {
        get
        {
            return current.pickupDrop && !previous.pickupDrop;
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
            bool toReturn = pauseKey.isPressed() && !previousPause;
            previousPause = pauseKey.isPressed();
            return toReturn;
        }
    }
    public bool ZoomIn
    {
        get
        {
            bool toReturn = zoomInKey.isPressed() && !previousZoomIn;
            previousZoomIn = zoomInKey.isPressed();
            return toReturn;
        }
    }
    public bool ZoomOut
    {
        get
        {
            bool toReturn = zoomOutKey.isPressed() && !previousZoomOut;
            previousZoomOut = zoomOutKey.isPressed();
            return toReturn;
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

        if (mouseTurn)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 playerPos = Vector2.zero;

            if (Level.currentLevel != null)
            {
                for (int i = Controls.get().players.Length - 1; i >= 0; i--)
                {
                    PlayerControls current = Controls.get().players[i];
                    if (current == this)
                    {
                        playerPos = Level.currentLevel.players[i].position;
                    }
                }
            }

            mousePos = playerPos - mousePos;

            if (mousePos.x > 0)
            {
                current.turnL = mousePos.x;
                current.turnR = 0;
            }
            else
            {
                current.turnL = 0;
                current.turnR = -mousePos.x;
            }

            if (mousePos.y > 0)
            {
                current.turnDown = mousePos.y;
                current.turnUp = 0;
            }
            else
            {
                current.turnDown = 0;
                current.turnUp = -mousePos.y;
            }
        }
        else
        {
            current.turnUp = turnUpKey.getAxis();
            current.turnDown = turnDownKey.getAxis();
            current.turnL = turnLKey.getAxis();
            current.turnR = turnRKey.getAxis();
        }

        for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
        {
            current.items[i] = itemKeys[i].isPressed();
        }
        current.pickupDrop = pickupDropKey.isPressed();
        current.shoot = shootKey.isPressed();

        current.relativeMovement = theRelativeMovement;

        if (mouseTurn)
        {
            current.turns = false;
        }
        else
        {
            current.turns = theTurns;
        }

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

        if (parts.Length == 26)
        {
            forwardKey = new Key(System.Convert.ToInt32(parts[0]));
            backwardKey = new Key(System.Convert.ToInt32(parts[1]));
            straifLKey = new Key(System.Convert.ToInt32(parts[2]));
            straifRKey = new Key(System.Convert.ToInt32(parts[3]));
            turnUpKey = new Key(System.Convert.ToInt32(parts[4]));
            turnDownKey = new Key(System.Convert.ToInt32(parts[5]));
            turnLKey = new Key(System.Convert.ToInt32(parts[6]));
            turnRKey = new Key(System.Convert.ToInt32(parts[7]));

            for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
            {
                itemKeys[i] = new Key(System.Convert.ToInt32(parts[i + 8]));
            }

            pickupDropKey = new Key(System.Convert.ToInt32(parts[18]));
            shootKey = new Key(System.Convert.ToInt32(parts[19]));
            pauseKey = new Key(System.Convert.ToInt32(parts[20]));
            zoomInKey = new Key(System.Convert.ToInt32(parts[21]));
            zoomOutKey = new Key(System.Convert.ToInt32(parts[22]));
            theRelativeMovement = System.Convert.ToBoolean(parts[23]);
            theTurns = System.Convert.ToBoolean(parts[24]);
            mouseTurn = System.Convert.ToBoolean(parts[25]);
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
        toReturn += turnUpKey.getValue() + " ";
        toReturn += turnDownKey.getValue() + " ";
        toReturn += turnLKey.getValue() + " ";
        toReturn += turnRKey.getValue() + " ";
        for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
        {
            toReturn += itemKeys[i].getValue() + " ";
        }
        toReturn += pickupDropKey.getValue() + " ";
        toReturn += shootKey.getValue() + " ";
        toReturn += pauseKey.getValue() + " ";
        toReturn += zoomInKey.getValue() + " ";
        toReturn += zoomOutKey.getValue() + " ";
        toReturn += theRelativeMovement.ToString() + " ";
        toReturn += theTurns.ToString() + " ";
        toReturn += mouseTurn.ToString();

        return toReturn;
    }
}

public class Controls
{
    public const int MAX_PLAYERS = 4;
    public const string CONTROLS_FILE = "Controls.options";

    public bool staticLevel = false;

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
        Debug.Log("Setting default controls");

        players[0].forwardKey = new Key(KeyCode.W);
        players[0].backwardKey = new Key(KeyCode.S);
        players[0].straifLKey = new Key(KeyCode.A);
        players[0].straifRKey = new Key(KeyCode.D);
        players[0].turnUpKey = new Key(KeyCode.UpArrow);
        players[0].turnDownKey = new Key(KeyCode.DownArrow);
        players[0].turnLKey = new Key(KeyCode.LeftArrow);
        players[0].turnRKey = new Key(KeyCode.RightArrow);
        players[0].itemKeys[0] = new Key(KeyCode.Mouse1);
        players[0].itemKeys[1] = new Key(KeyCode.Mouse2);
        players[0].itemKeys[2] = new Key(KeyCode.Mouse3);
        players[0].itemKeys[3] = new Key(KeyCode.Mouse4);
        players[0].itemKeys[4] = new Key(KeyCode.E);
        players[0].itemKeys[5] = new Key(KeyCode.R);
        players[0].itemKeys[6] = new Key(KeyCode.T);
        players[0].itemKeys[7] = new Key(KeyCode.F);
        players[0].itemKeys[8] = new Key(KeyCode.G);
        players[0].itemKeys[9] = new Key(KeyCode.C);
        players[0].pickupDropKey = new Key(KeyCode.Space);
        players[0].shootKey = new Key(KeyCode.Mouse0);
        players[0].pauseKey = new Key(KeyCode.Escape);
        players[0].zoomInKey = new Key(KeyCode.LeftShift);
        players[0].zoomOutKey = new Key(KeyCode.LeftControl);
        players[0].setRelativeMovement(false);
        players[0].setTurns(true);
        players[0].mouseTurn = true;

        if (MAX_PLAYERS > 1)
        {
            players[1].forwardKey = new Key(1, 2, false);
            players[1].backwardKey = new Key(1, 2, true);
            players[1].straifLKey = new Key(1, 1, false);
            players[1].straifRKey = new Key(1, 1, true);
            players[1].turnUpKey = new Key(1, 5, false);
            players[1].turnDownKey = new Key(1, 5, true);
            players[1].turnLKey = new Key(1, 4, false);
            players[1].turnRKey = new Key(1, 4, true);
            players[1].itemKeys[0] = new Key(KeyCode.Joystick1Button0);
            players[1].itemKeys[1] = new Key(KeyCode.Joystick1Button1);
            players[1].itemKeys[2] = new Key(KeyCode.Joystick1Button2);
            players[1].itemKeys[3] = new Key(KeyCode.Joystick1Button3);
            players[1].itemKeys[4] = new Key(1, 6, false);
            players[1].itemKeys[5] = new Key(1, 6, true);
            players[1].itemKeys[6] = new Key(1, 7, false);
            players[1].itemKeys[7] = new Key(1, 7, true);
            players[1].itemKeys[8] = new Key(KeyCode.Joystick1Button8);
            players[1].itemKeys[9] = new Key(KeyCode.Joystick1Button9);
            players[1].pickupDropKey = new Key(1, 3, false);
            players[1].shootKey = new Key(1, 3, true);
            players[1].pauseKey = new Key(KeyCode.Joystick1Button7);
            players[1].zoomInKey = new Key(KeyCode.Joystick1Button4);
            players[1].zoomOutKey = new Key(KeyCode.Joystick1Button5);
            players[1].setRelativeMovement(false);
            players[1].setTurns(false);
            players[1].mouseTurn = false;
        }
        if (MAX_PLAYERS > 2)
        {
            players[2].forwardKey = new Key(2, 2, false);
            players[2].backwardKey = new Key(2, 2, true);
            players[2].straifLKey = new Key(2, 1, false);
            players[2].straifRKey = new Key(2, 1, true);
            players[2].turnUpKey = new Key(2, 5, false);
            players[2].turnDownKey = new Key(2, 5, true);
            players[2].turnLKey = new Key(2, 4, false);
            players[2].turnRKey = new Key(2, 4, true);
            players[2].itemKeys[0] = new Key(KeyCode.Joystick2Button0);
            players[2].itemKeys[1] = new Key(KeyCode.Joystick2Button1);
            players[2].itemKeys[2] = new Key(KeyCode.Joystick2Button2);
            players[2].itemKeys[3] = new Key(KeyCode.Joystick2Button3);
            players[2].itemKeys[4] = new Key(2, 6, false);
            players[2].itemKeys[5] = new Key(2, 6, true);
            players[2].itemKeys[6] = new Key(2, 7, false);
            players[2].itemKeys[7] = new Key(2, 7, true);
            players[2].itemKeys[8] = new Key(KeyCode.Joystick2Button8);
            players[2].itemKeys[9] = new Key(KeyCode.Joystick2Button9);
            players[2].pickupDropKey = new Key(2, 3, false);
            players[2].shootKey = new Key(2, 3, true);
            players[2].pauseKey = new Key(KeyCode.Joystick2Button7);
            players[2].zoomInKey = new Key(KeyCode.Joystick2Button4);
            players[2].zoomOutKey = new Key(KeyCode.Joystick2Button5);
            players[2].setRelativeMovement(false);
            players[2].setTurns(false);
            players[2].mouseTurn = false;
        }
        if (MAX_PLAYERS > 3)
        {
            players[3].forwardKey = new Key(3, 2, false);
            players[3].backwardKey = new Key(3, 2, true);
            players[3].straifLKey = new Key(3, 1, false);
            players[3].straifRKey = new Key(3, 1, true);
            players[3].turnUpKey = new Key(3, 5, false);
            players[3].turnDownKey = new Key(3, 5, true);
            players[3].turnLKey = new Key(3, 4, false);
            players[3].turnRKey = new Key(3, 4, true);
            players[3].itemKeys[0] = new Key(KeyCode.Joystick3Button0);
            players[3].itemKeys[1] = new Key(KeyCode.Joystick3Button1);
            players[3].itemKeys[2] = new Key(KeyCode.Joystick3Button2);
            players[3].itemKeys[3] = new Key(KeyCode.Joystick3Button3);
            players[3].itemKeys[4] = new Key(3, 6, false);
            players[3].itemKeys[5] = new Key(3, 6, true);
            players[3].itemKeys[6] = new Key(3, 7, false);
            players[3].itemKeys[7] = new Key(3, 7, true);
            players[3].itemKeys[8] = new Key(KeyCode.Joystick3Button8);
            players[3].itemKeys[9] = new Key(KeyCode.Joystick3Button9);
            players[3].pickupDropKey = new Key(3, 3, false);
            players[3].shootKey = new Key(3, 3, true);
            players[3].pauseKey = new Key(KeyCode.Joystick3Button7);
            players[3].zoomInKey = new Key(KeyCode.Joystick3Button4);
            players[3].zoomOutKey = new Key(KeyCode.Joystick3Button5);
            players[3].setRelativeMovement(false);
            players[3].setTurns(false);
            players[3].mouseTurn = false;
        }
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
        staticLevel = System.Convert.ToBoolean(file.ReadLine());
        foreach (PlayerControls item in players)
        {
            item.updateFromString(file.ReadLine());
        }
    }

    public void saveInputsToFile(System.IO.StreamWriter file)
    {
        for (int i = 0; i < players[0].inputs.Count; i++)
        {
            file.WriteLine(staticLevel.ToString());
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
        System.IO.StreamReader file = null;
        try
        {
            file = new System.IO.StreamReader(fileName);

            foreach (PlayerControls item in thePlayers)
            {
                item.setFromString(file.ReadLine());
            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Probelm loading controls from file.", e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void saveControlsToFile()
    {
        saveControlsToFile(CONTROLS_FILE);
    }

    public void saveControlsToFile(string fileName)
    {
        System.IO.StreamWriter file = null;
        try
        {
            file = new System.IO.StreamWriter(fileName, false);

            foreach (PlayerControls item in thePlayers)
            {
                file.WriteLine(item.ToString());
            }

            file.Close();
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Probelm saving controls to file.", e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void clearInputs()
    {
        foreach (PlayerControls item in thePlayers)
        {
            item.clearInputs();
        }
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
