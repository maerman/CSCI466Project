using UnityEngine;
using System.Collections;

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
            scale = new Vector2(scale.x, distanceFrom(other) / originalLength);
        }
    }

    protected override void startNonInteractiveObject()
    {
        angle = 0;
        originalLength = gameObject.GetComponent<Collider2D>().bounds.size.y / scale.y;
        angle = attachedTo.angle + attachAngle;
    }

    protected override void updateNonInteractiveObject()
    {
        if (attachedTo != null && attachedTo.active)
        {
            float currentLenght = originalLength * scale.y;
            currentLenght += extendSpeed;

            if (currentLenght < maxLength)
                scale = new Vector2(scale.x, currentLenght / originalLength);

            angle = attachedTo.angle + attachAngle;
            Vector2 toRotate = attachPoint;

            position = attachedTo.position + toRotate.rotate(angle);

            velocity = Vector2.zero;
            angularVelocity = 0;
        }
        else
        {
            destroyThis();
        }
    }
}
