using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;

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

    private void OnEnable()
    {
        refresh();
    }

    public LoadGame(GameObject gameSavesList, ToggleGroup group, Color selectedColor, Color notSelectedColor, List<SavedGameItem> saves)
    {
        this.gameSavesList = gameSavesList;
        this.group = group;
        this.selectedColor = selectedColor;
        this.notSelectedColor = notSelectedColor;
        this.saves = saves;
    }

    private void refresh()
    {
        saves.Clear();
        foreach (Transform t in gameSavesList.transform)
        {
            Destroy(t.gameObject);
        }

        Directory.CreateDirectory(Level.SAVE_PATH);

        List<FileInfo> files = new DirectoryInfo(Level.SAVE_PATH).GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();

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


    public void load()
    {
        foreach (SavedGameItem item in saves)
        {
            if (item.toggle.isOn)
            {
                if (item.loadSave())
                {
                    GameStates.gameState = GameStates.GameState.Playing;
                    return;
                }
                else
                {
                    showErrorMenu("Problem loading save!");
                }
            }
        }

        showErrorMenu("No save selected.");
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
