// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// SavedGameItem is a MonoBehaviour that controls SavedGameItem toggles that each hold information
/// about one saved game file and provides methods to create a Level from that saved game file.
/// </summary>
public class SavedGameItem : MonoBehaviour
{
    //initilized in editor
    public Text date;
    public Text saveName;
    public Text level;
    public Text players;
    public Text difficulty;
    public Text pvp;
    public Toggle toggle;

    public FileInfo saveFile;

    /// <summary>
    /// Static method that creates a SavedGameItem from a file containing a saved game.
    /// Returns the SavedGameItem if it was created succesfully, null if not. 
    /// </summary>
    /// <param name="file">File containing the saved game.</param>
    /// <returns>The created SavedGameItem or null.</returns>
    public static GameObject getFromFile(FileInfo file)
    {
        StreamReader reader = null;

        try
        {
            string tempSaveName, tempDate, tempLevel, tempPlayers, tempDifficulty, tempPvp;

            //make sure the file is the correct type
            if (file.Extension.Equals(Level.AUTO_SAVE_EXTENTION))
            {
                tempSaveName = "Auto Save";
            }
            else if (file.Extension.Equals(Level.SAVE_EXTENTION))
            {
                tempSaveName = Path.GetFileNameWithoutExtension(file.Name);
            }
            else
            {
                return null;
            }

            tempDate = file.LastWriteTime.ToString();

            reader = new System.IO.StreamReader(file.FullName);

            //read in the Levels settings
            tempLevel = reader.ReadLine();
            tempPlayers = reader.ReadLine();
            tempDifficulty = reader.ReadLine();
            tempPvp = reader.ReadLine();

            //create the SavedGameItem
            GameObject obj = Instantiate(Resources.Load("SaveGameItemPF"), Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
            SavedGameItem save = obj.GetComponent<SavedGameItem>();

            //set the values in the SavedGameItem with the read in Level settings,
            //filename and file modified time
            save.date.text = tempDate;
            save.saveName.text = tempSaveName;
            save.level.text = tempLevel;
            save.players.text = tempPlayers;
            save.difficulty.text = tempDifficulty;
            save.pvp.text = tempPvp;
            save.saveFile = file;

            return obj;
        }
        catch
        {
            return null;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
    }

    /// <summary>
    /// Creates a Level in play mode from the saved game file represented by this SavedGameItem
    /// </summary>
    /// <returns>True if the Level was created succesfully, false otherwise.</returns>
    public bool loadSave()
    {
        if (saveFile == null)
        {
            return false;
        }
        else
        {
            return (Level.loadLevel(saveFile.FullName) != null);
        }
    }
}
