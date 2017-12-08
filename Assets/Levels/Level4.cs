﻿// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;

public class Level4 : Level
{
    public override int levelNumber
    {
        get
        {
            return 4;
        }
    }

    public override string levelName
    {
        get
        {
            return "Four";
        }
    }

    protected override void createLevel()
    {
        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 4; i++)
        {
             HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
    }

    protected override void updateLevel()
    {
        
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
