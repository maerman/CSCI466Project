// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;

/// <summary>
/// LoadGame is a MonoBehavior that controls the Load Game menu and has methods 
/// that are called when the menu's buttons are pressed. It displays to the user 
/// the saved game files that are locally saved. 
/// </summary>
public class LoadGame : MonoBehaviour
{
    //initilzied in editor
    public GameObject gameSavesList;
    public ToggleGroup group;
    public Color selectedColor = Color.green;
    public Color notSelectedColor = Color.gray;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    public List<SavedGameItem> saves = new List<SavedGameItem>();

   
    void Start()
    {
        refresh();
    }

    private void OnGUI()
    {
        //When a SavedGameItem is clicked, highlight that one
        foreach (SavedGameItem item in saves)
        {
            ColorBlock colors = item.toggle.colors;
            if (item.toggle.isOn)
            {
                
                colors.normalColor = selectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = selectedColor;
            }
            else
            {
                colors.normalColor = notSelectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = notSelectedColor;
            }
            item.toggle.colors = colors;
        }
    }

    /// <summary>
    /// Clears and repopulats the list of saved games
    /// </summary>
    private void refresh()
    {
        //clear the list
        saves.Clear();
        foreach (Transform t in gameSavesList.transform)
        {
            Destroy(t.gameObject);
        }

        //find the saved games in the save directory
        Directory.CreateDirectory(Level.SAVE_PATH);
        List<FileInfo> files = new DirectoryInfo(Level.SAVE_PATH).GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();

        //create a SavedGameItem for each saved game and add it to the saved game list
        foreach (FileInfo item in files)
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

        OnGUI();
    }

    private void OnEnable()
    {
        refresh();
    }

    /// <summary>
    /// creates a SavedGameItem for each saved game and add it to the saved game list
    /// </summary>
    public void load()
    {
        //find the selected saved gae and try to load it
        foreach (SavedGameItem item in saves)
        {
            if (item.toggle.isOn)
            {
                //if load was successful, change the screen to Playing
                if (item.loadSave())
                {
                    GameStates.gameState = GameStates.GameState.Playing;
                    return;
                }
                //if it wasn't successful, show an error
                else
                {
                    showErrorMenu("Problem loading save!");
                }
            }
        }

        //if no saved game was selected, show an error
        showErrorMenu("No save selected.");
    }

    /// <summary>
    /// method the back button calls, changes the screen to the Main menu
    /// </summary>
    public void back()
    {
        GameStates.gameState = GameStates.GameState.Main;
    }

    public void showErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
