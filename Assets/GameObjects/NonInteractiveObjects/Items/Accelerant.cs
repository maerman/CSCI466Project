using UnityEngine;
using System.Collections;

/// <summary>
/// Acceleraten is an Item that can be activated or deactivated. When activated, 
/// it increases its holder's acceleration and decreses their armor. 
/// </summary>
public class Accelerant : Item
{
    public float accelerationPerSecGain = 15f;
    public float armorLoss = 2.0f;

    private bool activated = false;

    protected override void dropItem()
    {
        //if activated when dropped, deactivate before dropping.
        if (activated)
        {
            holder.accelerationPerSec -= accelerationPerSecGain;
            holder.armor += armorLoss;
            activated = false;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the user presses this item's corropsoing key, toggle this item's activation.
        if (startUse)
        {
            activated = !activated;

            //if it is activated, increase holder's acceleration and decrease their armor.
            if (activated)
            {
                holder.accelerationPerSec += accelerationPerSecGain;
                holder.armor -= armorLoss;
            }
            //if it is deactivated, decrease the holder's acceleration and increase their armor
            //to bring the values back to normal.
            else
            {
                holder.accelerationPerSec -= accelerationPerSecGain;
                holder.armor += armorLoss;
            }
        }
    }

    protected override void pickupItem()
    {
        
    }
}
