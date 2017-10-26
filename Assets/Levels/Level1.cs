using UnityEngine;
using System.Collections;
using System;
using static User;
using static CRUD;
using static GameStates;


//We want to make a "Pre-Loading" scene that loads all the one-time thingslike User and User Game Data and then we load the actual first scene.

//We should transition to a game-state model where there is a single game-state loop that controls what occurs when the game enters certain states...

/* In simple terms it would look something like this:
 * public void gameStateLoop(or Start on the scene we want it in) {
 * 
 * while(true) {
 *      
 *      while(gamestate = Gamestate.notLoggedIn) {
 *      
 *      }
 *      
 *      while(gamestate = Gamestate.loggedIn) {
 *      
 *          // do stuff that happens while the user is logged in
 *      }
 *      
 *      while(gamestate = Gamestate.playing) {
 *      
 *          //do stuff that happens while the game is being played---load levels, etc
 *      }
 *      
 *      while(gamestate = Gamestate.paused) {
 *          //do stuff that happens while the game is paused
 *      }
 *      
 *      while(gamestate - Gamestate.gameOver) {
 *          //do stuff that happens when the player dies
 *      }
 * 
 * }
 * 
 * 
 * 
 * 
 */

public class Level1 : Level
{
    public override int levelNumber
    {
        get
        {
            return 1;
        }
    }

    //private void Start()
    // {
    //gameState = GameState.NotLoggedIn;
    //crud.GetUser.queryUser("JimSmith", "smith11"); //this would be getting the data from the username and password textboxes and run when the user hits the login button
    //}

    protected override void initilizeLevel()
    {
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
}
