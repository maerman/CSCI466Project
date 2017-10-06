using UnityEngine;
using System.Collections;

public abstract class NonInteractiveObject : SpaceObject
{
    private Vector2 theVelocity = new Vector2(0, 0);
    private float theAngularVelocity = 0;
    private float theMass = 0;

    public override Vector2 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }

    public override Vector2 velocity
    {
        get
        {
            return theVelocity;
        }
        set
        {
            theVelocity = value;
        }
    }

    public override float angle
    {
        get
        {
            return transform.localRotation.z;
        }
        set
        {
            transform.localRotation = new Quaternion(0, 0, value, 0);
        }
    }

    public override float angularVelocity
    {
        get
        {
            return theAngularVelocity;
        }

        set
        {
            theAngularVelocity = value;
        }
    }

    public override Vector2 scale
    {
        get
        {
            return transform.localScale;
        }

        set
        {
            transform.localScale = value;
        }
    }

    public override float mass
    {
        get
        {
            return theMass; 
        }

        set
        {
            theMass = value;
        }
    }

    private void OnDestroy()
    {
        if (this.GetType() != typeof(Player))
        {
            level.nonInteractives.Remove(this);
        }
    }

    protected abstract void startNonInteractiveObject();

    protected override void startObject()
    {
        startNonInteractiveObject();
        level.nonInteractives.Add(this);
    }

    protected abstract void updateNonInteractiveObject();

    // Update is called once per frame
    protected override void updateObject()
    {
        updateNonInteractiveObject();
        position += velocity;
        angle += angularVelocity;
    }


    protected abstract void playerCollision(Player other);
    protected abstract void destructableObjectCollision(DestructableObject other);
    protected abstract void indestructableObjectCollision(IndestructableObject other);

    public void OnTriggerEnter(Collider other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType().IsSubclassOf(typeof(Player)))
        {
            playerCollision((Player) spaceObject);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(DestructableObject)))
        {
            destructableObjectCollision((DestructableObject)spaceObject);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(IndestructableObject)))
        {
            indestructableObjectCollision((IndestructableObject)spaceObject);
        }
    }


}
