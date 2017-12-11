// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;

public class LevelSurvival : Level
{
    public override int levelNumber
    {
        get
        {
            return int.MinValue;
        }
    }

    public override string levelName
    {
        get
        {
            return "Survival";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");

        //levelSize = new Vector2(80, 60); //set the level size

        createObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.displayMessage("Survive for as long as you can.", 2.5f);
    }

    int type = 0;
    int round = 1;
    private const int NUM_TYPES = 12;

    protected override void updateLevel()
    {
        if (type > NUM_TYPES)
        {
            type = 1;
            round++;
        }

        switch (type)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            default:
                break;
        }

        type++;

        for (int i = 0; i < round; i++)
        {
            SputteringDebris current = (SputteringDebris)createObject("SputteringDebrisPF", getRandomGameEdge(), getRandomAngle());
            Heal heal = (Heal)createObject("HealPF");
        }

        for (int i = 0; i < round; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle(), getRandomVelocity(20), random.Next(100));
            MultiShots multiShot = (MultiShots)createObject("MultiShotPF");
        }

        for (int i = 0; i < round; i++)
        {
            LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomGameEdge(), getRandomAngle());
            LazerSword sword = (LazerSword)createObject("LazerSwordPF");
        }

        for (int i = 0; i < round; i++)
        {
            RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomGameEdge(), getRandomAngle());
            HomingMissiles homing = (HomingMissiles)createObject("HomingMissilesPF");
        }

        for (int i = 0; i < round; i++)
        {
            MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
            GravityWellController well = (GravityWellController)createObject("GravityWellControllerPF");
        }

        for (int i = 0; i < round; i++)
        {
            RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
            LazerBeam beam = (LazerBeam)createObject("LazerBeamPF");
        }

        for (int i = 0; i < round; i++)
        {
            GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomGameEdge(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            RapidShots rapid = (RapidShots)createObject("RapidShotsPF");
        }

        for (int i = 0; i < round; i++)
        {
            RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomGameEdge(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            Armor armor = (Armor)createObject("ArmorPF");
        }

        for (int i = 0; i < round; i++)
        {
            BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomGameEdge(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
            ChargedShots charged = (ChargedShots)createObject("ChargedShotPF");
        }

        for (int i = 0; i < round; i++)
        {
            SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
            HomingMines mines = (HomingMines)createObject("HomingMinesPF");
        }

        for (int i = 0; i < round; i++)
        {
            Rammer current = (Rammer)createObject("RammerPF", getRandomGameEdge(), getRandomAngle());
            Accelerant accelerant = (Accelerant)createObject("AccelerantFP");
        }

        for (int i = 0; i < round; i++)
        {
            LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
            Shield shield = (Shield)createObject("ShieldPF");
        }   
    }

    protected override void endLevel()
    {

    }

    /*
    public override string progress
    {
        get
        {
            //return a string representing the progress though the level here
            //default is stating how many enemies are remaining in the level

            return "";
        }
    }
    */
    
    protected override bool won()
    {
        //no win conditions, play until you die

        return false;
    }
    
    /*
    protected override bool lost()
    {
        //add loss conditions here, if player dies then its always loss

        return false;
    }
    */
}
