// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using DG.Tweening;

/// <summary>
/// LoadReplay is a MonoBehavior that controls the Load Replay menu and has methods 
/// that are called when the menu's buttons are pressed. It displays to the user 
/// the replay files that are locally saved. 
/// </summary>
public class LoadReplay : MonoBehaviour
{
    //initilzied in editor
    public GameObject replayList;
    public ToggleGroup group;
    public Color selectedColor = Color.green;
    public Color notSelectedColor = Color.gray;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;

    public List<ReplayItem> replays = new List<ReplayItem>();

    void Start()
    {
        refresh();
    }

    private void OnGUI()
    {
        //When a ReplayItem is clicked, highlight that one
        foreach (ReplayItem item in replays)
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
    /// Clears and repopulats the list of replays
    /// </summary>
    private void refresh()
    {
        //clear the list
        replays.Clear();
        foreach (Transform t in replayList.transform)
        {
            Destroy(t.gameObject);
        }

        //find the replays in the save directory
        System.IO.Directory.CreateDirectory(Level.SAVE_PATH);
        List<FileInfo> files = new DirectoryInfo(Level.SAVE_PATH).GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();

        //create a ReplayItem for each replay and add it to the replay list
        foreach (FileInfo item in files)
        {
            GameObject replay = ReplayItem.getFromFile(item);
            if (replay != null)
            {
                replay.transform.SetParent(replayList.transform);
                replay.transform.localScale = Vector3.one;
                replay.GetComponent<Toggle>().group = group;
                replay.GetComponent<Toggle>().isOn = false;

                replays.Add(replay.GetComponent<ReplayItem>());
            }
        }

        OnGUI();
    }

    private void OnEnable()
    {
        refresh();
    }

    /// <summary>
    /// creates a ReplayItem for each replay and add it to the replay list
    /// </summary>
    public void load()
    {
        //find the selected replay and try to load it
        foreach (ReplayItem item in replays)
        {
            if (item.toggle.isOn)
            {
                //if load was successful, change the screen to Replay
                if (item.loadReplay())
                {
                    GameStates.gameState = GameStates.GameState.Replay;
                    return;
                }
                //if it wasn't successful, show an error
                else
                {
                    showErrorMenu("Problem loading replay!");
                }
            }
        }

        //if no replay was selected, show an error
        showErrorMenu("No replay selected.");
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
