using UnityEngine;
using System.Collections;

/// <summary>
/// Armor is an Item that can be activated or deactivated. When activated, 
/// it increase's the holder's armor and makes them slowly lose health. 
/// </summary>
public class Armor : Item
{
    public float armorGain = 3.0f;
    public float healthPerSecondLoss = 0.5f;

    private bool activated = false;


    protected override void dropItem()
    {
        //if activated when dropped, deactivate before dropping.
        if (activated)
        {
            holder.armor -= armorGain;
            activated = false;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the user presses this item's corropsoing key, toggle this item's activation.
        if (startUse)
        {
            activated = !activated;

            //if it is activated, increase holder's armor.
            if (activated)
            {
                 holder.armor += armorGain;
            }
            //if it is deactivated, decrease the holder's armor to bring the value back to normal.
            else
            {
                holder.armor -= armorGain;
            }
        }

        //every update, if activated, decrease the holder's health until the holder runs out of health.
        if (activated)
        {
            if (holder.health > healthPerSecondLoss * level.secsPerUpdate)
            {
                holder.health -= healthPerSecondLoss * level.secsPerUpdate;
            }
            //if the holder runs out of health, display that the Item is being deacitvated, then deactivate it.
            else
            {
                IngameInterface.displayMessage("Armor deactivated, not enough health remaining!", 5f);
                activated = false;
                holder.armor -= armorGain;
            }
        }
    }

    protected override void pickupItem()
    {
        
    }
}
