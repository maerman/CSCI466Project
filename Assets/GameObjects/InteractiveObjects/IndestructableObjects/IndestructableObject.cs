using UnityEngine;
using System.Collections;

public abstract class IndestructableObject : InteractiveObject
{
    private void OnDestroy()
    {
        if (level != null && level.indestructables != null)
        {
            level.removeFromGame(this);
        }
        inPlay = false;
    }

    protected abstract void startIndestructableObject();

    protected override void startInteractiveObject()
    {
        startIndestructableObject();
        level.addToGame(this);
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
