using UnityEngine;
using System.Collections;

public abstract class IndestructableObject : InteractiveObject
{
    protected abstract void startIndestructableObject();
    protected override void startInteractiveObject()
    {
        if (level == null)
        {
            Debug.Log("Destroying " + this + " since level is null when it is being created.");
            Destroy(this);
        }
        else
        {
            startIndestructableObject();
            level.addToGame(this);
        }
    }

    protected abstract void updateIndestructableObject();
    protected override void updateInteractiveObject()
    {
        updateIndestructableObject();
    }

    protected abstract void destroyIndestructableObject();
    protected override void destroyInteractiveObject()
    {
        destroyIndestructableObject();
        if (level != null && level.indestructables != null)
        {
            level.removeFromGame(this);
        }
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
