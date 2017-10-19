using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : DestructableObject
{
    public int playerNum = 0;
    public float acceleration = 0.4f;
    public float turnSpeed = 100;

    private int sinceLastShot = 0;
    public int shootTime = 30;

    /*
    public virtual void damageThis(float damage)
    {
        if (damage > armor)
        {
            health -= (damage - armor);
        }
    }
    */

    protected override void startDestructableObject()
    {
        Controls.get().setDefaultControls();

        switch (playerNum)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = Color.cyan;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.magenta;
                break;
            default:
                color = Color.grey;
                break;
        }
    }

    protected override void updateDestructableObject()
    {
        Vector2 move = new Vector2();

        move.y += Controls.get().players[playerNum].forward;
        move.y -= Controls.get().players[playerNum].backward;
        move.x -= Controls.get().players[playerNum].straifL;
        move.x += Controls.get().players[playerNum].straifR;

        move.Normalize();
        move *= acceleration;

        if (Controls.get().players[playerNum].relativeMovement)
        {
            modifyVelocityRelative(move);
        }
        else
        {
            modifyVelocityAbsolute(move);
        }

        angularVelocity += turnSpeed * Controls.get().players[playerNum].turnL;
        angularVelocity -= turnSpeed * Controls.get().players[playerNum].turnR;

        sinceLastShot++;
        if (Controls.get().players[playerNum].shoot && sinceLastShot >= shootTime)
        {
            sinceLastShot = 0;
            GameObject newShot = Instantiate(Resources.Load("LazerShotPF"), new Vector2(0, 2).rotate(angle) + position, 
                Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
            LazerShot current = newShot.GetComponent<LazerShot>();
            current.velocity += this.velocity;
        }
    }

    public override void destroyThis()
    {
        //end level
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
}
