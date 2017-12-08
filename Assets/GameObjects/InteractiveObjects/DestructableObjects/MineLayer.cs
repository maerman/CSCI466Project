// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MineLayers is a DestructableObject that creates a HomingMine every so often and moves away from enimes
/// </summary>
public class MineLayer : DestructableObject
{
    private LinkedList<HomingMine> mines = new LinkedList<HomingMine>();
    public int maxMines = 10;
    public float mineLayWaitSecs = 4;
    private int layMineTimer = 0;
    public float damage = 15;
    public float stayAwayDistance = 15f;
    public float turnSpeed = 0.5f;
    public float acceleration = 0.2f;
    public Vector2 offset = new Vector2(0, -2);

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
        //remove HomingMine entries of HomingMines that no longer exist
        List<HomingMine> remove = new List<HomingMine>();
        foreach (HomingMine item in mines)
        {
            if (item == null || !item.active)
                remove.Add(item);
        }
        foreach (HomingMine item in remove)
            mines.Remove(item);

        if (layMineTimer > 0)
            layMineTimer--;

        //lay a new mine if it is time to
        if (layMineTimer <= 0)
        {
            //reset the layMineTimer
            layMineTimer = (int)(mineLayWaitSecs * level.updatesPerSec);

            //destroy and remove the first mine if there are too many
            if (mines.Count >= maxMines * difficultyModifier)
            {
                HomingMine firstMine = mines.First.Value;
                firstMine.destroyThis();
                mines.Remove(firstMine);
            }

            //create a new mine behind this 
            HomingMine mine = (HomingMine)level.createObject("HomingMinePF", offset.rotate(angle) + position, 0, 0);

            mine.damage = damage;
            mine.team = team;

            mines.AddLast(mine);
        }

        //turn and move away from the closest enemy InteractiveObject if it is too close, or just move forward
        SpaceObject turnFrom = closestObject(level.getTypes(true, true, true, false), false);

        if (turnFrom != null && distanceFrom(turnFrom) < stayAwayDistance)
            turnTowards(turnFrom, -turnSpeed);

        moveForward(acceleration);
    }
}
