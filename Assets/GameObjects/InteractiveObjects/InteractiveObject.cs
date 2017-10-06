using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : SpaceObject
{

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

    public override float angle
    {
        get
        {
            return GetComponent<Rigidbody2D>().rotation;
        }
        set
        {
            GetComponent<Rigidbody2D>().MoveRotation(value);
        }
    }

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
            return GetComponent<Rigidbody2D>().mass;
        }
        set
        {
            value = GetComponent<Rigidbody2D>().mass;
        }
    }

    protected abstract void startInteractiveObject();

    protected override void startObject()
    {
        startInteractiveObject();
    }

    protected abstract void updateInteractiveObject();

    // Update is called once per frame
    protected override void updateObject()
    {
        updateInteractiveObject();
    }

    protected abstract void itemCollision(Item other);
    protected abstract void nonInteractiveObjectCollision(NonInteractiveObject other);

    public void OnTriggerEnter(Collider other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType().IsSubclassOf(typeof(Item)))
        {
            itemCollision((Item)spaceObject);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(NonInteractiveObject)))
        {
            nonInteractiveObjectCollision((NonInteractiveObject)spaceObject);
        }
    }

    protected abstract void playerCollision(Player other, Collision2D collision);
    protected abstract void destructableObjectCollision(DestructableObject other, Collision2D collision);
    protected abstract void indestructableObjectCollision(IndestructableObject other, Collision2D collision);
}
