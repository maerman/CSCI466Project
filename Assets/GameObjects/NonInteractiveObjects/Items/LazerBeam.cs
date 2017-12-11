// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;

/// <summary>
/// LazerBeam is an Item that allows the holder to create a Lazer infront of themselves
/// </summary>
public class LazerBeam : Item
{
    protected const float USE_POINTS = -0.01f;

    public float beamDamge = 5f;
    public float beamMaxLength = 20f;
    public float beamExtendSpeed = 1f;
    public Vector2 offset = new Vector2(0, 0);

    private Lazer lazer;

    //If this Item is dropped, destroy the Lazer if it exists
    protected override void dropItem()
    {
        if (lazer != null)
        {
            lazer.destroyThis();
            lazer = null;
        }
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //if the holder presses this Item's key, create a Lazer
        if (startUse)
        {
            lazer = (Lazer)level.createObject("LazerPF");
            lazer.position = holder.position;
            lazer.angle = holder.angle;
            lazer.attachedTo = holder;
            lazer.damage = beamDamge;
            lazer.maxLength = beamMaxLength;
            lazer.extendSpeed = beamExtendSpeed;
            lazer.color = holder.color;
        }

        //if the user presses releases this Item's key, destroy the Lazer
        if (!use && lazer != null)
        {
            lazer.destroyThis();
            lazer = null;

            level.score += USE_POINTS;
        }
    }

    protected override void pickupItem()
    {
        
    }
}
