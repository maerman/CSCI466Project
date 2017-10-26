using UnityEngine;
using System.Collections;

public class LazerBeam : NonInteractiveObject
{
    public float damage = 2;
    public float extendSpeed = 0.1f;
    public float extendDistance = 8f;

    public Vector2 attachPoint = new Vector2(0, 1f);
    public float attachAngle = 0;
    public SpaceObject attachedTo;

    private float originalLength;


    protected override void destructableObjectCollision(DestructableObject other)
    {
        if (other != attachedTo)
        {
            other.damageThis(damage);
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
        }
    }

    protected override void startNonInteractiveObject()
    {
        angle = 0;
        originalLength = gameObject.GetComponent<Collider2D>().bounds.size.y / scale.y;
    }

    protected override void updateNonInteractiveObject()
    {
        if (attachedTo != null && attachedTo.inPlay)
        {
            float currentLenght = originalLength * scale.y;
            if (currentLenght < extendDistance)
            {
                float targetLength = currentLenght + extendSpeed;
                scale = new Vector2(scale.x, scale.y * targetLength / currentLenght);
            }

            angle = attachedTo.angle + attachAngle;
            Vector2 toRotate = attachPoint;
            toRotate.y += currentLenght * 0.5f;

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
