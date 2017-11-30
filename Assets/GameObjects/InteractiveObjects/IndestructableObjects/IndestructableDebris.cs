using UnityEngine;
using System.Collections;

public class IndestructableDebris : IndestructableObject
{
    public float damageMultiplier = 1f;
    public float minDamageSpeed = 5f;

    protected override void destroyIndestructableObject()
    {
        
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        float damageSpeed = collision.relativeVelocity.magnitude - minDamageSpeed;
        if (damageSpeed > 0)
        {
            other.damageThis(damageSpeed * damageMultiplier * difficultyModifier);
        }
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        float damageSpeed = collision.relativeVelocity.magnitude - minDamageSpeed;
        if (damageSpeed > 0)
        {
            other.damageThis(damageSpeed * damageMultiplier * difficultyModifier);
        }
    }

    protected override void startIndestructableObject()
    {
        
    }

    protected override void updateIndestructableObject()
    {
        
    }
}
