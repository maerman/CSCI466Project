// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// Lazer is an NonInteractiveObject that attaches to a SpaceObject and extends to a certain
/// distance, damaging any DestructableObject it collides with (besides what it is attached to)
/// </summary>
public class Lazer : NonInteractiveObject
{
    public float damage = 2;
    public float extendSpeed = 0.1f;
    public float maxLength = 8f;

    public Vector2 attachPoint = new Vector2(0, 0);
    public float attachAngle = 0;
    public SpaceObject attachedTo;

    private float originalLength;

    protected override void destroyNonInteractiveObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other)
    {
        if (other != attachedTo)
        {
            other.damageThis(damage);

            //Retract the Lazer to only go to what it collided with, not past it
            scale = new Vector2(scale.x, distanceFrom(other) / originalLength);
        }
    }

    protected override void indestructableObjectCollision(IndestructableObject other)
    {
        
    }

    protected override void playerCollision(Player other)
    {
        if (other != attachedTo)
        {
            other.damageThis(damage);

            //Retract the Lazer to only go to what it collided with, not past it
            scale = new Vector2(scale.x, distanceFrom(other) / originalLength);
        }
    }

    protected override void startNonInteractiveObject()
    {
        //find and save the length of this
        angle = 0;
        originalLength = gameObject.GetComponent<Collider2D>().bounds.size.y / scale.y;
        angle = attachedTo.angle + attachAngle;
    }

    protected override void updateNonInteractiveObject()
    {
        if (attachedTo != null && attachedTo.active)
        {
            //extend this by its extendSpeed until it reaches it maxLenght
            float currentLenght = originalLength * scale.y;
            currentLenght += extendSpeed;
            if (currentLenght < maxLength)
                scale = new Vector2(scale.x, currentLenght / originalLength);

            //keep it attached to the correct position relative to what it is attached to
            angle = attachedTo.angle + attachAngle;
            Vector2 toRotate = attachPoint;
            position = attachedTo.position + toRotate.rotate(angle);

            //make sure physics is not moving this, would cause it to be attached incorrectly
            velocity = Vector2.zero;
            angularVelocity = 0;
        }
        //if this is not attached to anything, destroy it
        else
        {
            destroyThis();
        }
    }
}
