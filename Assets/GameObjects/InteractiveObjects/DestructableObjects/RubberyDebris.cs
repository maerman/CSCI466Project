using UnityEngine;
using System.Collections;

public class RubberyDebris : DestructableObject
{
    public float damage = 5;
    private Vector2 previousVelocity;

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.damageThis(damage);
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
            other.damageThis(damage);
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
