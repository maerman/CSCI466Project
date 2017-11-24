using UnityEngine;
using System.Collections;

public class SputteringDebris : DestructableObject
{
    public float damage = 15f;
    public float acceleration = 0.5f;
    public float turnSpeed = 1f;
    public float maxAccelerateDurationSecs = 2f;

    private int wait = 0;
    private bool accelerate = false;

    protected override void destroyDestructableObject()
    {
        
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (team != other.team)
        {
            other.damageThis(damage);
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
        if (team != other.team)
        {
            other.damageThis(damage);
        }
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        if (wait <= 0)
        {
            accelerate = !accelerate;
            wait = level.random.Next((int)(maxAccelerateDurationSecs * level.updatesPerSec));
        }

        if (accelerate)
        {
            angularVelocity += turnSpeed;
            moveForward(acceleration);
        }

        wait--;
    }
}
