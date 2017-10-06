using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : DestructableObject
{
    public int playerNum = 0;
    public float acceleration = 0.4f;
    public float turnSpeed = 100;

    private int sinceLastShot = 0;
    public float shootTime = 30;

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
        Controls.setDefaultControls();
    }

    protected override void updateDestructableObject()
    {
        Controls.updateFromInput();

        Vector2 move = new Vector2();
        if (Controls.players[playerNum].forward)
        {
            move.y += 1;
        }
        if (Controls.players[playerNum].backward)
        {
            move.y -= 1;
        }
        if (Controls.players[playerNum].straifL)
        {
            move.x -= 1;
        }
        if (Controls.players[playerNum].straifR)
        {
            move.x += 1;
        }

        move.Normalize();
        move *= acceleration;

        modifyVelocityRelative(move);

        if (Controls.players[playerNum].turnL)
        {
            angularVelocity += turnSpeed;
        }

        if (Controls.players[playerNum].turnR)
        {
            angularVelocity -= turnSpeed;
        }

        sinceLastShot++;
        if (Controls.players[playerNum].shoot && sinceLastShot >= shootTime)
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

    protected override void itemCollision(Item other)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        
    }
}
