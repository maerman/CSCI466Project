using UnityEngine;
using System.Collections;

public class RandomTurner : DestructableObject
{
    public float damage = 20f;
    public float acceleration = 0.4f;
    public float turnSpeed = 0.3f;
    private int updatesToNextRand = 0;
    public float maxSecsInADirection = 10;
    public float minSecsInADirection = 1;
    private bool turnLeft = false;

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
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
        if (other.team != team)
        {
            other.damageThis(damage);
        }
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        updatesToNextRand--;

        if (updatesToNextRand <= 0)
        {
            turnLeft = !turnLeft;

            updatesToNextRand = level.random.Next((int)(minSecsInADirection * level.updatesPerSec), (int)(maxSecsInADirection * level.updatesPerSec));
        }

        if (turnLeft)
        {
            angle = angle - turnSpeed;
        }
        else
        {
            angle = angle + turnSpeed;
        }

        moveForward(acceleration);
    }
}
