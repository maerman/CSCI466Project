using UnityEngine;
using System.Collections;

public abstract class IndestructableObject : InteractiveObject
{
    private void OnDestroy()
    {
        level.indestructables.Remove(this);
        inPlay = false;
    }

    protected abstract void startIndestructableObject();

    protected override void startInteractiveObject()
    {
        startIndestructableObject();
        level.indestructables.AddLast(this);
    }

    protected abstract void updateIndestructableObject();

    protected override void updateInteractiveObject()
    {
        updateIndestructableObject();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType() == (typeof(Player)))
        {
            playerCollision((Player)spaceObject, collision);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(DestructableObject)))
        {
            destructableObjectCollision((DestructableObject)spaceObject, collision);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(IndestructableObject)))
        {
            indestructableObjectCollision((IndestructableObject)spaceObject, collision);
        }
    }
}
