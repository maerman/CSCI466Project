using UnityEngine;
using System.Collections;

public class LazerSword : Item
{
    public float timeToTurnSecs = 0.25f;
    private int turnTimer;
    public float turnSpeed = 1;

    public float swordLength = 5;
    public float swordDamge = 5;

    public Vector2 offset = new Vector2(0, 1);

    private Lazer sword;

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
                sword = (Lazer)level.createObject("LazerPF");
                sword.attachedTo = holder;
                sword.damage = swordDamge;
                sword.maxLength = swordLength;
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
