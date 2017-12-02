using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HomingMines are DestructableObjects that move towards enemy DestructableObjects that
/// are close enough to it. When it collides with a DestructableObject, it deals damage to it. 
/// </summary>
public class HomingMine : DestructableObject
{
    public float acceleration = 0.2f;
    public float targetAngularVelocity = 3;
    public float angularAcceleration = 0.1f;
    public float targetFindProximity = 6f;
    public float targetLossProximity = 12f;
    public SpaceObject target;
    public float damage = 10f;

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
        //if the target is too far away, make it no longer the target
        if (target != null && distanceFrom(target) > targetLossProximity)
            target = null;

        if (target == null || !target.active)
        {
            //set the target to the closest enemy DestructableObject
            target = closestObject(level.getTypes(true, true, false, false), false);

            //if the target is too far away, make it no longer the target
            if (target != null && distanceFrom(target) > targetFindProximity)
                target = null;
        }

        //if there is no target, slow down to a stop
        if (target == null || !target.active) 
        {
            if (speed > acceleration)
                speed = speed - acceleration;
            else
                speed = 0;

            if (angularVelocity > 0 && angularVelocity > angularAcceleration)
                angularVelocity = angularVelocity - angularAcceleration;
            else if (angularVelocity < 0 && angularVelocity < -angularAcceleration)
                angularVelocity = angularVelocity + angularAcceleration;    
            else
                angularVelocity = 0;
        }
        //if there is a target, accelerate towards it
        else 
        {
            if (angularVelocity > targetAngularVelocity)
            {
                if (angularVelocity - angularAcceleration < targetAngularVelocity)
                    angularVelocity = targetAngularVelocity;
                else
                    angularVelocity -= angularAcceleration;
            }
            else if (angularVelocity < targetAngularVelocity)
            {
                if (angularVelocity + angularAcceleration > targetAngularVelocity)
                    angularVelocity = targetAngularVelocity;
                else
                    angularVelocity += angularAcceleration;
            }

            moveTowards(target, acceleration * difficultyModifier);
        }
    }
}
