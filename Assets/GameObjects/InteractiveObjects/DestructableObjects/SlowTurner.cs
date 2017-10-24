using UnityEngine;
using System.Collections;

public class SlowTurner : DestructableObject
{
    public float turnSpeed = 0.01f;
    public float damage = 10f;
    public float acceleration = 0.1f;
    private DestructableObject target;


    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.damageThis(damage);
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.damageThis(damage);
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        if (target == null || !target.inPlay)
        {
            if (enemy)
            {
                target = closestObject<DestructableObject>(level.players, true);

                DestructableObject temp = closestObject<DestructableObject>(level.destructables, false);

                if (target == null || temp != null && distanceFrom(temp) < distanceFrom(target))
                {
                    target = temp;
                }
            }
            else
            {
                target = closestObject<DestructableObject>(level.destructables, true);
            }
        }
        else
        {
            rotateTowards(target, turnSpeed);
        }

        moveForward(acceleration);
    }
}
