// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;

public class Level5 : Level
{
    public override int levelNumber
    {
        get
        {
            return 5;
        }
    }

    public override string levelName
    {
        get
        {
            return "Five";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.displayMessage("Survive the onslaught!", 3);

        for (int i = 0; i < 6; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        HomingMines mines = (HomingMines)createObject("HomingMinesPF");
    }

    private int spawnTimer = 0;
    private float spawnTimeSecs = 60;

    protected override void updateLevel()
    {
        spawnTimer--;

        if (spawnTimer <= 0)
        {
            spawnTimer = (int)(spawnTimeSecs * updatesPerSec);

            for (int i = 0; i < 1; i++)
            {
                Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle());
                current.velocity = getRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
                current.velocity = getRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                current.velocity = getRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomGameEdge(), getRandomAngle());
                current.velocity = getRandomVelocity(current.maxSpeed);
            }
        }

    }

    public override string progress
    {
        get
        {
            return base.progress + "  Seconds until next spawn: " + (int)(spawnTimer / secsPerUpdate);
        }
    }

    protected override void endLevel()
    {
        
    }
    
    /*
    protected override bool won()
    {
        //add win conditinos here, default is when all enimes die    

        return win;
    }

    
    protected override bool lost()
    {
        //add loss conditions here, if player dies then its always loss

        return false;
    }
    */
}
