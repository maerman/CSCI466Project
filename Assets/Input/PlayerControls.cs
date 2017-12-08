// written by: Thomas Stewart, Michael Quinn
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Shane Barry, Diane Gregory

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents the inputs and controls for one Player. This keeps track of the 
/// Players current controls, current inputs and past inputs. 
/// </summary>
public class PlayerControls
{
    //60,000 ~ 20 mins at 50fps
    private const int INITIAL_INPUT_SIZE = 100000;

    //The Player's controls
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

    //used to determine if the corrosponding keys just started to be pressed
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
    //all lowercase accessors return the value itself
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

    //these lowercase accessors return if the key is pressed at all
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

    //these upppercase accessors return the change in value
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

    //these uppercase accessors return if the key was just pressed (not held down)
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

    //current and previous inputs of the Player
    private PlayerInput current = new PlayerInput(), previous = new PlayerInput();

    //all inputs of the Player since the start of the Level
    private List<PlayerInput> theInputs = new List<PlayerInput>(INITIAL_INPUT_SIZE);

    public List<PlayerInput> inputs
    {
        get
        {
            return theInputs;
        }
    }

    /// <summary>
    /// Creates a new PlayerInput and sets its values from the corrosponding Keys current value
    /// this new PlayerInput is set as the current input
    /// </summary>
    public void updateFromInput()
    {
        previous = current;
        current = new PlayerInput();

        //set movement values from the corrosponding Key' current values
        current.forward = forwardKey.getAxis();
        current.backward = backwardKey.getAxis();
        current.straifL = straifLKey.getAxis();
        current.straifR = straifRKey.getAxis();

        //if mouseTurn option is enabled, set the turn values from the cursor's location
        //relative to the Player's position
        if (mouseTurn)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 playerPos = Vector2.zero;

            if (Level.current != null)
            {
                for (int i = Controls.get().players.Length - 1; i >= 0; i--)
                {
                    PlayerControls current = Controls.get().players[i];
                    if (current == this)
                    {
                        playerPos = Level.current.players[i].position;
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
        //if the mouseTurn option is not enabled, set turn values from the corrosponding Keys' current values
        else
        {
            current.turnUp = turnUpKey.getAxis();
            current.turnDown = turnDownKey.getAxis();
            current.turnL = turnLKey.getAxis();
            current.turnR = turnRKey.getAxis();
        }

        //set item values from the corrosponding Keys' current values
        for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
        {
            current.items[i] = itemKeys[i].isPressed();
        }

        //set other values from the corrosponding Keys' current values
        current.pickupDrop = pickupDropKey.isPressed();
        current.shoot = shootKey.isPressed();

        //set the input settings options from the saved options
        current.relativeMovement = theRelativeMovement;
        if (mouseTurn)
        {
            //if mouseTurn is set, turns is always false since it will always point at the cursor
            current.turns = false;
        }
        else
        {
            current.turns = theTurns;
        }

        //add the current PlayerInput to the history of inputs
        inputs.Add(current);
    }

    /// <summary>
    /// Creates a new PlayerInput with the given string
    /// this new PlayerInput is set as the current input
    /// </summary>
    /// <param name="updateInfo">string to create the PlayerInput with</param>
    public void updateFromString(string updateInfo)
    {
        previous = current;
        current = new PlayerInput(updateInfo);

        inputs.Add(current);
    }

    /// <summary>
    /// Resets the input history, current and previous inputs
    /// </summary>
    public void clearInputs()
    {
        theInputs = new List<PlayerInput>(INITIAL_INPUT_SIZE);
        previous = new PlayerInput();
        current = new PlayerInput();
    }

    /// <summary>
    /// Sets the controls from the given string
    /// </summary>
    /// <param name="settings">set the controls from</param>
    public void setFromString(string settings)
    {
        //string for each value should be seperated by a space
        char[] seperators = new char[1];
        seperators[0] = ' ';
        string[] parts = settings.Split(seperators);

        //make the control Keys from the strings
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

    /// <summary>
    /// Creates a string of the control Keys 
    /// </summary>
    /// <returns>A string of the control Keys</returns>
    public override string ToString()
    {
        string toReturn = "";

        //make each Key a string with a space seperating them
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
