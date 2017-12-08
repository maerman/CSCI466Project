// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// RubberyDebris is a DestructableObject that perfectly bounces off any InteractiveObject (not losing any speed)
/// and damages any DestructableObject not on the same team that it collides with
/// </summary>
public class RubberyDebris : DestructableObject
{
    public float damage = 5;
    private Vector2 previousVelocity;

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.damageThis(damage * difficultyModifier);
        }

        bounce(collision);
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        bounce(collision);
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.damageThis(damage * difficultyModifier);
        }

        bounce(collision);
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        previousVelocity = velocity;
    }

    private void bounce(Collision2D collision)
    {
        // Normal
        Vector2 N = collision.contacts[0].normal;

        //Direction
        Vector2 V = previousVelocity.normalized;

        // Reflection
        Vector2 R = Vector2.Reflect(V, N).normalized;

        // Assign normalized reflection with the constant speed
        velocity = new Vector2(R.x, R.y) * previousVelocity.magnitude;
    }
}
