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

    protected override void initilizeLevel()
    {
        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 6; i++)
        {
             HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
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
