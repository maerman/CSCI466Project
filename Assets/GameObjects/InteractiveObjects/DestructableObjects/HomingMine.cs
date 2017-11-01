using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        ScoreScript.ScoreValue += 10;
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.damageThis(damage);
        destroyThis();
        ScoreScript.ScoreValue += -50;
    }

    protected override void startDestructableObject()
    {
        angularVelocity = targetAngularVelocity;
    }

    protected override void updateDestructableObject()
    {
        if (target != null && distanceFrom(target) > targetLossProximity)
        {
            target = null;
        }

        if (target == null || !target.destroyed)
        {
            IEnumerable<DestructableObject>[] targetList = new IEnumerable<DestructableObject>[2];
            targetList[0] = level.destructables;
            targetList[1] = level.players;

            target = closestObject<DestructableObject>(targetList, false);

            if (target != null && distanceFrom(target) > targetFindProximity)
            {
                target = null;
            }
        }


        if (target == null || !target.destroyed)
        {
            if (speed > acceleration)
            {
                speed = speed - acceleration;
            }
            else
            {
                speed = 0;
            }

            if (angularVelocity > 0 && angularVelocity > angularAcceleration)
            {
                angularVelocity = angularVelocity - angularAcceleration;
            }
            else if (angularVelocity < 0 && angularVelocity < -angularAcceleration)
            {
                angularVelocity = angularVelocity + angularAcceleration;    
            }
            else
            {
                angularVelocity = 0;
            }
        }
        else
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

            moveTowards(target, acceleration);
        }
    }
}
