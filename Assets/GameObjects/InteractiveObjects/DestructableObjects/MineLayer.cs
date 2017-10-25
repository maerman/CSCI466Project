using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineLayer : DestructableObject
{
    LinkedList<HomingMine> mines = new LinkedList<HomingMine>();
    public int maxMines = 10;
    public float mineLayWaitSecs = 4;
    private int updatesUntilNextMine = 0;
    public float damage = 15;
    public float stayAwayDistance = 15f;
    public float turnSpeed = 0.5f;
    public float acceleration = 0.2f;

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
        List<HomingMine> remove = new List<HomingMine>();
        foreach (HomingMine item in mines)
        {
            if (item == null || !item.inPlay)
            {
                remove.Add(item);
            }
        }

        foreach (HomingMine item in remove)
        {
            mines.Remove(item);
        }

        if (updatesUntilNextMine > 0)
        {
            updatesUntilNextMine--;
        }

        if (updatesUntilNextMine <= 0)
        {
            if (mines.Count >= maxMines)
            {
                HomingMine firstMine = mines.First.Value;
                firstMine.destroyThis();
                mines.Remove(firstMine);
            }

            HomingMine mine = (HomingMine)level.createObject("HomingMinePF", new Vector2(0, -2).rotate(angle) + position, 0, 0);

            mine.damage = damage;
            mine.team = team;

            mines.AddLast(mine);

            updatesUntilNextMine = (int)(mineLayWaitSecs * level.updatesPerSec);
        }

        IEnumerable<InteractiveObject>[] awayFrom = new IEnumerable<InteractiveObject>[3];
        awayFrom[0] = level.players;
        awayFrom[1] = level.destructables;
        awayFrom[2] = level.indestructables;

        InteractiveObject turnFrom = closestObject<InteractiveObject>(awayFrom, false);

        if (turnFrom != null && distanceFrom(turnFrom) < stayAwayDistance)
        {
            Debug.Log(distanceFrom(turnFrom));
            rotateTowards(turnFrom, -turnSpeed);
        }

        moveForward(acceleration);
    }
}
