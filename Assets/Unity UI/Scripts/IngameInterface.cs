// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Diane Gregory, Metin Erman

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Message is a simple class that contains a string and the number of
/// updates it is to be displayed.
/// </summary>
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

        //convert the seconds into updates
        if (Level.current == null)
        {
            duration = (int)(durationSecs * 50);
        }
        else
        {
            duration = (int)(durationSecs * Level.current.updatesPerSec);
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

/// <summary>
/// IngameInterface is a MonoBehavior that controls the GameInterface Canvas. 
/// It updates parts of the interface baised on the Level's and Players' status.
/// </summary>
public class IngameInterface : MonoBehaviour
{
    //Initilzed in editor
    public Text durationText;
    public Text numberName;
    public Text progress;
    public Text messages;
    public List<GameObject> playerPanels;

    //These static members and methods allow other classes to create Messages 
    //to be displayed without having a reference to an instance of this.
    private static IngameInterface current;
    private List<Message> messageList = new List<Message>();
    public static void displayMessage(Message message)
    {
        if (current == null)
        {
            Debug.Log("IngameInterface not created when calling displayMessage(), discarding message: " 
                + message.text);
        }
        else
        {
            current.messageList.Add(message);
        }
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
         
        if (Level.current != null)
        {
            Level level = Level.current;

            //update the display of the current Level's duration
            durationText.text = level.duration.ToString(@"hh\:mm\:ss");
            durationText.applyAlpha(Options.get().ingameInterfaceAlpha);

            //update the display of the current Level's name
            numberName.text = level.levelNumber.ToString() + ":" + level.levelName;
            numberName.applyAlpha(Options.get().ingameInterfaceAlpha);

            //update the display of the progress though the current Level
            progress.text = level.progress;
            progress.applyAlpha(Options.get().ingameInterfaceAlpha);

            //put each message in the messageList into one string with each on its own line
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

                //remove any message that have run out of updates to display them
                messageList[i].duration--;
                if (messageList[i].duration <= 0)
                {
                    messageList.RemoveAt(i);
                }
            }

            //update the display of messages
            messages.text = messageText;
            messages.applyAlpha(Options.get().ingameInterfaceAlpha);

            for (int i = 0; i < playerPanels.Count; i++)
            {
                //deactivate playerPanels for dead or non-existant Players
                if (level.players.Length <= i || level.players[i] == null || !level.players[i].active)
                {
                    playerPanels[i].SetActive(false);
                }
                //activate playerPanels for active Players, make sure each playerPanel has its Player set correctly
                else
                {
                    playerPanels[i].SetActive(true);
                    playerPanels[i].GetComponent<PlayerPanel>().player = level.players[i];
                    playerPanels[i].applyAlphaToChildren(Options.get().ingameInterfaceAlpha);
                }
            }
        }      
    }

    private void OnDestroy()
    {
        if (current == this)
            current = null;
    }
}
