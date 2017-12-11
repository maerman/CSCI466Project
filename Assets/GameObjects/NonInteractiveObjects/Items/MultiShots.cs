// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// MultiShots is an Item that allow its holder to shoot multiple LazerShots
/// at once in an arch
/// </summary>
public class MultiShots : Item
{
    protected const float USE_POINTS = -10f;

    public float numberOfShots = 8f;
    public float damageEach = 10f;
    public float shotSpeed = 20f;
    public float shotLifeSecs = 3f;
    public Vector2 offset = new Vector2(0, 2f);
    public Vector2 shotScale = new Vector2(0.25f, 0.025f);

    public float shotTimeSecs = 2f;
    private int shotTimer = 0;
    private int shotCooldown = 0;

    public float spreadStart = 30f;
    public float spreadMax = 180f;
    private float spreadSpeed;
    private float spread;

    protected override void dropItem()
    {
        
    }

    /// <summary>
    ///shoot LazerShots infront of the holder in an arch 
    /// </summary>
    private void shoot()
    {
        //make sure the arch is within the correct bounds
        if (spread < spreadStart)
        {
            spread = spreadStart;
        }
        else if (spread > spreadMax)
        {
            spread = spreadMax;
        }

        //create each LazerShot at a different angle in the arch
        for (int i = 0; i < numberOfShots; i++)
        {
            //find the angle for the current LazerShot
            float currentAngle = holder.angle - spread / 2.0f + i * spread / (numberOfShots - 1);

            //create the LazerShot infront of the holder its angle
            LazerShot current = (LazerShot)level.createObject("LazerShotPF", holder.position + offset.rotate(currentAngle), 
                currentAngle, new Vector2(shotSpeed, 0).toAngle(currentAngle) + holder.velocity);

            //set the LazerShot's initial settings
            current.damage = damageEach;
            current.timeToLiveSecs = shotLifeSecs;
            current.color = color;
            current.team = holder.team;
        }

        //reset shoot settings
        shotCooldown = (int)(shotTimeSecs * level.updatesPerSec);
        shotTimer = 0;
        spread = 0;

        level.score += USE_POINTS;
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //update the time until the next shot
        if (shotCooldown > 0)
        {
            shotCooldown--;
        }
        //if it is time and the holder releases this Item's key, shoot the shots
        else if (endUse)
        {
            shoot(); 
        }

        //update the time since the holder pressed this Item's key,
        //if the time has passed a certian amount, shoot the shots
        if (shotTimer > 0)
        {
            shotTimer--;

            if (shotTimer <= 0)
            {
                shoot();
            }
        }

        //if the holder is holding down this Item's key, then keep track of when
        //the next shot should be fired and increase the spread of the shots
        if (use)
        {
            if (shotTimer <= 0)
            {
                shotTimer = (int)(shotTimeSecs * level.updatesPerSec);
                spread = spreadStart;
            }
            else
            {
                spread += spreadSpeed;

                if (spread > spreadMax)
                {
                    spread = spreadMax;
                }
            }
        }
    }

    protected override void pickupItem()
    {
        //calculate the spreadSpeed
        spreadSpeed = (spreadMax - spreadStart) / (shotTimeSecs * level.updatesPerSec);
    }
}
