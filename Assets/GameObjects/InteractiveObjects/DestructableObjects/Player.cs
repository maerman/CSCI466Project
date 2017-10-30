﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : DestructableObject
{
    public int playerNum = 0;
    public float acceleration = 0.4f;
    public float turnSpeed = 100;

    private float shootNextUpdates = 0;
    public float shootTimeSecs = 0.5f;
    public float shotSpeed = 15f;

    protected PlayerControls input;

    private Item[] theItems = new Item[PlayerInput.NUM_ITEMS];
    public Item[] items
    {
        get
        {
            return theItems;
        }
    }

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
        team = 1;

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

        if (level.levelNumber == 1)
        {
            LazerSword sword = (LazerSword)level.createObject("LazerSwordPF");
            sword.pickup(this);
        }
    }

    protected override void updateDestructableObject()
    {
        input = Controls.get().players[playerNum];

        Vector2 move = new Vector2();

        move.y += input.forward;
        move.y -= input.backward;
        move.x -= input.straifL;
        move.x += input.straifR;

        move.Normalize();
        move *= acceleration;

        if (input.relativeMovement)
        {
            modifyVelocityRelative(move);
        }
        else
        {
            modifyVelocityAbsolute(move);
        }

        if (input.turns)
        {
            angularVelocity += turnSpeed * input.turnL;
            angularVelocity -= turnSpeed * input.turnR;
        }
        else
        {
            double toTurn = new Vector2(input.turnL - input.turnR, input.turnUp - input.turnDown).getAngle();

            if (!double.IsNaN(toTurn))
            {
                angle = (float)toTurn;
            }
        }

        shootNextUpdates--;
        if (input.shoot && shootNextUpdates <= 0)
        {
            shootNextUpdates = shootTimeSecs * level.updatesPerSec;
            SpaceObject shot = level.createObject("LazerShotPF", new Vector2(0, 2).rotate(angle) + position, angle, shotSpeed + speed);
            shot.color = color;
        }

        for (int i = 0; i < theItems.Length; i++)
        {
            if (theItems[i] != null)
            {
                if (input.pickupDrop && input.items(i))
                {
                    theItems[i].drop();
                }
                theItems[i].holding(input.items(i));
            }
        }
    }

    protected override void destroyDestructableObject()
    {
        for (int i = 0; i < theItems.Length; i++)
        {
            if (theItems[i] != null)
            {
                theItems[i].drop();
            }
        }
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
        if (input.pickupDrop && other.GetType().IsSubclassOf(typeof(Item)))
        {
            Item item = (Item)other;
            for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
            {
                if (input.items(i))
                {
                    item.pickup(this, i);
                    return;
                }
            }
            item.pickup(this);
        }
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
        
    }

    public Player clone()
    {
        return (Player)this.MemberwiseClone();
    }
}
