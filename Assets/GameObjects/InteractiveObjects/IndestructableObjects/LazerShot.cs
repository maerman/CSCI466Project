using UnityEngine;
using System.Collections;

public class LazerShot : IndestructableObject
{
    public float damage = 10;
    private int timeAlive = 0;
    public int updatesToLive = 50;

    protected override void startIndestructableObject()
    {

    }

    protected override void updateIndestructableObject()
    {
        timeAlive++;
        if (timeAlive > updatesToLive)
        {
            destroyThis();
        }
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.damageThis(damage);
        destroyThis();
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        destroyThis();
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.damageThis(damage);
        destroyThis();
    }
}
