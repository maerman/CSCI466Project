using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Message
{
    private string theText;
    public int duration;

    public Message(string text, int durationFrames)
    {
        this.theText = text;
        duration = durationFrames;
    }

    public Message(string text, float durationSecs)
    {
        this.theText = text;
        if (Level.currentLevel == null)
        {
            duration = (int)(durationSecs * 50);
        }
        else
        {
            duration = (int)(durationSecs * Level.currentLevel.updatesPerSec);
        }
    }

    public string text
    {
        get
        {
            if (theText == null)
            {
                return "";
            }
            else
            {
                return theText;
            }
        }
        set
        {
            theText = value;
        }
    }
}


public class IngameInterface : MonoBehaviour
{
    //Initilzed in editor
    public Text durationText;
    public Text numberName;
    public Text progress;
    public Text messages;
    public List<GameObject> playerPanels;


    private static IngameInterface current;
    private List<Message> messageList = new List<Message>();
    
    public static void displayMessage(Message message)
    {
        if (current == null)
        {
            throw new System.Exception("IngameInterface not created when calling displayMessage()");
        }
        else
        {
            current.messageList.Add(message);
        }
    }

    public static void displayMessage(string message, int durationFrames)
    {
        displayMessage(new Message(message, durationFrames));
    }

    public static void displayMessage(string message, float durationSecs)
    {
        displayMessage(new Message(message, durationSecs));
    }

    void Start()
    {
        current = this;
    }

    void Update()
    {
         
        if (Level.currentLevel != null)
        {
            Level level = Level.currentLevel;

            System.TimeSpan duration = level.duration;
            durationText.text = duration.ToString(@"hh\:mm\:ss");

            numberName.text = level.levelNumber.ToString() + ":" + level.levelName;

            progress.text = level.progress;

            string messageText = "";
            for (int i = messageList.Count - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    messageText += messageList[i].text + "\n";
                }
                else
                {
                    messageText += messageList[i].text;
                }

                messageList[i].duration--;
                if (messageList[i].duration <= 0)
                {
                    messageList.RemoveAt(i);
                }
            }
            messages.text = messageText;

            for (int i = 0; i < playerPanels.Count; i++)
            {
                if (level.players.Length <= i || level.players[i] == null || !level.players[i].enabled)
                {
                    playerPanels[i].SetActive(false);
                }
                else
                {
                    playerPanels[i].SetActive(true);
                    playerPanels[i].GetComponent<PlayerPanel>().player = level.players[i];
                }
            }
        }      
    }

    private void OnDestroy()
    {
        current = null;
    }
}
