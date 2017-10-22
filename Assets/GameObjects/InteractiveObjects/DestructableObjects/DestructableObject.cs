using UnityEngine;
using System.Collections;

public abstract class DestructableObject : InteractiveObject
{
    public float maxHealth = 1000;
    public float health = 100;
    public float armor = 1;
    public bool enemy = true;

    public virtual void damageThis(float damage)
    {
        if (damage > armor)
        {
            health -= (damage - armor);
        }
    }

    protected virtual void kineticDamage(Collision2D collision)
    {
        damageThis(collision.relativeVelocity.magnitude * collision.rigidbody.mass / mass / 100f);
    }

    private void OnDestroy()
    {
        if (this.GetType() != typeof(Player))
        {
            if (level != null && level.destructables != null)
            {
                level.destructables.Remove(this);
            }
        }
        inPlay = false;
    }

    protected abstract void startDestructableObject();

    protected override void startInteractiveObject()
    {
        startDestructableObject();
        if (this.GetType() != typeof(Player))
        {
            level.destructables.AddLast(this);
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

        if (collision.relativeVelocity.magnitude > 10)
        {
            kineticDamage(collision);
        }

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
