using UnityEngine;
using System.Collections;

public abstract class DestructableObject : InteractiveObject
{
    public float maxHealth = 1000;
    public float health = 100;
    public float armor = 1;

    private GameObject healthBar;

    public virtual void damageThis(float damage)
    {
        if (damage > armor)
        {
            health -= (damage - armor);
        }
    }

    protected abstract void startDestructableObject();
    protected override void startInteractiveObject()
    {
        startDestructableObject();
        if (this.GetType() != typeof(Player))
        {
            level.addToGame(this);
        }

        healthBar = Instantiate(Resources.Load("HealthBarPF"), position, Quaternion.Euler(0, 0, 0)) as GameObject;
        healthBar.GetComponent<HealthBar>().owner = this;
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

    protected abstract void destroyDestructableObject();
    protected override void destroyInteractiveObject()
    {
        destroyDestructableObject();
        if (GetType() == typeof(Player))
        {
            if (level != null && level.players != null)
            {
                level.players.Remove((Player)this);
            }
        }
        else
        {
            if (level != null && level.destructables != null)
            {
                level.removeFromGame(this);
            }
        }
        Destroy(healthBar);
    }

    void OnCollisionEnter2D(Collision2D collision)
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
