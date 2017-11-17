using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SavedGameItem : MonoBehaviour
{

    //initilize in editor
    public Text date;
    public Text saveName;
    public Text level;
    public Text players;
    public Text difficulty;
    public Text pvp;
    public Toggle saveItem;

    public string saveFile;

    public bool selected
    {
        get
        {
            return saveItem.isOn;
        }
    }

    public static GameObject getFromFile(string file)
    {
        StreamReader reader = null;

        try
        {
            string extention = Path.GetExtension(file);
            string tempSaveName, tempDate, tempLevel, tempPlayers, tempDifficulty, tempPvp;

            if (extention.Equals(Level.AUTO_SAVE_EXTENTION))
            {
                tempSaveName = "Auto Save";
            }
            else if (extention.Equals(Level.SAVE_EXTENTION))
            {
                tempSaveName = Path.GetFileNameWithoutExtension(file);
            }
            else
            {
                return null;
            }

            tempDate = File.GetLastWriteTime(file).ToString();

            reader = new System.IO.StreamReader(file);

            tempLevel = reader.ReadLine();
            tempPlayers = reader.ReadLine();
            tempDifficulty = reader.ReadLine();
            tempPvp = reader.ReadLine();

            GameObject obj = Instantiate(Resources.Load("SaveGameItemPF"), Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
            SavedGameItem save = obj.GetComponent<SavedGameItem>();

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

    public bool loadSave()
    {
        if (saveFile == null)
        {
            return false;
        }
        else
        {
            return (Level.loadLevel(saveFile) != null);
        }
    }
}
