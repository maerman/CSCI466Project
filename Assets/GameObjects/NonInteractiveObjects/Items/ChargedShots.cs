using UnityEngine;
using System.Collections;

public class ChargedShots : Item
{
    public float shotMinDamage = 10f;
    public float shotMaxDamage = 100f;
    public float shotSpeed = 20f;
    public float shotLifeSecs = 4f;
    public Vector2 offset = new Vector2(0, 1f);
    public Vector2 shotScaleStart = new Vector2(0.05f, 0.5f);

    public float toFullChargeSecs = 5f;
    private float damageChargeSpeed;

    private LazerShot chargingShot;

    protected override void dropItem()
    {
        if (chargingShot != null)
        {
            chargingShot.destroyThis();
            chargingShot = null;
        }
    }

    private void updateChargingShot()
    {
        if (chargingShot != null)
        {
            chargingShot.position = holder.position + offset.rotate(holder.angle);
            chargingShot.angle = holder.angle;
            chargingShot.resetTimeAlive();
            chargingShot.damage += damageChargeSpeed;

            if (chargingShot.damage > shotMaxDamage)
            {
                chargingShot.damage = shotMaxDamage;
            }

            chargingShot.scale = shotScaleStart * Mathf.Sqrt(chargingShot.damage / shotMinDamage);
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (startUse)
        {
            if (chargingShot != null)
                chargingShot.destroyThis();

            chargingShot = (LazerShot)level.createObject("LazerShotPF");
            chargingShot.team = holder.team;
            chargingShot.color = color;
            chargingShot.timeToLiveSecs = shotLifeSecs;
            chargingShot.damage = shotMinDamage;
            updateChargingShot();
        }
        else if (use)
        {
            updateChargingShot();
        }

        if (endUse && chargingShot != null)
        {
            chargingShot.velocity = holder.velocity;
            chargingShot.moveForward(shotSpeed);
            chargingShot = null;
        }

    }

    protected override void pickupItem()
    {
        damageChargeSpeed = (shotMaxDamage - shotMinDamage) / (toFullChargeSecs * level.updatesPerSec);
    }
}
