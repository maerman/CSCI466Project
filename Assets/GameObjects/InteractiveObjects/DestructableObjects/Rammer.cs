using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rammer : DestructableObject
{
    public float damage = 15;

    public DestructableObject target;
    private float ramTime = 0;
    public float overRunSecs = 2;

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.damageThis(damage);
        }
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.damageThis(damage);
        }
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        if (target == null || !target.destroyed)
        {
            IEnumerable<DestructableObject>[] targetList = new IEnumerable<DestructableObject>[2];
            targetList[0] = level.destructables;
            targetList[1] = level.players;

            target = closestObject<DestructableObject>(targetList, false);
        }
        else if (ramTime > 0)
        {
            ramTime--;
        }
        else
        {
            Vector3 temp = intersectPosTime(target, maxSpeed);
            if (temp.z < 0)
            {
                target = null;
            }
            else
            {
                ramTime = (temp.z + overRunSecs) * level.updatesPerSec;
                turnTowards(temp);
                angularVelocity = 0;
                velocity = Vector2.zero;
                moveTowards(temp, maxSpeed);
            }
        }
    }
}
