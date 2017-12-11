// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// HomingMissles is an Item that allows its holder to create (HomingMissle)s
/// every so often.
/// </summary>
public class HomingMissiles : Item
{
    protected const float USE_POINTS = -10f;

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
        //update the time before the next missle can be shot
        if (shotTimer > 0)
        {
            shotTimer--;
        }
        //if its time to shoot the next missle and the holder is pressing this Item's key, 
        //then create a new missle infront of the holder
        else if (use)
        {
            //create the missle infront of the holder
            HomingMissile missile = (HomingMissile)level.createObject("HomingMissilePF",
                holder.position + offset.rotate(holder.angle), holder.angle, holder.velocity);

            //set the missle's initial settings
            missile.damage = missileDamge;
            missile.acceleration = missileAcceleration;
            missile.turnSpeed = missileTurnSpeed;
            missile.timeToLiveSecs = missleLifeTimeSecs;
            missile.team = holder.team;
            missile.color = color;
            missile.maxSpeed = missileMaxSpeed;

            //reset the timer to shoot the next missle
            shotTimer = (int)(shootTimeSecs * level.updatesPerSec);

            level.score += USE_POINTS;
        }
    }

    protected override void pickupItem()
    {
        
    }
}
