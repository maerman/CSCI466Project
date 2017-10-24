using UnityEngine;
using System.Collections;

public class LazerShot : IndestructableObject
{
    public float initialSpeed = 15;
    public float damage = 10;
    private int timeAlive = 0;
    public int timeToLive = 50;

    protected override void startIndestructableObject()
    {
        moveForward(initialSpeed);
        //modifyVelocityRelative(new Vector2(0, initialSpeed));
    }

    protected override void updateIndestructableObject()
    {
        timeAlive++;
        if (timeAlive > timeToLive)
        {
            destroyThis();
        }
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.damageThis(damage);
        destroyThis();
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        destroyThis();
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.damageThis(damage);
        destroyThis();
    }
}
