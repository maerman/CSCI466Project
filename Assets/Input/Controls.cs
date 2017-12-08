// written by: Thomas Stewart, Michael Quinn
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Shane Barry, Diane Gregory

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Controls is a singleton class that represents the Controls for all Playrs. It has a PlayerControls
/// for each Player and it manages PlayerControls. 
/// </summary>
public class Controls
{
    public const int MAX_PLAYERS = 4;
    public const string CONTROLS_FILE = "Controls.options";

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

        //try to load the PlayerControls from a file and if there
        //is a problem, the set the PlayerControls Keys to default values
        try
        {
            loadControls();
        }
        catch
        {
            setDefaultControls();
        }
    }

    /// <summary>
    /// Sets the PlayerControl Keys and input settings to default values
    /// </summary>
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

    /// <summary>
    /// Updates the PlayerControls from the user's input
    /// </summary>
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

    /// <summary>
    /// Updates the PlayerControls from the given file
    /// </summary>
    /// <param name="file">file to update from</param>
    public void updateFromFile(System.IO.StreamReader file)
    {
        if (file.Peek() >= 0)
            Options.get().levelStatic = System.Convert.ToBoolean(file.ReadLine());
        foreach (PlayerControls item in players)
        {
            if (file.Peek() >= 0)
                item.updateFromString(file.ReadLine());
        }
    }

    /// <summary>
    /// Saves the input history in PlayerControls to the given file
    /// </summary>
    /// <param name="file">file to save to</param>
    public void saveInputsToFile(System.IO.StreamWriter file)
    {
        for (int i = 0; i < players[0].inputs.Count; i++)
        {
            file.WriteLine(Options.get().levelStatic.ToString());
            for (int j = 0; j < MAX_PLAYERS; j++)
            {
                file.WriteLine(players[j].inputs[i].ToString());
            }
        }
    }

    /// <summary>
    /// Loads the PlayerControls Keys and input options from the default file
    /// </summary>
    public void loadControls()
    {
        loadControls(CONTROLS_FILE);
    }

    /// <summary>
    /// Loads the PlayerControls Keys and input options from the given file
    /// </summary>
    /// <param name="fileName">file to load PlayerControls from</param>
    public void loadControls(string fileName)
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

    /// <summary>
    /// Saves the PlayerControls Keys and input options to the default file
    /// </summary>
    public void saveControls()
    {
        saveControls(CONTROLS_FILE);
    }

    /// <summary>
    /// Saves the PlayerControls Keys and input options to the given file
    /// </summary>
    /// <param name="fileName">file to save PlayerControls to</param>
    public void saveControls(string fileName)
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

    /// <summary>
    /// Clear the input history in all the PlayerControls
    /// </summary>
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
