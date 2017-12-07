using UnityEngine;
using System.Collections;

/// <summary>
/// IndestructableDebris is an IndestructableObject that moves around only baised on physics and 
/// damages any DestructableObject it collides with baised on the collision speed
/// </summary>
public class IndestructableDebris : IndestructableObject
{
    public float damageMultiplier = 1f;
    public float minDamageSpeed = 5f;

    protected override void destroyIndestructableObject()
    {
        
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        //if the collision speed is larger than the minimum, deal damage to the DestructableObject
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
        //if the collision speed is larger than the minimum, deal damage to the Player
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
