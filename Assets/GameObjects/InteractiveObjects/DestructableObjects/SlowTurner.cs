// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SlowTurner is a DestructableObject that slowly turns towars its target, always moving forward.
/// The target is assigned to the closest enemy DestructableObject. SlowTurners damage any enemy DestructableObject
/// they collide with. 
/// </summary>
public class SlowTurner : DestructableObject
{
    public float turnSpeed = 0.01f;
    public float damage = 10f;
    public float acceleration = 0.1f;
    private SpaceObject target;

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
        //find a target if there is not currently one
        if (target == null || !target.active)
        {
            target = closestObject(level.getTypes(true, true, false, false));
        }
        //if there is a target, turn towards it
        else
        {
            turnTowards(target, turnSpeed * difficultyModifier);
        }

        moveForward(acceleration);
    }
}
