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
    int waveTimer = 0;
    private const int NUM_TYPES = 12;
    private const float WAVE_MESSAGE_TIME = 2;
    private const float ITEM_POWER_INCRIMENT = 0.5f;

    protected override void updateLevel()
    {
        waveTimer--;

        if (waveTimer <= 0)
        {
            type++;

            if (type > NUM_TYPES)
            {
                type = 1;
                round++;
            }

            switch (type)
            {
                case 1:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Suttering Debris and a " 
                        + round + " power Heal item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        SputteringDebris current = (SputteringDebris)createObject("SputteringDebrisPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    Heal heal = (Heal)createObject("HealPF");
                    heal.healthPerSecGain *= round * ITEM_POWER_INCRIMENT;
                    break;
                case 2:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Aseroid(s) and a "
                        + round + " power MultiShot item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomGameEdge(), getRandomAngle(), 0, random.Next(100));
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    MultiShots multiShot = (MultiShots)createObject("MultiShotPF");
                    multiShot.numberOfShots *= round * ITEM_POWER_INCRIMENT; 
                    break;
                case 3:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Lazer Emitters(s) and a "
                        + round + " power Lazer Sword item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomGameEdge(), getRandomAngle(), 0, random.Next(100));
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    LazerSword sword = (LazerSword)createObject("LazerSwordPF");
                    sword.swordLength *= round * ITEM_POWER_INCRIMENT;
                    break;
                case 4:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Random Turner(s) and a "
                        + round + " power Homing Missiles item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    HomingMissiles homing = (HomingMissiles)createObject("HomingMissilesPF");
                    
                    break;
                case 5:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Mine Layer(s) and a "
                        + round + " power Gravity Well Controller item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    GravityWellController well = (GravityWellController)createObject("GravityWellControllerPF");
                    well.maxGravity *= round * ITEM_POWER_INCRIMENT;
                    break;
                case 6:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Rotating Lazer Sentry(s) and a "
                        + round + " power Lazer Beam item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    LazerBeam beam = (LazerBeam)createObject("LazerBeamPF");
                    beam.beamDamge *= round * ITEM_POWER_INCRIMENT;
                    break;
                case 7:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Green Blob(s) and a "
                        + round + " power Rapid Shots item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    RapidShots rapid = (RapidShots)createObject("RapidShotsPF");
                    rapid.shotSpeed *= round * ITEM_POWER_INCRIMENT;
                    break;
                case 8:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Red Blob(s) and a "
                        + round + " power Armor item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    Armor armor = (Armor)createObject("ArmorPF");
                    break;
                case 9:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Blue Blob(s) and a "
                        + round + " power Charged Shot item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    ChargedShots charged = (ChargedShots)createObject("ChargedShotPF");
                    break;
                case 10:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Slow Turner(s) and a "
                        + round + " power Homing Mines item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    HomingMines mines = (HomingMines)createObject("HomingMinesPF");
                    break;
                case 11:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " Rammer(s) and a "
                        + round + " power Accelerant item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        Rammer current = (Rammer)createObject("RammerPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    Accelerant accelerant = (Accelerant)createObject("AccelerantFP");
                    break;
                case 12:
                    IngameInterface.displayMessage("Wave " + (type * round + type) + ", " + round + " LazerShooter(s) and a "
                        + round + " power Shield item.", WAVE_MESSAGE_TIME);
                    for (int i = 0; i < round; i++)
                    {
                        LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomGameEdge(), getRandomAngle());
                        current.velocity = getRandomVelocity((int)current.maxSpeed);
                    }
                    Shield shield = (Shield)createObject("ShieldPF");
                    break;
                default:
                    if (type > 12)
                        round++;
                    type = 0;
                    waveTimer = 0;
                    Debug.Log("Veraible 'type' has an invalid value of: " + type);
                    break;
            }
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
