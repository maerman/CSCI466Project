using UnityEngine;
using System.Collections;

public class Rammer : DestructableObject
{
    public float damage = 15;

    public DestructableObject target;
    private float ramTime = 0;
    public float overRunSecs = 2;

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
            getNewTarget();
        }
        else if (ramTime > 0)
        {
            Debug.Log(ramTime);
            ramTime--;
        }
        else
        {
            Vector3 temp = intersectPosTime(target, maxSpeed);
            if (temp.z < 0)
            {
                getNewTarget();
            }
            else
            {
                ramTime = temp.z + level.updatesPerSec * overRunSecs;
                turnTowards(temp);
                angularVelocity = 0;
                velocity = Vector2.zero;
                moveTowards(temp, maxSpeed);
            }
        }
    }

    protected void getNewTarget()
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
}
