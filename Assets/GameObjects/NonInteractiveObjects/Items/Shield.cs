using UnityEngine;
using System.Collections;

public class Shield : Item
{
    public float timeToTurnSecs = 0.25f;
    private int turnTimer;
    public float turnSpeed = 1;

    public float shieldWidth = 5;

    public Vector2 offset = new Vector2(0, 3);

    private Barrier shield;

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
        if (doubleUse && shield != null)
        {
            shield.destroyThis();
            shield = null;
        }
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
            turnTimer = (int)(timeToTurnSecs * level.updatesPerSec);
        }
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
