using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LazerShooter : DestructableObject
{
    private DestructableObject target;

    public float damage = 15;
    public float shootTimeSecs = 0.5f;
    private float shootNextUpdates;
    public float shotSpeed = 20;
    public float maxShotLiveSecs = 0.5f;
    public float acceleration = 0.3f;
    public float targetDistanceFrom = 10f;

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        
    }

    protected override void startDestructableObject()
    {
        shootNextUpdates = 4 * level.updatesPerSec;
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
            shootNextUpdates--;

            Vector3 intersect = intersectPosTime(target, shotSpeed, new Vector2(0, 3).rotate(angle) + position);

            if (intersect.z >= 0)
            {
                turnTowards(intersect);
            }

            if (shootNextUpdates <= 0)
            {

                if (intersect.z < 0 || intersect.z > maxShotLiveSecs)
                {
                    target = null;
                }
                else
                {
                    shootNextUpdates = shootTimeSecs * level.updatesPerSec;

                    LazerShot shot = (LazerShot)level.createObject("LazerShotPF", 
                        new Vector2(0, 3 + shotSpeed * level.secsPerUpdate).rotate(angle) + position, angle);

                    shot.maxSpeed = shotSpeed;
                    shot.speed = shotSpeed;
                    shot.color = Color.red;
                    shot.updatesToLive = (int)(maxShotLiveSecs * level.updatesPerSec + 1);
                    shot.damage = damage;
                }
            }
            else
            {
                if (distanceFrom(target) > targetDistanceFrom)
                {
                    moveTowards(target, acceleration);
                }
                else
                {
                    moveTowards(target, -acceleration);
                }
            }
        }
    }
}
