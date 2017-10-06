using UnityEngine;
using System.Collections;

public abstract class DestructableObject : InteractiveObject
{
    public float maxHealth = 1000;
    public float health = 100;
    private float theArmor = 1;

    public float armor
    {
        get
        {
            return theArmor;
        }
        set
        {
            if (armor >= 0)
            { 
                theArmor = armor;
            }
        }
    }

    public virtual void damageThis(float damage)
    {
        if (damage > armor)
        {
            health -= (damage - armor);
        }
    }

    protected virtual void kineticDamage(Collision2D collision)
    {
        damageThis(collision.relativeVelocity.magnitude * mass / 1000f);
    }

    private void OnDestroy()
    {
        if (this.GetType() != typeof(Player))
        {
            level.destructables.Remove(this);
        }
    }

    protected abstract void startDestructableObject();

    protected override void startInteractiveObject()
    {
        startDestructableObject();
        if (this.GetType() != typeof(Player))
        {
            level.destructables.Add(this);
        }
    }

    protected abstract void updateDestructableObject();

    protected override void updateInteractiveObject()
    {
        updateDestructableObject();

        if (health <= 0)
        {
            destroyThis();
        }       
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();

        kineticDamage(collision);

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
