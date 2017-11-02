using UnityEngine;
using System.Collections;

public class HomingMissiles : Item
{
    public float missileDamge = 15;
    public float missileAcceleration = 1;
    public float missileMaxSpeed = 16f;
    public float missileTurnSpeed = 4f;
    public float missleLifeTimeSecs = 5f;

    public float shootTimeSecs = 1f;
    private int shotTimer = 0;

    public Vector2 offset = new Vector2(0, 2);

    protected override void dropItem()
    {
        
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (shotTimer > 0)
        {
            shotTimer--;
        }

        if (use && shotTimer <= 0)
        {
            HomingMissile missile = (HomingMissile)level.createObject("HomingMissilePF",
                holder.position + offset.rotate(holder.angle), holder.angle, holder.velocity);

            missile.damage = missileDamge;
            missile.acceleration = missileAcceleration;
            missile.turnSpeed = missileTurnSpeed;
            missile.timeToLiveSecs = missleLifeTimeSecs;
            missile.team = holder.team;
            missile.color = color;
            missile.maxSpeed = missileMaxSpeed;

            shotTimer = (int)(shootTimeSecs * level.updatesPerSec);
        }
    }

    protected override void pickupItem()
    {
        
    }
}
