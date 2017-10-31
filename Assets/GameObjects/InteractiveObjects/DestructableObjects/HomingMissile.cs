using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingMissile : DestructableObject
{
    public float damage = 20f;
    public float acceleration = 1f;
    public float turnSpeed = 4;
    public float timeToLiveSecs = 5f;
    private int timeAlive = 0;
    private DestructableObject target;


    protected override void destroyDestructableObject()
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

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        if (timeToLiveSecs > 0)
        {
            timeAlive++;

            if (timeAlive > timeToLiveSecs * level.updatesPerSec)
            {
                destroyThis();
            }
        }

        if (target == null || !target.inPlay)
        {
            IEnumerable<DestructableObject>[] list = new IEnumerable<DestructableObject>[2];
            list[0] = level.players;
            list[1] = level.destructables;

            target = closestObjectInDirection(list, angle, false);
        }

        if (target != null)
        {
            turnTowards(target);
            //rotateTowards(target); //better to use this but it is currently broken
            Debug.Log(target);
        }
        moveForward(acceleration);
    }
}
