using UnityEngine;
using System.Collections;

public class Asteroid :DestructableObject
{
    public float minSize = 0.3f;

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {

    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {

    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {

    }

    protected override void playerCollision(Player other, Collision2D collision)
    {

    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        
    }

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
                    for (int i = 0; i < pieces; i++)
                    {
                        float theAngle = i * 360.0f / pieces + angle;
                        Asteroid current = (Asteroid)level.createObject("AsteroidPF", position + new Vector2(1, 0).rotate(theAngle), angle,
                            velocity + new Vector2(pieces, 0).rotate(theAngle), angularVelocity + pieces, theScale);

                        current.mass = mass / pieces;
                        current.health = health / pieces;
                    }

                    if (pieces > 0)
                    {
                        destroyThis();
                    }
                }
            }
        }

    }
}
