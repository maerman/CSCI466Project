// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HomingMissile is a DestructableObject that accelerates forward until it finds a target infront of it,
/// then it accelerates towards the target. When it collides with any DestructableObject, it deals damage to it.
/// </summary>
public class HomingMissile : DestructableObject
{
    public float damage = 20f;
    public float acceleration = 1f;
    public float turnSpeed = 4;
    public float timeToLiveSecs = 5f;
    private int timeAlive = 0;
    private SpaceObject target;


    protected override void destroyDestructableObject()
    {
        
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

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        //destroys itself after so long if it is set to
        if (timeToLiveSecs > 0)
        {
            timeAlive++;

            if (timeAlive > timeToLiveSecs * level.updatesPerSec)
                destroyThis();
        }

        //trys to find a target if it doesn't have one
        //the target would be the closest enemy DestructableObject infront of it
        if (target == null || !target.active)
        {
            target = closestObjectInDirection(level.getTypes(true, true, false, false), angle, false);
        }

        //turn towards the target if it has one
        if (target != null)
        {
            turnTowards(target, turnSpeed * difficultyModifier);
        }

        moveForward(acceleration);
    }
}
