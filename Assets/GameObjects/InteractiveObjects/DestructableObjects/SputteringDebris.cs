// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// SputteringDebris is a DestructableObject that switches between accelerating forward while turning in a certain direction
/// and waiting (gliding forward at its current speed) for a random durations. It damages any enemy DestructableObject it
/// collides with. 
/// </summary>
public class SputteringDebris : DestructableObject
{
    public float damage = 15f;
    public float acceleration = 0.5f;
    public float turnSpeed = 1f;
    public float maxAccelerateDurationSecs = 2f;

    private int timer = 0;
    private bool accelerate = false;

    protected override void destroyDestructableObject()
    {
        
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (team != other.team)
        {
            other.damageThis(damage);
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
        if (team != other.team)
        {
            other.damageThis(damage);
        }
    }

    protected override void startDestructableObject()
    {
        
    }

    protected override void updateDestructableObject()
    {
        //if timer is up, toggle between accelerating and reset the timer
        if (timer <= 0)
        {
            accelerate = !accelerate;
            timer = level.random.Next((int)(maxAccelerateDurationSecs * level.updatesPerSec));
        }

        //if accelerating, turn in a direction and accelerate forward
        if (accelerate)
        {
            angularVelocity += turnSpeed;
            moveForward(acceleration);
        }

        timer--;
    }

    /// <summary>
    /// Takes less damage baised on difficulty
    /// </summary>
    /// <param name="damage"></param>
    public override void damageThis(float damage)
    {
        if (damage > armor * difficultyModifier)
        {
            health -= (damage - armor - difficultyModifier * 2f);
        }
    }
}
