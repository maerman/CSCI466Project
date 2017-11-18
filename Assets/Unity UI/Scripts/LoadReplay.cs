using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

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

    private void OnEnable()
    {
        refresh();
    }

    private void refresh()
    {
        replays.Clear();
        foreach (Transform t in replayList.transform)
        {
            Destroy(t.gameObject);
        }

        System.IO.Directory.CreateDirectory(Level.SAVE_PATH);
        string[] filePaths = System.IO.Directory.GetFiles(Level.SAVE_PATH);

        System.Array.Sort(filePaths);

        foreach (string item in filePaths)
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


    public void load()
    {
        foreach (ReplayItem item in replays)
        {
            if (item.toggle.isOn)
            {
                if (item.loadReplay())
                {
                    GameStates.gameState = GameStates.GameState.Replay;
                    return;
                }
                else
                {
                    showErrorMenu("Problem loading replay!");
                }
            }
        }

        showErrorMenu("No replay selected.");
    }

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
