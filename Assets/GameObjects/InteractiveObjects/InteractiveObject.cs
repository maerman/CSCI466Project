using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InteractiveObject : SpaceObject
{
    public byte team = 0;

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

    public override Bounds bounds
    {
        get
        {
            return GetComponent<Collider2D>().bounds;
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
            GetComponent<Rigidbody2D>().mass = value;
        }
    }

    public bool usesPhysics
    {
        get
        {
            return GetComponent<Collider2D>().enabled;
        }
        set
        {
            GetComponent<Collider2D>().enabled = value;
        }
    }

    public T closestObject<T>(IEnumerable<IEnumerable<T>> objectLists, bool sameTeam) where T : InteractiveObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            T temp = closestObject<T>(item, sameTeam);

            if (temp != null)
            {
                float tempDistance = distanceFrom(temp);

                if (tempDistance < closestDistance)
                {
                    closest = temp;

                }
                closestDistance = tempDistance;
            }
        }

        return closest;
    }

    public T closestObject<T>(IEnumerable<T> objectList, bool sameTeam) where T : InteractiveObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (((sameTeam && item.team == team) || (!sameTeam && item.team != team)) && item != this && item.inPlay)
            {
                float distance = this.distanceFrom(item);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = item;
                }
            }
        }
        return closest;
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

    protected abstract void nonInteractiveObjectCollision(NonInteractiveObject other);

    public void OnTriggerStay2D(Collider2D other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType().IsSubclassOf(typeof(NonInteractiveObject)))
        {
            nonInteractiveObjectCollision((NonInteractiveObject)spaceObject);
        }
    }

    protected abstract void playerCollision(Player other, Collision2D collision);
    protected abstract void destructableObjectCollision(DestructableObject other, Collision2D collision);
    protected abstract void indestructableObjectCollision(IndestructableObject other, Collision2D collision);
}
