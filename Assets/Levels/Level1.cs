﻿using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;

public class Level1 : Level
{
    public override int levelNumber
    {
        get
        {
            return 1;
        }
    }

    protected override void initilizeLevel()
    {
        //levelSize = new Vector2(80, 60); //set the level size

        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 3; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 1; i++)
        {
            //HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 2; i++)
        {
            //Blob current = (Blob)createObject("BlobPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }

        for (int i = 0; i < 1; i++)
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

        for (int i = 0; i < 1; i++)
        {
            //RandomTurner current = (RandomTurner)createObject("RandomTurnerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 1; i++)
        {
            //MineLayer current = (MineLayer)createObject("MineLayerPF", getRandomPosition(), getRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            //RubberyDebris current = (RubberyDebris)createObject("RubberyDebrisPF", getRandomPosition(), getRandomAngle(), 20);
        }
    }

    protected override void updateLevel()
    {
        
    }

    /*
    protected override bool won()
    {
        //add win conditinos here, default is when all enimes die    

        return false;
    }
    */
    /*
    protected override bool lost()
    {
        //add loss conditions here, if player dies then its always loss

        return false;
    }
    */
}
