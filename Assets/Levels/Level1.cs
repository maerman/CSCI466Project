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

public class Level1 : Level
{
    public override int levelNumber
    {
        get
        {
            return 1;
        }
    }

    public override string levelName
    {
        get
        {
            return "One";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 6; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            RubberyDebris current = (RubberyDebris)createObject("RubberyDebrisPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }


        LazerSword sword = (LazerSword)createObject("LazerSwordPF");
        MultiShots multiShot = (MultiShots)createObject("MultiShotPF");
        Heal heal = (Heal)createObject("HealPF");
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
