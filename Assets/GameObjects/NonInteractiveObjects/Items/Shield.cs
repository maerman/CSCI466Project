using UnityEngine;
using System.Collections;

/// <summary>
/// Shield is an Item that allows the holder to create and control 
/// a Barrier attached to themselves
/// </summary>
public class Shield : Item
{
    public float timeToTurnSecs = 0.25f;
    private int turnTimer;
    public float turnSpeed = 1;

    public float shieldWidth = 5;

    public Vector2 offset = new Vector2(0, 3);

    private Barrier shield;

    //if this Item is dropped, destroy the Barrier if it exists
    protected override void dropItem()
    {
        if (shield != null)
        {
            shield.destroyThis();
            shield = null;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the holder double presses this Item's key, then destroy 
        //the Barrier sword if it exists
        if (doubleUse && shield != null)
        {
            shield.destroyThis();
            shield = null;
        }
        //if the holder presses this Item's key, then create a new Barrier attached
        //to the holder if one does not currently exist
        else if (startUse)
        {
            if (shield == null)
            {
                shield = (Barrier)level.createObject("BarrierPF");
                shield.attachedTo = holder;
                shield.width = shieldWidth;
                shield.color = color;
                shield.attachPoint = offset;
            }

            //reset the time before holding down this Item's key will turn the Barrier
            turnTimer = (int)(timeToTurnSecs * level.updatesPerSec);
        }
        //if the holder holds down this Item's key, rotate the Barrier around the 
        //holder if a certain amount of time has past
        else if (use)
        {
            if (turnTimer > 0)
            {
                turnTimer--;
            }
            else if (shield != null)
            {
                shield.attachAngle += turnSpeed;
            }
        }
    }

    protected override void pickupItem()
    {
        
    }
}
