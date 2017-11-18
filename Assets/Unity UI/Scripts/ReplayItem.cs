using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ReplayItem : MonoBehaviour
{
    //initilize in editor
    public Text date;
    public Text saveName;
    public Text level;
    public Text players;
    public Text difficulty;
    public Text pvp;
    public Toggle toggle;

    public string replayFile;


    public static GameObject getFromFile(string file)
    {
        StreamReader reader = null;

        try
        {
            string extention = Path.GetExtension(file);
            string tempSaveName, tempDate, tempLevel, tempPlayers, tempDifficulty, tempPvp;

            if (extention.Equals(Level.REPLAY_EXTENTION))
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

            GameObject obj = Instantiate(Resources.Load("ReplayItemPF"), Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
            ReplayItem save = obj.GetComponent<ReplayItem>();

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

    public bool loadReplay()
    {
        if (replayFile == null)
        {
            return false;
        }
        else
        {
            return (Level.loadReplay(replayFile) != null);
        }
    }
}
