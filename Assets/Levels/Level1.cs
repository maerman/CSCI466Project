using UnityEngine;
using System.Collections;

public class Level1 : Level
{
    public override void initilizeLevel()
    {
        for (int i = 0; i < 5; i++)
        {
            Asteroid current = (Asteroid)createObject("AsteroidPF", getRandomPosition(), getRandomAngle(), getRandomVelocity(10), random.Next(100));
        }
    }
}
