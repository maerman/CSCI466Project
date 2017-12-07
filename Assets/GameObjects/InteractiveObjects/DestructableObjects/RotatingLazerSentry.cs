using UnityEngine;
using System.Collections;

/// <summary>
/// RotatingLazerSentry is a DestructableObject that rotates unil an enemy DestructableObject is
/// infront of it within a certain range that it sets as its target. It then creates a Lazer that extends towards
/// the target until the target goes out of range. 
/// </summary>
public class RotatingLazerSentry : DestructableObject
{
    public float damage = 1.1f;
    public float extendSpeed = 0.2f;
    public float maxLength = 10f;
    public float turnSpeed = 4f;
    public Color lazerColor = Color.green;

    private SpaceObject target = null;
    private Lazer lazer = null;

    protected override void destroyDestructableObject()
    {
       
    }

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
        //if target is too far away, make it no longer the target
        if (target != null && distanceFrom(target) > maxLength * difficultyModifier)
        {
            target = null;
        }

        //if there is no target, try to find one
        if (target == null)
        {
            target = closestObjectInDirection(level.getTypes(true, true, false, false), angle, false);

            //make sure the target is within the correct distance
            if (target != null && distanceFrom(target) > maxLength * difficultyModifier)
            {
                target = null;
            }
        }

        //if there is no target, turn and make sure the lazer is not made
        if (target == null)
        {
            angle += turnSpeed;

            if (lazer != null)
            {
                lazer.destroyThis();
                lazer = null;
            }
        }
        //if there is a target, turn towards it and make sure the lazer is made
        else
        {
            angularVelocity = 0f;
            turnTowards(target, Mathf.Abs(turnSpeed));

            if (lazer == null)
            {
                lazer = (Lazer)level.createObject("LazerPF", position, angle);
                lazer.attachedTo = this;
                lazer.damage = damage;
                lazer.extendSpeed = extendSpeed * difficultyModifier;
                lazer.maxLength = maxLength * difficultyModifier;
                lazer.team = team;
                lazer.color = lazerColor;
            }
        }
    }
}
