using UnityEngine;
using System.Collections;

public class Heal : Item
{
    public float healthPerSecGain = 1.0f;
    public float accelerationPerSecLoss = 10.0f;

    private bool activated = false;

    protected override void dropItem()
    {
        if (activated)
        {
            holder.accelerationPerSec += accelerationPerSecLoss;
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
                if (holder.accelerationPerSec >= accelerationPerSecLoss)
                {
                    holder.accelerationPerSec -= accelerationPerSecLoss;
                }
                else
                {
                    IngameInterface.displayMessage("Can't activate Heal, not enough acceleration remaining!", 5f);
                    activated = false;
                }
            }
            else
            {
                holder.accelerationPerSec += accelerationPerSecLoss;
            }
        }

        if (activated)
        {
            if (holder.health < holder.maxHealth)
            {
                holder.health += healthPerSecGain * level.secsPerUpdate;
            }
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
