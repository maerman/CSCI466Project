using UnityEngine;
using System.Collections;

public class HomingMine : DestructableObject
{
    public float acceleration = 0.2f;
    public float targetAngularVelocity = 3;
    public float angularAcceleration = 0.2f;
    public SpaceObject target;
    public float damage = 10f;

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (enemy && !other.enemy || (!enemy && other.enemy))
        {
            other.damageThis(damage);
            destroyThis();
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
        if (enemy)
        {
            other.damageThis(damage);
            destroyThis();
        }
    }

    protected override void startDestructableObject()
    {
        angularVelocity = targetAngularVelocity;
    }

    protected override void updateDestructableObject()
    {
        if (angularVelocity > targetAngularVelocity)
        {
            if (angularVelocity - angularAcceleration < targetAngularVelocity)
            {
                angularVelocity = targetAngularVelocity;
            }
            else
            {
                angularVelocity -= angularAcceleration;
            }
        }
        else if (angularVelocity < targetAngularVelocity)
        {
            if (angularVelocity + angularAcceleration > targetAngularVelocity)
            {
                angularVelocity = targetAngularVelocity;
            }
            else
            {
                angularVelocity += angularAcceleration;
            }
        }

        if (target == null || !target.inPlay)
        {
            if (enemy)
            {
                foreach (Player item in level.players)
                {
                    if (target == null || distanceFrom(target) > distanceFrom(item))
                    {
                        target = item;
                    }
                }
            }
            else
            {
                foreach (DestructableObject item in level.destructables)
                {
                    if (target == null || distanceFrom(target) > distanceFrom(item))
                    {
                        target = item;
                    }
                }
            }
        }

        if (target == null)
        {
            moveForward(acceleration);
        }
        else
        {
            moveTowards(target, acceleration);
        }
    }
}
