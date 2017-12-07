using UnityEngine;
using System.Collections;

/// <summary>
/// IndestructableObjects are InteractiveObjects which have no health and thus can't
/// be destroyed though damage (as opposed to DestructableObjects)
/// </summary>
public abstract class IndestructableObject : InteractiveObject
{
    //called shortly after this IndestructableObject is created
    protected abstract void startIndestructableObject();
    protected override void startInteractiveObject()
    {
        //if there is no level, this should not exist, so it is destoryed 
        if (level == null)
        {
            Debug.Log("Destroying " + this + " since level is null when it is being created.");
            Destroy(this.gameObject);
        }
        else
        {
            startIndestructableObject();

            //add this to the Level's lists of what it contains
            level.addToGame(this);
        }
    }

    // Called every time the game is FixedUpated, 50 times a second by default
    protected abstract void updateIndestructableObject();
    protected override void updateInteractiveObject()
    {
        updateIndestructableObject();
    }

    // Called right before this IndestructableObject is destroyed
    // removes this from the Level's lists
    protected abstract void destroyIndestructableObject();
    protected override void destroyInteractiveObject()
    {
        destroyIndestructableObject();
        if (level != null && level.indestructables != null)
        {
            level.removeFromGame(this);
        }
    }

    /// <summary>
    /// Called by Unity when this GameObject collides with another GameObject
    /// Calls another method depending on the collision type to categorize the collision
    /// </summary>
    /// <param name="collision">holds the properties of the collision</param>
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
