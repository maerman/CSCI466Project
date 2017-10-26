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
            transform.position = new Vector3(value.x, value.y, transform.position.z);
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
            return transform.rotation.eulerAngles.z;
        }
        set
        {
            Vector3 currentEuler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, value));
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

    public override Vector2 dimentions
    {
        get
        {
            Vector2 size = GetComponent<SpriteRenderer>().bounds.size;
            return new Vector2(size.x * scale.x, size.y * scale.y);
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
        if (level != null && level.nonInteractives != null)
        {
            level.removeFromGame(this);
        }
        inPlay = false;
    }

    protected abstract void startNonInteractiveObject();

    protected override void startObject()
    {
        startNonInteractiveObject();
        level.addToGame(this);
    }

    protected abstract void updateNonInteractiveObject();

    // Update is called once per frame
    protected override void updateObject()
    {
        updateNonInteractiveObject();
        position += velocity * level.secsPerUpdate;
        angle += angularVelocity * level.secsPerUpdate;
    }


    protected abstract void playerCollision(Player other);
    protected abstract void destructableObjectCollision(DestructableObject other);
    protected abstract void indestructableObjectCollision(IndestructableObject other);

    public void OnTriggerStay2D(Collider2D other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType() == (typeof(Player)))
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
