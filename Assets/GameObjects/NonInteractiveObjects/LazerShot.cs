using UnityEngine;
using System.Collections;

public class LazerShot : NonInteractiveObject
{
    public float damage = 10;
    private int timeAlive = 0;
    public float timeToLiveSecs = 2;

    protected override void startNonInteractiveObject()
    {
        
    }

    public void resetTimeAlive()
    {
        timeAlive = 0;
    }

    protected override void updateNonInteractiveObject()
    {
        timeAlive++;
        if (timeAlive > timeToLiveSecs * level.updatesPerSec)
        {
            destroyThis();
        }
    }

    protected override void destroyNonInteractiveObject()
    {
        
    }

    protected override void playerCollision(Player other)
    {
        other.damageThis(damage);
        destroyThis();
    }

    protected override void destructableObjectCollision(DestructableObject other)
    {
        other.damageThis(damage);
        destroyThis();
    }

    protected override void indestructableObjectCollision(IndestructableObject other)
    {
        destroyThis();
    }
}
