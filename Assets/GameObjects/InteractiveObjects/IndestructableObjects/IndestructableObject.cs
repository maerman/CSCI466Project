using UnityEngine;
using System.Collections;

public abstract class IndestructableObject : InteractiveObject
{
    private void OnDestroy()
    {
        if (this.GetType() != typeof(Player))
        {
            level.indestructables.Remove(this);
        }
    }

    protected abstract void startIndestructableObject();

    protected override void startInteractiveObject()
    {
        startIndestructableObject();
        level.indestructables.Add(this);
    }

    protected abstract void updateIndestructableObject();

    protected override void updateInteractiveObject()
    {
        updateIndestructableObject();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType().IsSubclassOf(typeof(Player)))
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
