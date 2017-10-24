using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GravityWell : IndestructableObject
{
    public float gravity = 1f;
    public float damage = 10;

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;
        other.damageThis(damage);
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
            
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;
        other.damageThis(damage);
    }

    protected override void startIndestructableObject()
    {
        mass = 1000;
    }

    protected override void updateIndestructableObject()
    {
        foreach (Player item in level.players)
        {
            Vector2 pull = gravity / (float)Math.Pow(distanceFrom(item), 2) * -vector2From(item).normalized;
            item.modifyVelocityAbsolute(pull);
        }

        foreach (DestructableObject item in level.destructables)
        {
            Vector2 pull = gravity / (float)Math.Pow(distanceFrom(item), 2) * -vector2From(item).normalized;
            item.modifyVelocityAbsolute(pull);
        }

        foreach (IndestructableObject item in level.indestructables)
        {
            if (item.GetType() != typeof(GravityWell))
            {
                Vector2 pull = gravity / (float)Math.Pow(distanceFrom(item), 2) * -vector2From(item).normalized;
                item.modifyVelocityAbsolute(pull);
            }
        }
    }
}
