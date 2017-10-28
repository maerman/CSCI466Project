using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowTurner : DestructableObject
{
    public float turnSpeed = 0.01f;
    public float damage = 10f;
    public float acceleration = 0.1f;
    private DestructableObject target;


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
        if (target == null || !target.inPlay)
        {
            IEnumerable<DestructableObject>[] targetList = new IEnumerable<DestructableObject>[2];
            targetList[0] = level.destructables;
            targetList[1] = level.players;

            target = closestObject<DestructableObject>(targetList, false);
        }
        else
        {
            rotateTowards(target, turnSpeed);
        }

        moveForward(acceleration);
    }
}
