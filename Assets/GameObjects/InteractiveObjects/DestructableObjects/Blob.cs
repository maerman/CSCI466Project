using UnityEngine;
using System.Collections;

public class Blob : DestructableObject
{
    private bool merged = false;
    public float maxSize = 1.5f;

    public void mergeWith(Blob other)
    {
        other.merged = true;
        merged = true;

        float portionThis = mass / (mass + other.mass);
        float portionOther = other.mass / (mass + other.mass);

        mass += other.mass;
        health += other.health;
        
        float area = (float)(scale.x * scale.x / 4 * System.Math.PI);
        area += (float)(other.scale.x * other.scale.x / 4 * System.Math.PI);
        area = (float)(System.Math.Sqrt(area  * 4/ System.Math.PI));
        scale = new Vector2(area, area);
        
        position = position * portionThis + other.position * portionOther;
        angle = angle * portionThis + other.angle * portionOther;
        angularVelocity = angularVelocity * portionThis + other.angularVelocity * portionOther;
        velocity = velocity * portionThis + other.velocity * portionOther;
        
        maxHealth = health;

        other.destroyThis();
    }

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != this.team)
        {
            other.damageThis(scale.x * 4);
        }
        else if (other.GetType() == typeof(Blob))
        {
            if(!merged && !((Blob)(other)).merged && scale.x + other.scale.x < maxSize)
            {
                mergeWith((Blob)other);
            }
        }
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        if (other.team != this.team)
        {
            other.damageThis(scale.x * 4);
        }
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        merged = false;
    }

    public override void damageThis(float damage)
    {
        damage -= armor;
        if (damage > health)
        {
            destroyThis();
        }
        else if (damage > 0)
        {
            int pieces = (int)(damage / 8f) + 2;

            if (pieces > 1)
            {

                float area = (float)(scale.x * scale.x / 4 * System.Math.PI);
                area /= pieces;
                float theScale = (float)(System.Math.Sqrt(area * 4 / System.Math.PI));

                for (int i = 0; i < pieces; i++)
                {
                    float theAngle = i * 360.0f / pieces + angle;
                    Blob current = (Blob)level.createObject("BlobPF", position + new Vector2(size.x / pieces, 0).rotate(theAngle), angle,
                        velocity + new Vector2(pieces, 0).rotate(theAngle), angularVelocity + pieces, theScale);

                    current.mass = mass / pieces;
                    current.health = health / pieces;
                }
                destroyThis();
            }
            else
            {
                base.damageThis(damage);
            }
        }
    }
}
