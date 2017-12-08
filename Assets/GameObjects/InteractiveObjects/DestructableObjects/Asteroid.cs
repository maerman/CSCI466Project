// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// Aserioids are DestructableObjects that break apart when damaged.
/// They deal damage to DestructableObjects baised on their size. 
/// </summary>
public class Asteroid :DestructableObject
{
    public float minSize = 0.3f;
    public float damageMultiply = 5f;

    protected override void destroyDestructableObject()
    {

    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.damageThis(damageMultiply * mass * difficultyModifier);
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {

    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        other.damageThis(damageMultiply * mass * difficultyModifier);
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        
    }

    /// <summary>
    /// If the damage gets past thie Asteroid's armor and this Asteroid is not too small
    /// then it will be broken up into smaller pieces baised on the damage.
    /// </summary>
    /// <param name="damage"></param>
    public override void damageThis(float damage)
    {
        if (damage - armor > 0)
        {
            if (damage - armor > health)
            {
                destroyThis();
            }
            else if (scale.x <= minSize)
            {
                base.damageThis(damage);
            }
            else
            {
                int pieces = (int)(damage * 8 / health) + 1;

                base.damageThis(damage);

                //find the size of the new Asteroids baised on the number of them and 
                //the area of the old Asteroid
                //if the new Asteroids would be too small, decrease the number of them 
                float area;
                float theScale;
                do
                {
                    pieces -= 1;
                    area = (float)(scale.x * scale.x / 4 * System.Math.PI);
                    area /= pieces;
                    theScale = (float)(System.Math.Sqrt(area / System.Math.PI * 4));
                } while (theScale < minSize);

                if (pieces > 1)
                {
                    //create each new Asteroid
                    for (int i = 0; i < pieces; i++)
                    {
                        //make each new Asteroid and have them spread out from the old Asteroid's position
                        float theAngle = i * 360.0f / pieces + angle;
                        Asteroid current = (Asteroid)level.createObject("AsteroidPF", position + new Vector2(size.x / pieces, 0).rotate(theAngle),
                            angle, velocity + new Vector2(pieces, 0).rotate(theAngle), angularVelocity + pieces, theScale);

                        current.mass = mass / pieces;
                        current.health = health / pieces;
                        current.team = team;
                        current.color = color;
                    }

                    //Destroy the old Asteroid, since it is no longer needed
                    destroyThis();
                }
            }
        }

    }
}
