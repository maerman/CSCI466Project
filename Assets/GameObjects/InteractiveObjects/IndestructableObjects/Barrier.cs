using UnityEngine;
using System.Collections;

public class Barrier : IndestructableObject
{
    public float width = 8f;

    public Vector2 attachPoint = new Vector2(0, 2);
    public float attachAngle = 0;
    public SpaceObject attachedTo;

    private float originalWidth;
    private float previousScaleX = 0;
    public float previousWidth = 0;

    protected override void destroyIndestructableObject()
    {
        
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {

    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {

    }


    protected override void startIndestructableObject()
    {
        angle = 0;
        originalWidth = gameObject.GetComponent<Collider2D>().bounds.size.x / scale.x;
        angle = attachedTo.angle + attachAngle;
    }

    protected override void updateIndestructableObject()
    {
        if (attachedTo != null && attachedTo.active)
        {
            if (originalWidth * scale.x != width)
            {
                Vector2 newScale = scale;
                newScale.x = width / originalWidth;
                scale = newScale;
            }

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
