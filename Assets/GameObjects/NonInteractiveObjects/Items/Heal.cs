using UnityEngine;
using System.Collections;

/// <summary>
/// Heal is an Item that can be activated or deactivated. When activated, 
/// it slowly increase's the holder's health and decreases the holder's acceleration. 
/// </summary>
public class Heal : Item
{
    public float healthPerSecGain = 1.0f;
    public float accelerationPerSecLoss = 10.0f;

    private bool activated = false;

    protected override void dropItem()
    {
        //if activated when dropped, deactivate before dropping.
        if (activated)
        {
            holder.accelerationPerSec += accelerationPerSecLoss;
            activated = false;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the user presses this item's corropsoing key, toggle this item's activation.
        if (startUse)
        {
            activated = !activated;

            //if it is activated, decrease holder's acceleration if the holder has enough acceleration.
            if (activated)
            {
                if (holder.accelerationPerSec >= accelerationPerSecLoss)
                {
                    holder.accelerationPerSec -= accelerationPerSecLoss;
                }
                //if the holder does not have enough acceleration, don't activate it and display that it won't be activated.
                else
                {
                    IngameInterface.displayMessage("Can't activate Heal, not enough acceleration remaining!", 5f);
                    activated = false;
                }
            }
            //if it is deactivated, increase the holder's acceleration to bring the value back to normal.
            else
            {
                holder.accelerationPerSec += accelerationPerSecLoss;
            }
        }

        //every update, if activated, increase the holder's health until the holder has full health.
        if (activated)
        {
            if (holder.health < holder.maxHealth)
            {
                holder.health += healthPerSecGain * level.secsPerUpdate;
            }
            //if the holder's health is full, display that the Item is being deacitvated, then deactivate it.
            else
            {
                IngameInterface.displayMessage("Heal deactivated, fully healed.", 5f);
                holder.accelerationPerSec += accelerationPerSecLoss;
                activated = false;
            }
        }
    }

    protected override void pickupItem()
    {
        
    }
}
