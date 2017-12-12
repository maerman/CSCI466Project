// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Diane Gregory, Metin Erman

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// PlayerPanel is MonoBehavior that controls the different PlayerPanels.
/// It updates parts of the panel baised on its Player's status.
/// </summary>
public class PlayerPanel : MonoBehaviour
{
    //Initilize in editor
    public Text healthText;
    public Text armorText;
    public Image healthBar;
    public Image backHealthBar;
    public List<Image> items;
    public List<Text> itemKeys;

    private Player thePlayer;
    public Player player
    {
        get
        {
            return thePlayer;
        }
        set
        {
            thePlayer = value;
        }
    }
    
    void Start()
    {

    }

    
    void Update()
    {
        if (thePlayer != null && Controls.get().players[player.playerNum] != null)
        {
            PlayerControls controls = Controls.get().players[player.playerNum];

            //update the health and armor text displays baised on the Player's health and armor
            healthText.text = System.Math.Round(player.health, 2).ToString() + " / " + player.maxHealth.ToString();
            armorText.text = player.armor.ToString();

            //update the scale of the front and back of the healthbar baised on 
            //the portion of the Player's health is remaining and lost
            Vector3 scale = healthBar.transform.localScale;
            if (player.maxHealth > 0)
                scale.x = player.health / player.maxHealth;
            healthBar.transform.localScale = scale;
            scale.x = 1 - scale.x;
            backHealthBar.transform.localScale = scale;

            if (player.items.Length != items.Count || controls.itemKeys.Length != itemKeys.Count || itemKeys.Count != items.Count)
            {
                throw new System.Exception("Items length not correct in PlayerPanel update.");
            }

            //update the Item displays baised on the Player's Item list and Item control Keys
            for (int i = 0; i < player.items.Length; i++)
            {
                //if the Item slot is empty, don't disply an image
                if (player.items[i] == null)
                {
                    items[i].gameObject.SetActive(false);
                }
                //display the Item's image and color if it exists
                else
                {
                    items[i].gameObject.SetActive(true);

                    items[i].sprite = player.items[i].sprite;
                    items[i].color = player.items[i].color;
                }

                //display the Key the Item slot is assigned
                itemKeys[i].text = controls.itemKeys[i].toShortString();
            }
        }
    }
}
