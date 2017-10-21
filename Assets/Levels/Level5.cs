using UnityEngine;
using System.Collections;
using System;
using static User;
using static getUser;
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

    protected override void initilizeLevel()
    {
        for (int i = 0; i < 5; i++)
        {
             HomingMine current = (HomingMine)createObject("HomingMinePF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
    }

    protected override void updateLevel()
    {
        
    }
}
