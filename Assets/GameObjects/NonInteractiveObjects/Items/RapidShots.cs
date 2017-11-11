using UnityEngine;
using System.Collections;

public class RapidShots : Item
{
    public float shotLifeSecs = 3f;
    public float shotSpeed = 20f;
    public float shotDamage = 2f;
    public Vector2 offset = new Vector2(0, 2f);

    public float shotCooldownSecs = 0.2f;
    private int shotCooldown = 0;

    protected override void dropItem()
    {
        
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (shotCooldown > 0)
        {
            shotCooldown--;
        }
        else if (use)
        {
            LazerShot shot = (LazerShot)level.createObject("LazerShotPF", holder.position + offset.rotate(holder.angle), holder.angle);
            shot.velocity = holder.velocity;
            shot.moveForward(shotSpeed);
            shot.damage = shotDamage;
            shot.timeToLiveSecs = shotLifeSecs;
            shot.color = color;
            shot.team = holder.team;

            shotCooldown = (int)(shotCooldownSecs * level.updatesPerSec);
        }
    }

    protected override void pickupItem()
    {
        
    }
}
