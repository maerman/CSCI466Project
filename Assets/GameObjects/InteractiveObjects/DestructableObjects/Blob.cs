using UnityEngine;
using System.Collections.Generic;

public abstract class BlobBehaviour
{
    public float magnitude = 1;

    public abstract void update(Blob thisBlob);

    public abstract bool combine(BlobBehaviour other);

    public virtual BlobBehaviour clone()
    {
        return (BlobBehaviour)this.MemberwiseClone();
    }
}

public class Blob : DestructableObject
{
    public float damageMultiplier = 6f;
    public float mergeCooldownSecs = 0.25f;
    private int mergeTimer = 0;
    public float maxSize = 1.5f;
    public LinkedList<BlobBehaviour> behaviors = new LinkedList<BlobBehaviour>();

    public void mergeWith(Blob other)
    {
        other.mergeTimer = -1;
        mergeTimer = (int)(mergeCooldownSecs * level.updatesPerSec);

        float portionThis = mass / (mass + other.mass);
        float portionOther = other.mass / (mass + other.mass);

        mass += other.mass;
        health += other.health;
        maxHealth += other.maxHealth;

        float area = (float)(scale.x * scale.x / 4 * System.Math.PI);
        area += (float)(other.scale.x * other.scale.x / 4 * System.Math.PI);
        area = (float)(System.Math.Sqrt(area  * 4/ System.Math.PI));
        scale = new Vector2(area, area);
        
        position = position * portionThis + other.position * portionOther;
        angle = angle * portionThis + other.angle * portionOther;
        angularVelocity = angularVelocity * portionThis + other.angularVelocity * portionOther;
        velocity = velocity * portionThis + other.velocity * portionOther;

        color = color * portionThis + other.color * portionOther;

        foreach (BlobBehaviour item in other.behaviors)
        {
            behaviors.AddFirst(item);
        }
        other.behaviors.Clear();

        if (behaviors.Count > 1)
        {
            LinkedListNode<BlobBehaviour> current = behaviors.First;

            while (current != null && current.Next != null)
            {
                LinkedListNode<BlobBehaviour> next = current.Next;
                do
                {
                    if (current.Value.combine(next.Value))
                    {
                        LinkedListNode<BlobBehaviour> toRemove = next;
                        next = next.Next;
                        behaviors.Remove(toRemove);
                    }
                    else
                    {
                        next = next.Next;
                    }

                } while (next != null);

                current = current.Next;
            }
        }

        other.destroyThis();
    }

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != this.team)
        {
            other.damageThis(scale.x * damageMultiplier);
        }
        else if (other.GetType() == typeof(Blob))
        {
            if(mergeTimer == 0 && ((Blob)(other)).mergeTimer == 0 && scale.x + other.scale.x < maxSize)
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
            other.damageThis(scale.x * damageMultiplier);
        }
    }

    protected virtual void startBlob() { }
    protected override void startDestructableObject()
    {
        startBlob();
    }

    protected override void updateDestructableObject()
    {
        if (mergeTimer > 0)
        {
            mergeTimer--;
        }

        foreach (BlobBehaviour item in behaviors)
        {
            item.update(this);
        }
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
                    current.team = team;
                    current.color = color;
                    current.mergeTimer = (int)(mergeCooldownSecs * level.updatesPerSec);

                    foreach (BlobBehaviour item in behaviors)
                    {
                        BlobBehaviour temp = item.clone();
                        temp.magnitude /= pieces;
                        current.behaviors.AddFirst(temp);
                    }
                }
                mergeTimer = -1;
                destroyThis();
            }
            else
            {
                base.damageThis(damage);
            }
        }
    }
}
