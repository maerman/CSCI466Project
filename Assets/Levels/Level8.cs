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

public class Level8 : Level
{
    public override int levelNumber
    {
        get
        {
            return 8;
        }
    }

    public override string levelName
    {
        get
        {
            return "Eight";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.displayMessage("Survive for 5 minutes", 3);

        for (int i = 0; i < 2; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            Rammer current = (Rammer)createObject("RammerPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            SputteringDebris current = (SputteringDebris)createObject("SputteringDebrisPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        ChargedShots charged = (ChargedShots)createObject("ChargedShotPF");

        spawnTimer = (int)(spawnTimeSecs * updatesPerSec);
    }

    private const float MESSAGE_SECS = 5;
    private TimeSpan levelTime = new TimeSpan(0, 5, 0);
    private int remaining = 0;
    private int wave = 0;
    private int spawnTimer = 0;
    private float spawnTimeSecs = 60;
    private bool win = false;

    protected override void updateLevel()
    {
        remaining = 0;

        foreach (DestructableObject item in destructables)
        {
            if (item.team <= 0)
            {
                remaining++;
            }
        }

        spawnTimer--;

        if (spawnTimer <= 0)
        {
            spawnTimer = (int)(spawnTimeSecs * updatesPerSec);

            wave++;
            switch (wave)
            {
                case 1:
                    IngameInterface.displayMessage("Wave 1", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 2:
                    IngameInterface.displayMessage("Wave 2", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 3:
                    IngameInterface.displayMessage("Wave 3", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 4:
                    IngameInterface.displayMessage("Wave 4", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity(current.maxSpeed);
                    }
                    break;
                default:
                    win = true;
                    break;
            }
        }
    }

    public override string progress
    {
        get
        {
            return base.progress + ": " + (levelTime - duration).TotalSeconds.ToString() + " seconds remaining.";
        }
    }

    protected override void endLevel()
    {
        
    }
    
   /*
    protected override bool won()
    {
        return win;
    }
    */
    
    protected override bool lost()
    {
        if ((levelTime - duration).TotalMilliseconds <= 0)
            return true;
        else
            return false;
    }
}
