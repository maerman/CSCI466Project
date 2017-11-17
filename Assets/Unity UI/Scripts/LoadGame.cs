using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    //initilzied in editor
    public GameObject gameSavesList;
    public ToggleGroup group;

    public System.Collections.Generic.List<SavedGameItem> saves;

   
    void Start()
    {
        refresh();
    }

    private void OnEnable()
    {
        refresh();
    }

    private void refresh()
    {
        saves.Clear();
        foreach (Transform t in gameSavesList.transform)
        {
            Destroy(t.gameObject);
        }

        System.IO.Directory.CreateDirectory(Level.SAVE_PATH);
        string[] filePaths = System.IO.Directory.GetFiles(Level.SAVE_PATH);

        System.Array.Sort(filePaths);

        foreach (string item in filePaths)
        {
            GameObject saveItem = SavedGameItem.getFromFile(item);
            if (saveItem != null)
            {
                saveItem.transform.SetParent(gameSavesList.transform);
                saveItem.transform.localScale = Vector3.one;
                saveItem.GetComponent<Toggle>().group = group;
                saveItem.GetComponent<Toggle>().isOn = false;

                saves.Add(saveItem.GetComponent<SavedGameItem>());
            }
        }
    }


    public void load()
    {
        foreach (SavedGameItem item in saves)
        {
            if (item.selected)
            {
                if (item.loadSave())
                {
                    GameStates.gameState = GameStates.GameState.Playing;
                }
                else
                {
                    //display error, problem loading save
                }
            }
        }

        //display error, no save selected
    }

    public void back()
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
