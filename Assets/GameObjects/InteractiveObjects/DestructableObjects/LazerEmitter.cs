// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// LazerEmitters are DestructableObjects that every so often shoot LazerShots all around it
/// </summary>
public class LazerEmitter : DestructableObject
{
    public float damage = 5f;
    public float lazerSpeed = 15f;
    public float lazerEmitSecs = 2f;
    public float lazerLifeSecs = 2f;
    public int numLazers = 8;
    public Color lazerColor = Color.red;
    public Vector2 lazerOffset = new Vector2(0, 2f);

    private int emitTimer = 100;

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
        emitTimer--;

        //emit Lazers if it is time to
        if (emitTimer <= 0)
        {
            emitTimer = (int)(lazerEmitSecs * level.updatesPerSec);

            //create LazerShots around the this LazerEmitter equally spaced angles
            for (int i = 0; i < numLazers * difficultyModifier; i++)
            {
                float currentAngle = angle + i * 360f / numLazers;
                LazerShot current = (LazerShot)level.createObject("LazerShotPF", position + lazerOffset.rotate(currentAngle), currentAngle, new Vector2(lazerSpeed, 0).toAngle(currentAngle) + velocity);
                current.damage = damage;
                current.timeToLiveSecs = lazerLifeSecs;
                current.color = lazerColor;
                current.team = team;
            }
        }
    }
}
