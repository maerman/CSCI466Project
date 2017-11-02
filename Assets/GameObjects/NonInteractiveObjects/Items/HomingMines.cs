using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingMines : Item
{
    public float mineDamage = 25f;
    public float mineHealth = 20f;
    public float mineAcceleration = 0.5f;
    public float mineMaxSpeed = 15f;
    public float findTargetProximity = 5f;
    public float loseTargetProximity = 10f;
    public Vector2 offset = new Vector2(0, -2);
    public int maxMines = 5;

    public float layTimeSecs = 1f;
    private int layTimer = 0;

    private LinkedList<HomingMine> mines = new LinkedList<HomingMine>();

    protected override void dropItem()
    {
        foreach (HomingMine item in mines)
        {
            item.destroyThis();
        }

        mines.Clear();
    }

    protected override void holdingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        List<HomingMine> remove = new List<HomingMine>();
        foreach (HomingMine item in mines)
        {
            if (item == null || !item.active)
            {
                remove.Add(item);
            }
        }
        foreach (HomingMine item in remove)
        {
            mines.Remove(item);
        }

        if (layTimer > 0)
        {
            layTimer--;
        }
        else if (use)
        {
            Vector2 direction = holder.velocity.normalized;
            direction.x *= -1;
            HomingMine mine = (HomingMine)level.createObject("HomingMinePF", holder.position + offset.rotate(direction.getAngle()));
            mine.acceleration = mineAcceleration;
            mine.damage = mineDamage;
            mine.health = mineHealth;
            mine.maxSpeed = mineMaxSpeed;
            mine.color = color;
            mine.team = holder.team;

            if (mines.Count >= maxMines)
            {
                mines.Last.Value.destroyThis();
                mines.RemoveLast();
            }
            mines.AddFirst(mine);

            layTimer = (int)(layTimeSecs * level.updatesPerSec);
        }
    }

    protected override void pickupItem()
    {
        
    }
}
