using UnityEngine;
using System.Collections;

public class Accelerant : Item
{
    public float accelerationPerSecGain = 15f;
    public float armorLoss = 2.0f;

    private bool activated = false;

    protected override void dropItem()
    {
        if (activated)
        {
            holder.accelerationPerSec -= accelerationPerSecGain;
            holder.armor += armorLoss;
            activated = false;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (startUse)
        {
            activated = !activated;

            if (activated)
            {
                holder.accelerationPerSec += accelerationPerSecGain;
                holder.armor -= armorLoss;
            }
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
