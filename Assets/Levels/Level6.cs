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

public class Level6 : Level
{
    public override int levelNumber
    {
        get
        {
            return 6;
        }
    }

    public override string levelName
    {
        get
        {
            return "Six";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.displayMessage("Survive the attack of the Space Blobs!", 3);

        for (int i = 0; i < 4; i++)
        {
            //Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(20), random.Next(100));
        }

        for (int i = 0; i < 2; i++)
        {
            //HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 2; i++)
        {
            Blob current = (Blob)createObject("BlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 2; i++)
        {
            //GravityWell current = (GravityWell)createObject("GravityWellPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //Rammer current = (Rammer)createObject("RammerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            //RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //RubberyDebris current = (RubberyDebris)createObject("RubberyDebrisPF", getRandomPosition(), getRandomAngle(), 20);
        }

        for (int i = 0; i < 2; i++)
        {
            //SputteringDebris current = (SputteringDebris)createObject("SputteringDebrisPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 5; i++)
        {
            //IndestructableDebris current = (IndestructableDebris)createObject("IndestructableDebrisPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(15), random.Next(10));
        }

        for (int i = 0; i < 1; i++)
        {
            //RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
        for (int i = 0; i < 2; i++)
        {
            //GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
        for (int i = 0; i < 2; i++)
        {
            //BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        LazerSword sword = (LazerSword)createObject("LazerSwordPF");
        MultiShots multiShot = (MultiShots)createObject("MultiShotPF");
        HomingMissiles homing = (HomingMissiles)createObject("HomingMissilesPF");
        HomingMines mines = (HomingMines)createObject("HomingMinesPF");
        //GravityWellController well = (GravityWellController)createObject("GravityWellControllerPF");
        //ChargedShots charged = (ChargedShots)createObject("ChargedShotPF");
        RapidShots rapid = (RapidShots)createObject("RapidShotsPF");
        //LazerBeam beam = (LazerBeam)createObject("LazerBeamPF");
        Armor armor = (Armor)createObject("ArmorPF");
        Accelerant accelerant = (Accelerant)createObject("AccelerantFP");
        Heal heal = (Heal)createObject("HealPF");
        //Shield shield = (Shield)createObject("ShieldPF");
    }

    private int wave = 0;
    private int remaining = 0;
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

        if (remaining == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                //RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            }
            for (int i = 0; i < 2; i++)
            {
                //GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            }
            for (int i = 0; i < 2; i++)
            {
                //BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            }
        }
        else
        {
            win = true;
        }
    }

    protected override void endLevel()
    {
        
    }
    
    protected override bool won()
    {
        //add win conditinos here, default is when all enimes die    

        return win;
    }

    /*
    protected override bool lost()
    {
        //add loss conditions here, if player dies then its always loss

        return false;
    }
    */
}
