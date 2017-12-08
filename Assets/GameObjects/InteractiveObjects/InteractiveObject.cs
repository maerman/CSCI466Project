// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// InteractiveObject is a SpaceObject that does interact with physics, having a Ridgidbody2D.
/// Its velocity and angularVelocity are updated automatically with physics.
/// </summary>
public abstract class InteractiveObject : SpaceObject
{
    /// <summary>
    /// Since InteractiveObjects use physics, its Ridgidbody2D is used for its position
    /// </summary>
    public override Vector2 position
    {
        get
        {
            return GetComponent<Rigidbody2D>().position;
        }
        set
        {
            GetComponent<Rigidbody2D>().MovePosition(value);
        }
    }

    /// <summary>
    /// Since InteractiveObjects use physics, its Ridgidbody2D is used for its velocity
    /// </summary>
    public override Vector2 velocity
    {
        get
        {
            return GetComponent<Rigidbody2D>().velocity;
        }
        set
        {
            GetComponent<Rigidbody2D>().velocity = value;
        }
    }

    /// <summary>
    /// Since InteractiveObjects use physics, its Ridgidbody2D is used for its angle
    /// </summary>
    public override float angle
    {
        get
        {
            return GetComponent<Rigidbody2D>().rotation;
        }
        set
        {
            GetComponent<Rigidbody2D>().rotation = value;
        }
    }

    /// <summary>
    /// Since InteractiveObjects use physics, its Ridgidbody2D is used for its angularVelocity
    /// </summary>
    public override float angularVelocity
    {
        get
        {
            return GetComponent<Rigidbody2D>().angularVelocity;
        }
        set
        {
            GetComponent<Rigidbody2D>().angularVelocity = value;
        }
    }

    /// <summary>
    /// Since InteractiveObjects use physics, its Collider2D is used for its bounds
    /// </summary>
    public override Bounds bounds
    {
        get
        {
            return GetComponent<Collider2D>().bounds;
        }
    }

    /// <summary>
    /// Since InteractiveObjects use physics, its Ridgidbody2D is used for its mass
    /// </summary>
    public override float mass
    {
        get
        {
            return GetComponent<Rigidbody2D>().mass;
        }
        set
        {
            GetComponent<Rigidbody2D>().mass = value;
        }
    }

    /// <summary>
    /// If this object collides with physics or not. Disabling it will allow this to go through things
    /// while still calling collision methods. 
    /// </summary>
    public bool usesPhysics
    {
        get
        {
            return !GetComponent<Collider2D>().isTrigger;
        }
        set
        {
            GetComponent<Collider2D>().isTrigger = !value;
        }
    }

    //Called shortly after this is created
    protected abstract void startInteractiveObject();
    protected override void startObject()
    {
        startInteractiveObject();
    }

    // Called every time the game is FixedUpated, 50 times a second by default
    protected abstract void updateInteractiveObject();
    protected override void updateObject()
    {
        updateInteractiveObject();
    }

    // Called right before this is destroyed
    protected abstract void destroyInteractiveObject();
    protected override void destroyObject()
    {
        destroyInteractiveObject();
    }

    //Methods OnTriggerStay2D and OnCollsionEnter2D call depending on what it overlapped/collided with to catagorize the overlap/collsion
    protected abstract void nonInteractiveObjectCollision(NonInteractiveObject other);
    protected abstract void playerCollision(Player other, Collision2D collision);
    protected abstract void destructableObjectCollision(DestructableObject other, Collision2D collision);
    protected abstract void indestructableObjectCollision(IndestructableObject other, Collision2D collision);

    /// <summary>
    /// Called by Unity when this GameObject's collier overlaps with with another GameObject's collider
    /// Calls another method depending on the type of object it overlapped with to catagorize it
    /// </summary>
    /// <param name="other">holds the properties of the overlap</param>
    public void OnTriggerStay2D(Collider2D other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType() == (typeof(Player)))
        {
            playerCollision((Player)spaceObject, new Collision2D());
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(DestructableObject)))
        {
            destructableObjectCollision((DestructableObject)spaceObject, new Collision2D());
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(IndestructableObject)))
        {
            indestructableObjectCollision((IndestructableObject)spaceObject, new Collision2D());
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(NonInteractiveObject)))
        {
            nonInteractiveObjectCollision((NonInteractiveObject)spaceObject);
        }
    }
}
