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

public class Level10 : Level
{
    public override int levelNumber
    {
        get
        {
            return 10;
        }
    }

    public override string levelName
    {
        get
        {
            return "Ten";
        }
    }

    private Vector2 minSize;
    private Vector2 shrinkAmount;
    private const int SHRINK_SECONDS = 300;

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(100, 80); //set the level size
        minSize = new Vector2(10, 8);
        shrinkAmount = (levelSize - minSize) / (SHRINK_SECONDS * updatesPerSec);
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.displayMessage("Kill all enemies before space collapses!", 3);

        for (int i = 0; i < 6; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(20), random.Next(100));
        }

        for (int i = 0; i < 2; i++)
        {
            HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 4; i++)
        {
            Blob current = (Blob)createObject("BlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 3; i++)
        {
            GravityWell current = (GravityWell)createObject("GravityWellPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            Rammer current = (Rammer)createObject("RammerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 4; i++)
        {
            SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //RubberyDebris current = (RubberyDebris)createObject("RubberyDebrisPF", getRandomPosition(), getRandomAngle(), 20);
        }

        for (int i = 0; i < 2; i++)
        {
            //SputteringDebris current = (SputteringDebris)createObject("SputteringDebrisPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 4; i++)
        {
            RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            IndestructableDebris current = (IndestructableDebris)createObject("IndestructableDebrisPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(15), random.Next(10));
        }

        for (int i = 0; i < 1; i++)
        {
            //RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
        for (int i = 0; i < 1; i++)
        {
            //GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
        for (int i = 0; i < 1; i++)
        {
            //BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        LazerSword sword = (LazerSword)createObject("LazerSwordPF");
        MultiShots multiShot = (MultiShots)createObject("MultiShotPF");
        HomingMissiles homing = (HomingMissiles)createObject("HomingMissilesPF");
        HomingMines mines = (HomingMines)createObject("HomingMinesPF");
        GravityWellController well = (GravityWellController)createObject("GravityWellControllerPF");
        ChargedShots charged = (ChargedShots)createObject("ChargedShotPF");
        RapidShots rapid = (RapidShots)createObject("RapidShotsPF");
        LazerBeam beam = (LazerBeam)createObject("LazerBeamPF");
        Armor armor = (Armor)createObject("ArmorPF");
        Accelerant accelerant = (Accelerant)createObject("AccelerantFP");
        Heal heal = (Heal)createObject("HealPF");
        Shield shield = (Shield)createObject("ShieldPF");
    }

    private bool levelLost = false;

    protected override void updateLevel()
    {
        if (levelSize.x > minSize.x)
            levelSize -= shrinkAmount;
        else
            levelLost = true;
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
    */
    
    protected override bool lost()
    {
        //add loss conditions here, if player dies then its always loss

        return levelLost;
    }

}
