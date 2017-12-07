using UnityEngine;
using System.Collections;

/// <summary>
/// LazerShot is a NonInteractiveObject that moves forward with its initial speed, unless it is 
/// modifed by something else, until it collides with an InteractiveObject or its life timer runs
/// out, which will cause it to be destroyed. When it collides with an enemy DestructableObject
/// it will damage it.
/// </summary>
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

    /// <summary>
    /// If the timeToLiveSecs is set to a positive number and this has been alive for
    /// longer than that many seconds, destroy this
    /// </summary>
    protected override void updateNonInteractiveObject()
    {
        if (timeToLiveSecs > 0)
        {
            timeAlive++;
            if (timeAlive > timeToLiveSecs * level.updatesPerSec)
            {
                destroyThis();
            }
        }
    }

    protected override void destroyNonInteractiveObject()
    {
        
    }

    protected override void playerCollision(Player other)
    {
        if (team != other.team)
        {
            other.damageThis(damage);
        }
        destroyThis();
    }

    protected override void destructableObjectCollision(DestructableObject other)
    {
        if (team != other.team)
        {
            other.damageThis(damage);
        }
        destroyThis();
    }

    protected override void indestructableObjectCollision(IndestructableObject other)
    {
        destroyThis();
    }
}
