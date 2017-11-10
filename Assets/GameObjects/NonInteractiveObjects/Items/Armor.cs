using UnityEngine;
using System.Collections;

public class Armor : Item
{
    public float armorGain = 3.0f;
    public float healthPerSecondLoss = 0.5f;

    private bool activated = false;

    protected override void dropItem()
    {
        if (activated)
        {
            holder.armor -= armorGain;
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
                 holder.armor += armorGain;
            }
            else
            {
                holder.armor -= armorGain;
            }
        }

        if (activated)
        {
            if (holder.health > healthPerSecondLoss * level.secsPerUpdate)
            {
                holder.health -= healthPerSecondLoss * level.secsPerUpdate;
            }
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
