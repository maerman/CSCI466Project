using UnityEngine;
using System.Collections;

public class HomingMine : DestructableObject
{
    public float acceleration = 0.2f;
    public float targetAngularVelocity = 3;
    public float angularAcceleration = 0.1f;
    public float targetFindProximity = 6f;
    public float targetLossProximity = 12f;
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
        if (distanceFrom(target) > targetLossProximity)
        {
            target = null;
        }

        if (target == null || !target.inPlay)
        {
            if (enemy)
            {
                DestructableObject tempTarget = closestObject<DestructableObject>(level.players);
                DestructableObject tempTarget2 = closestObject<DestructableObject>(level.destructables, false);

                if (tempTarget != null)
                {
                    if (tempTarget2 != null)
                    {
                        if (distanceFrom(tempTarget2) < targetFindProximity)
                        {
                            target = tempTarget2;
                        }
                        if (distanceFrom(tempTarget) < targetFindProximity && distanceFrom(tempTarget) < distanceFrom(tempTarget2))
                        {
                            target = tempTarget;
                        }
                    }
                    else if (distanceFrom(tempTarget) < targetFindProximity)
                    {
                        target = tempTarget;
                    }
                }
                else if (tempTarget2 != null)
                {
                    if (distanceFrom(tempTarget2) < targetFindProximity)
                    {
                        target = tempTarget2;
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


        if (target == null || !target.inPlay)
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
