using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using static User;

public abstract class Level : MonoBehaviour
{
    public static Vector2 GAME_SIZE = new Vector2(26.6666f, 20);
    public const int UPDATES_PER_SEC = 60;
    public const float PRECISION = 1024;

    private Canvas canvas;
    private Text timeText;

    private bool isReplay = false;
    private System.IO.StreamReader updateFile;

    private static Level theCurrentLevel;

    public virtual int levelNumber {get;set;}

    public static Level currentLevel
    {
        get
        {
            return theCurrentLevel;
        }
    }

    private DateTime theStartTime;
    public DateTime startTime
    {
        get
        {
            return theStartTime;
        }
    }

    public TimeSpan duration
    {
        get
        {
            return System.DateTime.Now - startTime;
        }
    }

    private int theDifficulty = 1;
    public int difficulty
    {
        get
        {
            return theDifficulty;
        }
    }

    private System.Random theRandom;
    public System.Random random
    {
        get
        {
            return theRandom;
        }
    }

    public float getRandomAngle()
    {
        return random.Next((int)(360 * PRECISION)) / PRECISION;
    }

    public Vector2 getRandomVector2(float maxValues)
    {
        return new Vector2(random.Next((int)(maxValues * PRECISION)) / PRECISION,
            random.Next((int)(maxValues * PRECISION)) / PRECISION);
    }

    public Vector2 getRandomVector2(float minValues, float maxValues)
    {
        return new Vector2(random.Next((int)(minValues * PRECISION), (int)(maxValues * PRECISION)) / PRECISION,
           random.Next((int)(minValues * PRECISION), (int)(maxValues * PRECISION)) / PRECISION);
    }

    public Vector2 getRandomPosition()
    {
        return new Vector2(random.Next((int)(GAME_SIZE.x * PRECISION)) / PRECISION,
            random.Next((int)(GAME_SIZE.y * PRECISION)) / PRECISION);
    }

    public Vector2 getRandomVelocity(float maxSpeed)
    {
        float angle = getRandomAngle() * Mathf.Deg2Rad;

        return new Vector2(random.Next((int)(maxSpeed * PRECISION)) / PRECISION * Mathf.Cos(angle),
            random.Next((int)(maxSpeed * PRECISION)) / PRECISION) * Mathf.Sin(angle);
    }

    public SpaceObject createObject(string namePF)
    {
        return createObject(namePF, getRandomPosition(), getRandomAngle());
    }

    public SpaceObject createObject(string namePF, Vector2 position)
    {
        SpaceObject current = createObject(namePF, position, getRandomAngle());
        return current;
    }

        public SpaceObject createObject(string namePF, Vector2 position, float angle)
    {
        UnityEngine.GameObject obj = Instantiate(Resources.Load(namePF), position, Quaternion.Euler(0, 0, angle)) as GameObject;
        return obj.GetComponent<SpaceObject>();
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, Vector2 velocity)
    {
        SpaceObject current = createObject(namePF, position, angle);
        current.velocity = velocity;
        return current;
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, Vector2 veloctiy, float angularVelocity)
    {
        SpaceObject current = createObject(namePF, position, angle, veloctiy);
        current.angularVelocity = angularVelocity;
        return current;
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, float speedForward)
    {
        SpaceObject current = createObject(namePF, position, angle);
        current.moveForward(speedForward);
        return current;
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, float speedForward, float angularVelocity)
    {
        SpaceObject current = createObject(namePF, position, angle, speedForward);
        current.angularVelocity = angularVelocity;
        return current;
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, Vector2 veloctiy, float angularVelocity, Vector2 scale)
    {
        SpaceObject current = createObject(namePF, position, angle, veloctiy, angularVelocity);
        current.scale = scale;
        return current;
    }

