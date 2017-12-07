using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// PlayerInput represents the input of one Player for one update
/// </summary>
public class PlayerInput
{
    public const int NUM_ITEMS = 10;

    //input values
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

    //option values that modify input
    public bool relativeMovement;
    public bool turns;

    /// <summary>
    /// Create a PlayerInput with all values set to 0 or false. Values should be assigned to manually later. 
    /// </summary>
    public PlayerInput() { }

    /// <summary>
    /// Create a PlayerInput with its input and setting values set from the given string 
    /// (string format of PlayerInput.toString())
    /// </summary>
    /// <param name="inputInfo">fstring to set values from</param>
    public PlayerInput(string inputInfo)
    {
        //string for each value should be seperated by a space
        char[] seperators = new char[1];
        seperators[0] = ' ';
        string[] parts = inputInfo.Split(seperators);

        //convert the string to their coropsonding values
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
            relativeMovement = System.Convert.ToBoolean(parts[20]);
            turns = System.Convert.ToBoolean(parts[21]);
        }
        else
        {
            throw new System.Exception(parts.Length.ToString() + " is the wrong number of parts needed to make a PlayerInput: " + inputInfo);
        }
    }

    /// <summary>
    /// Creates a string of the input values and input setting values
    /// </summary>
    /// <returns>A string of the input values</returns>
    public override string ToString()
    {
        string toReturn = "";

        //make each value a string with a space seperating them
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
