using UnityEngine;
using System.Collections;

public class LazerShot : IndestructableObject
{
    public float damage = 10;
    private int timeAlive = 0;
    public float timeToLiveSecs = 2;

    protected override void startIndestructableObject()
    {
        
    }

    protected override void updateIndestructableObject()
    {
        timeAlive++;
        if (timeAlive > timeToLiveSecs * level.updatesPerSec)
        {
            destroyThis();
        }
    }


    protected override void destroyIndestructableObject()
    {

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
