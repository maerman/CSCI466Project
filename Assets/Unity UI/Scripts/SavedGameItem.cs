using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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


    public static GameObject getFromFile(FileInfo file)
    {
        StreamReader reader = null;

        try
        {
            string tempSaveName, tempDate, tempLevel, tempPlayers, tempDifficulty, tempPvp;

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
            return (Level.loadLevel(saveFile.FullName) != null);
        }
    }
}
