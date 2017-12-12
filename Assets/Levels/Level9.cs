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

public class Level9 : Level
{
    public override int levelNumber
    {
        get
        {
            return 9;
        }
    }

    public override string levelName
    {
        get
        {
            return "Nine";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 8; i++)
        {
            HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            SlowTurner current = (SlowTurner)createObject("SlowTurnerPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            LazerShooter current = (LazerShooter)createObject("LazerShooterPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            RotatingLazerSentry current = (RotatingLazerSentry)createObject("RotatingLazerSentryPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            LazerEmitter current = (LazerEmitter)createObject("LazerEmitterPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 4; i++)
        {
            IndestructableDebris current = (IndestructableDebris)createObject("IndestructableDebrisPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        Armor armor = (Armor)createObject("ArmorPF");
    }

    protected override void updateLevel()
    {

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
