using UnityEngine;
using System.Collections;

public class LazerBeam : Item
{
    public float beamDamge = 5f;
    public float beamMaxLength = 20f;
    public float beamExtendSpeed = 1f;
    public Vector2 offset = new Vector2(0, 0);

    private Lazer beam;

    protected override void dropItem()
    {
        if (beam != null)
        {
            beam.destroyThis();
            beam = null;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        if (startUse)
        {
            beam = (Lazer)level.createObject("LazerPF");
            beam.position = holder.position;
            beam.angle = holder.angle;
            beam.attachedTo = holder;
            beam.damage = beamDamge;
            beam.maxLength = beamMaxLength;
            beam.extendSpeed = beamExtendSpeed;
            beam.color = holder.color;
        }

        if (!use && beam != null)
        {
            beam.destroyThis();
            beam = null;
        }
    }

    protected override void pickupItem()
    {
        
    }
}
