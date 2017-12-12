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

public class Level7 : Level
{
    public override int levelNumber
    {
        get
        {
            return 7;
        }
    }

    public override string levelName
    {
        get
        {
            return "Seven";
        }
    }

    protected override void createLevel()
    {
        musicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(100, 75); //set the level size
        
        createObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 2; i++)
        {
            RedBlob current = (RedBlob)createObject("RedBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }
        for (int i = 0; i < 2; i++)
        {
            GreenBlob current = (GreenBlob)createObject("GreenBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }
        for (int i = 0; i < 2; i++)
        {
            BlueBlob current = (BlueBlob)createObject("BlueBlobPF", getRandomPosition(), getRandomAngle());
            current.velocity = getRandomVelocity(current.maxSpeed);
        }

        Accelerant accelerant = (Accelerant)createObject("AccelerantFP");
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
