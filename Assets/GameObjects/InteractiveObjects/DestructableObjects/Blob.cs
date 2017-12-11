// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Each BlobBehavior represents a certain type of behavior that a Blob can have.
/// When Blobs combine, they also combine their behavior proportionally
/// </summary>
public abstract class BlobBehaviour
{
    public float magnitude = 1;

    public abstract void update(Blob thisBlob);

    /// <summary>
    /// Combines the given BlobBehavior with this one if they are of the same type,
    /// adding their magnitudes and any other setting members. The combined 
    /// BlobBehavior is this one, the other one can be deleted if the combine happened (this returned true).
    /// </summary>
    /// <param name="other">BlobBehavior to combine with this.</param>
    /// <returns>returns true if they combined, false if they didn't</returns>
    public abstract bool combine(BlobBehaviour other);

    public virtual BlobBehaviour clone()
    {
        return (BlobBehaviour)this.MemberwiseClone();
    }
}

/// <summary>
/// Blobs are DestructableObjects that break apart when damaged and merge when they touch. 
/// They deal damage to their enemies baised on their size. 
/// </summary>
public class Blob : DestructableObject
{
    public float damageMultiplier = 6f;
    public float mergeCooldownSecs = 0.25f;
    private int mergeTimer = 0;
    public float maxSize = 1.5f;
    public LinkedList<BlobBehaviour> behaviors = new LinkedList<BlobBehaviour>();

    /// <summary>
    /// Merge's this Blob with the given other Blob, then the other Blob is destroyed. 
    /// </summary>
    /// <param name="other">Blob to merge with this Blob</param>
    public void mergeWith(Blob other)
    {
        other.mergeTimer = -1;
        mergeTimer = (int)(mergeCooldownSecs * level.updatesPerSec);

        //figureout which portions of each of the old Blob will make up the new Blob
        float portionThis = mass / (mass + other.mass);
        float portionOther = other.mass / (mass + other.mass);

        mass += other.mass;
        health += other.health;
        maxHealth += other.maxHealth;

        //figureout the size of the new Blob by adding the area's of the old Blobs together, 
        //then calculating the its size from this combined area
        float area = (float)(scale.x * scale.x / 4 * System.Math.PI);
        area += (float)(other.scale.x * other.scale.x / 4 * System.Math.PI);
        area = (float)(System.Math.Sqrt(area  * 4/ System.Math.PI));
        scale = new Vector2(area, area);

        //set the properties of the new Blob baised on the portions of the old Blobs
        //makeup the new one and properties of the old Blobs
        position = position * portionThis + other.position * portionOther;
        angle = angle * portionThis + other.angle * portionOther;
        angularVelocity = angularVelocity * portionThis + other.angularVelocity * portionOther;
        velocity = velocity * portionThis + other.velocity * portionOther;
        damageMultiplier = damageMultiplier * portionThis + other.damageMultiplier * portionOther;
        color = color * portionThis + other.color * portionOther;

        //add the old Blob's behaviors together to get the behaviors of the new Blob
        foreach (BlobBehaviour item in other.behaviors)
        {
            behaviors.AddFirst(item);
        }
        other.behaviors.Clear();

        //combine any of the new Blob's behaviors that are the same
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

        //Destroy the given Blob, since it will no longer be needed
        other.destroyThis();
    }

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != this.team)
        {
            other.damageThis(scale.x * damageMultiplier * difficultyModifier);
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
            other.damageThis(scale.x * damageMultiplier * difficultyModifier);
        }
    }

    protected virtual void startBlob() { }
    protected override void startDestructableObject()
    {
        startBlob();
    }

    /// <summary>
    /// Updates the Blob's behaviors and mergeTimer
    /// </summary>
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

    /// <summary>
    /// If the damage gets past Blob's armor, then the Blob is broken up into pices 
    /// baised on the damage dealt to it (minus the armor)
    /// </summary>
    /// <param name="damage"></param>
    public override void damageThis(float damage)
    {
        damage -= armor;
        if (damage > health)
        {
            damageThis(damage + armor);
        }
        else if (damage > 0)
        {
            int pieces = (int)(damage / 8f) + 2;

            if (pieces > 1)
            {
                //find the size of each new Blob by dividing the area of the old Blob by the number of 
                //new Blobs, the calculating the new Blobs size from this area
                float area = (float)(scale.x * scale.x / 4 * System.Math.PI);
                area /= pieces;
                float theScale = (float)(System.Math.Sqrt(area * 4 / System.Math.PI));

                //create each new Blob
                for (int i = 0; i < pieces; i++)
                {  
                    //make each new Blob and have them spread out from the old Blob's position
                    float theAngle = i * 360.0f / pieces + angle;
                    Blob current = (Blob)level.createObject("BlobPF", position + new Vector2(size.x / pieces, 0).rotate(theAngle), angle,
                        velocity + new Vector2(pieces, 0).rotate(theAngle), angularVelocity + pieces, theScale);

                    //give each new Blob an equal portion of the old Blob's healh, mass and behaviors
                    current.mass = mass / pieces;
                    current.health = health / pieces;
                    current.maxHealth = maxHealth / pieces;
                    foreach (BlobBehaviour item in behaviors)
                    {
                        BlobBehaviour temp = item.clone();
                        temp.magnitude /= pieces;
                        current.behaviors.AddFirst(temp);
                    }

                    current.team = team;
                    current.color = color;
                    current.mergeTimer = (int)(mergeCooldownSecs * level.updatesPerSec);
                }

                //Destory the old Blob, since it is no longer needed. Make sure it dones't try to merge in the meantime. 
                mergeTimer = -1;
                destroyThis();
            }
            else
            {
                base.damageThis(damage + armor);
            }
        }
    }
}