    public SpaceObject createObject(string namePF, Vector2 position, float angle, Vector2 veloctiy, float angularVelocity, float scale)
    {
        SpaceObject current = createObject(namePF, position, angle, veloctiy, angularVelocity);
        current.scale = new Vector2(scale, scale);
        return current;
    }

    protected Text addTextToCanvas(string textToAdd, Vector2 position)
    {
        if (canvas == null)
        {
            canvas = FindObjectOfType<Canvas>();
        }
        GameObject obj = Instantiate(Resources.Load("TextPF"), Vector2.zero, Quaternion.identity) as GameObject;
        //obj.transform.SetParent(canvas.transform);
        Text text = obj.GetComponent<Text>();
        text.text = textToAdd;
        text.transform.position = position;
        return text;
    }


    private LinkedList<DestructableObject> theDestructables = new LinkedList<DestructableObject>();
	public LinkedList<DestructableObject> destructables
	{
		get 
		{
			return theDestructables;
		}
	}

    private LinkedList<IndestructableObject> theIndestructables = new LinkedList<IndestructableObject>();
	public LinkedList<IndestructableObject> indestructables
	{
		get 
		{
			return theIndestructables;
		}
	}

	private LinkedList<NonInteractiveObject> theNonInteractives = new LinkedList<NonInteractiveObject>();
	public LinkedList<NonInteractiveObject> nonInteractives
	{
		get 
		{
			return theNonInteractives;
		}
	}
		
    private List<Player> thePlayers = new List<Player>(Controls.MAX_PLAYERS);
	public List<Player> players
	{
		get 
		{
			return thePlayers;
		}
	}
    
    public void Start()
    {
        initilize(2, 1, System.DateTime.Now.Millisecond * System.DateTime.Now.Minute);

        timeText = addTextToCanvas("time", Vector2.zero);
    }

    protected abstract void initilizeLevel();

    public void initilize(int numPlayers, int difficulty, int randomSeed)
    {
        theCurrentLevel = this;

        theStartTime = DateTime.Now;
        theRandom = new System.Random(randomSeed);

        thePlayers = new List<Player>(numPlayers);

        for (int i = 0; i < numPlayers; i++)
        {
            if (i >= Controls.MAX_PLAYERS)
            {
                break;
            }

            UnityEngine.GameObject obj = Instantiate(Resources.Load("PlayerPF"), 
                new Vector2(GAME_SIZE.x / 2 - (numPlayers - 1) * 2 + i * 4, GAME_SIZE.y / 2) , Quaternion.identity) as GameObject;
            Player current = obj.GetComponent<Player>();
            if(current != null)
            {
                current.playerNum = i;
                players.Add(current);
            }
            else
            {
                Debug.Log("There is a null reference for the current object!!");
            }
        }

        isReplay = false;

        initilizeLevel();
    }

    public bool initilize(System.IO.StreamReader save)
    {
        int players = Convert.ToInt32(save.ReadLine());
        int difficulty = Convert.ToInt32(save.ReadLine());
        int randomSeed = Convert.ToInt32(save.ReadLine());
        initilize(players, difficulty, randomSeed);

        //load player items

        return true;
    }

    public bool initilizeReplay(System.IO.StreamReader replay)
    {
        int players = Convert.ToInt32(replay.ReadLine());
        int difficulty = Convert.ToInt32(replay.ReadLine());
        int randomSeed = Convert.ToInt32(replay.ReadLine());
        initilize(players, difficulty, randomSeed);

        //load player items

        updateFile = replay;
        isReplay = true;
        return true;
    }

    protected abstract void updateLevel();
    
    public void FixedUpdate()
    {
        if (isReplay)
        {
            Controls.get().updateFromFile(updateFile);
        }
        else
        {
            Controls.get().updateFromInput();
        }

        if (destructables.Count <= 0)
        {
            //nextLevel
        }

        //timeText.text = duration.Hours.ToString() + ":" + duration.Minutes.ToString() + ":" + duration.Seconds.ToString();
    }
}
