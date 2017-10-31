using UnityEngine;
using System.Collections;

public class LazerSword : Item
{
    public float timeToTurnSecs = 0.25f;
    private int turnTimer;
    public float turnSpeed = 1;

    public int swordLength = 5;
    public int swordDamge = 5;

    public Vector2 offset = new Vector2(0, 1);

    private LazerBeam sword;

    protected override void pickupItem()
    {
        
    }

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
        if (doubleUse && sword != null)
        {
            sword.destroyThis();
            sword = null;
        }
        else if (startUse)
        {
            if (sword == null)
            {
                sword = (LazerBeam)level.createObject("LazerBeamPF");
                sword.attachedTo = holder;
                sword.damage = swordDamge;
                sword.length = swordLength;
                sword.color = color;
                sword.attachPoint = offset;
            }
            turnTimer = (int)(timeToTurnSecs * level.updatesPerSec);
        }
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
}
