using UnityEngine;
using System.Collections;

public class GravityWellController : Item
{
    public Vector2 offset = new Vector2(0, 3);
    public float wellDamage = 10f;
    public float startGravity = 0.5f;
    public float maxGravity = 10f;
    public float gravityIncreaseSpeed = 0.1f;

    private GravityWell well;

    protected override void dropItem()
    {
        if (well != null)
        {
            well.destroyThis();
            well = null;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (startUse)
        {
            if (well == null)
            {
                Vector2 direction = holder.velocity.normalized;
                direction.x *= -1;

                well = (GravityWell)level.createObject("GravityWellPF", holder.position + offset.rotate(direction.getAngle()));
                well.damage = wellDamage;
                well.gravity = startGravity;
            }
            else
            {
                Vector2 direction = holder.velocity.normalized;
                direction.x *= -1;
                well.position = holder.position + offset.rotate(direction.getAngle());
            }
        }
        else if (use && well != null)
        {
            well.gravity *= 1f + gravityIncreaseSpeed;

            if (well.gravity > maxGravity)
            {
                well.gravity = maxGravity;
            }
        }

        if (doubleUse && well != null)
        {
            well.destroyThis();
            well = null;
        }
    }

    protected override void pickupItem()
    {
        
    }
}
