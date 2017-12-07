using UnityEngine;
using System.Collections;

/// <summary>
/// LazerSword is an Item that allows the holder to create and control 
/// a short Lazer attached to themselves
/// </summary>
public class LazerSword : Item
{
    public float timeToTurnSecs = 0.25f;
    private int turnTimer;
    public float turnSpeed = 1;

    public float swordLength = 5;
    public float swordDamge = 5;

    public Vector2 offset = new Vector2(0, 1);

    private Lazer sword;

    //if this Item is dropped, destroy the Lazer if it exists
    protected override void dropItem()
    {
        if (sword != null)
        {
            sword.destroyThis();
            sword = null;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the holder double presses this Item's key, then destroy 
        //the Lazer sword if it exists
        if (doubleUse && sword != null)
        {
            sword.destroyThis();
            sword = null;
        }
        //if the holder presses this Item's key, then create a new Lazer attached
        //to the user if one does not currently exist
        else if (startUse)
        {
            if (sword == null)
            {
                sword = (Lazer)level.createObject("LazerPF");
                sword.attachedTo = holder;
                sword.damage = swordDamge;
                sword.maxLength = swordLength;
                sword.color = color;
                sword.attachPoint = offset;
            }

            //reset the time before holding down this Item's key will turn the Lazer
            turnTimer = (int)(timeToTurnSecs * level.updatesPerSec);
        }
        //if the holder holds down this Item's key, rotate the Lazer around the 
        //holder if a certain amount of time has past
        else if (use)
        {
            if (turnTimer > 0)
            {
                turnTimer--;
            }
            else if (sword != null)
            {
                sword.attachAngle += turnSpeed;
            }
        }
    }

    protected override void pickupItem()
    {

    }
}
