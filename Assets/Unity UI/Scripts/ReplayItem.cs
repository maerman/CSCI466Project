// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// ReplayItem is a MonoBehaviour that controls ReplayItem toggles that each hold information
/// about one replay file and provides methods to create a Level fromthat replay file.
/// </summary>
public class ReplayItem : MonoBehaviour
{
    //initilized in editor
    public Text date;
    public Text saveName;
    public Text level;
    public Text players;
    public Text difficulty;
    public Text pvp;
    public Toggle toggle;

    public FileInfo replayFile;
    
    /// <summary>
    /// Static method that creates a ReplayItem from a file containing a replay.
    /// Returns the ReplayItem if it was created succesfully, null if not. 
    /// </summary>
    /// <param name="file">File containing the replay.</param>
    /// <returns>The created ReplayItem or null.</returns>
    public static GameObject getFromFile(FileInfo file)
    {
        StreamReader reader = null;

        try
        {
            string extention = file.Extension;
            string tempSaveName, tempDate, tempLevel, tempPlayers, tempDifficulty, tempPvp;

            //make sure the file is the correct type
            if (extention.Equals(Level.REPLAY_EXTENTION))
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

            //create the ReplayItem
            GameObject obj = Instantiate(Resources.Load("ReplayItemPF"), Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
            ReplayItem save = obj.GetComponent<ReplayItem>();

            //set the values in the ReplayItem with the read in Level settings,
            //filename and file modified time
            save.date.text = tempDate;
            save.saveName.text = tempSaveName;
            save.level.text = tempLevel;
            save.players.text = tempPlayers;
            save.difficulty.text = tempDifficulty;
            save.pvp.text = tempPvp;
            save.replayFile = file;

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
    /// Creates a Level in replay mode from the replay file represented by this ReplayItem
    /// </summary>
    /// <returns>True if the Level was created succesfully, false otherwise.</returns>
    public bool loadReplay()
    {
        if (replayFile == null)
        {
            return false;
        }
        else
        {
            return (Level.loadReplay(replayFile.FullName) != null);
        }
    }
}
