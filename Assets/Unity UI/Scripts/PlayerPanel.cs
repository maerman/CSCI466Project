using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

            healthText.text = player.health.ToString() + " / " + player.maxHealth.ToString();

            armorText.text = player.armor.ToString();

            Vector3 scale = healthBar.transform.localScale;
            scale.x = player.health / player.maxHealth;
            healthBar.transform.localScale = scale;
            scale.x = 1 - scale.x;
            backHealthBar.transform.localScale = scale;

            if (player.items.Length != items.Count || controls.itemKeys.Length != itemKeys.Count || itemKeys.Count != items.Count)
            {
                throw new System.Exception("Items length not correct in PlayerPanel update.");
            }

            for (int i = 0; i < player.items.Length; i++)
            {
                if (player.items[i] == null)
                {
                    items[i].gameObject.SetActive(false);
                }
                else
                {
                    items[i].gameObject.SetActive(true);

                    items[i].sprite = player.items[i].sprite;
                    items[i].color = player.items[i].color;
                }
                itemKeys[i].text = controls.itemKeys[i].toShortString();
            }
        }
    }
}
