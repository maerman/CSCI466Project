using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;

public class PopulateIndestructableDebris : Level
{
    public override int levelNumber
    {
        get
        {
            return 0;
        }
    }

    public override string levelName
    {
        get
        {
            return "Test";
        }
    }

    private void Start()
    {
        gameState = GameState.Playing;
        create(1, 1, (int)System.DateTime.Now.Ticks, false);
    }

    protected override void createLevel()
    {
        createObject("SpaceDustPF", gameBounds.center, 0);

       
        for (int i = 0; i < 1; i++)
        {
			IndestructableDebris current = (IndestructableDebris)createObject("IndestructableDebrisPF", new Vector2(30,35), getRandomAngle(), 0, random.Next(100));
        }
        
    }

    protected override void updateLevel()
    {

    }

    protected override void endLevel()
    {
        
    }

}
