using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using static User;

public abstract class Level : MonoBehaviour
{
    public const float PRECISION = 1024;
    public const string SAVE_PATH = "saves/";
    public const string AUTO_SAVE_EXTENTION = ".NEBULA";
    public const string SAVE_EXTENTION = ".nebula";
    public const string REPLAY_EXTENTION = ".replay";
    public const int TRIAL_LEVELS = 5;

    private System.IO.StreamReader updateFile;

    public abstract int levelNumber { get; }
    public abstract string levelName { get; }

    private Rect theGameBounds = new Rect(Vector2.zero, new Vector2(80, 60));
    public Rect gameBounds
    {
        get
        {
            return theGameBounds;
        }
    }
    protected Vector2 levelSize
    {
        get
        {
            return theGameBounds.size;
        }
        set
        {
            theGameBounds.size = value;
        }
    }

    private static Level theCurrentLevel;
    public static Level currentLevel
    {
        get
        {
            return theCurrentLevel;
        }
    }

    protected SpriteRenderer background;
    public Vector2 backgroundPosition
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = new Vector3(value.x, value.y, 10);
        }
    }

    private float theDuration = 0;
    public TimeSpan duration
    {
        get
        {
            return new TimeSpan(0, 0, (int)theDuration);
        }
    }

    public float updatesPerSec
    {
        get
        {
            return 1.0f / Time.fixedDeltaTime;
        }
    }

    public float secsPerUpdate
    {
        get
        {
            return Time.fixedDeltaTime;
        }
    }

    private float theDifficulty = 1;
    public float difficulty
    {
        get
        {
            return theDifficulty;
        }
    }

    private bool thePvp = false;
    public bool pvp
    {
        get
        {
            return thePvp;
        }
    }

    private int randomSeed;
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
        return new Vector2(random.Next((int)(gameBounds.width * PRECISION)) / PRECISION,
            random.Next((int)(gameBounds.height * PRECISION)) / PRECISION) + gameBounds.min;
    }

    public Vector2 getRandomGameEdge()
    {
        int edge = random.Next(3);

        switch (edge)
        {
            case 0:
                return new Vector2(gameBounds.xMax, random.Next((int)gameBounds.height) + gameBounds.yMin);
            case 1:
                return new Vector2(gameBounds.xMin, random.Next((int)gameBounds.height) + gameBounds.yMin);
            case 2:
                return new Vector2(random.Next((int)gameBounds.width) + gameBounds.xMin, gameBounds.yMax);
            case 3:
                return new Vector2(random.Next((int)gameBounds.width) + gameBounds.xMin, gameBounds.yMin);
            default:
                throw new Exception("I have no idea how this was called.");
        }

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

    private List<DestructableObject> removeDestructables = new List<DestructableObject>();
    private List<DestructableObject> addDestructables = new List<DestructableObject>();
    private LinkedList<DestructableObject> theDestructables = new LinkedList<DestructableObject>();
	public LinkedList<DestructableObject> destructables
	{
		get 
		{
			return theDestructables;
		}
	}

    private List<IndestructableObject> removeIndestructables = new List<IndestructableObject>();
    private List<IndestructableObject> addIndestructables = new List<IndestructableObject>();
    private LinkedList<IndestructableObject> theIndestructables = new LinkedList<IndestructableObject>();
	public LinkedList<IndestructableObject> indestructables
	{
		get 
		{
			return theIndestructables;
		}
	}

    private List<NonInteractiveObject> removeNonInteractives = new List<NonInteractiveObject>();
    private List<NonInteractiveObject> addNonInteractives = new List<NonInteractiveObject>();
    private LinkedList<NonInteractiveObject> theNonInteractives = new LinkedList<NonInteractiveObject>();
	public LinkedList<NonInteractiveObject> nonInteractives
	{
		get 
		{
			return theNonInteractives;
		}
	}

    public List<IEnumerable<SpaceObject>> getTypes(bool players, bool destructables, bool indestructables, bool nonInteractives)
    {
        List<IEnumerable<SpaceObject>> objects = new List<IEnumerable<SpaceObject>>();

        if (players)
            objects.Add(thePlayers);
        if (destructables)
            objects.Add(theDestructables);
        if (indestructables)
            objects.Add(theIndestructables);
        if (nonInteractives)
            objects.Add(theNonInteractives);

        return objects;
    }

    /*
    public List<IEnumerable<T>> getTypes<T>() where T : SpaceObject
    {
        if (typeof(T) == typeof(SpaceObject))
        {
            return (List<IEnumerable<T>>)getTypes(true, true, true, true);
        }
        else if (typeof(T) == typeof(NonInteractiveObject))
        {
            return (List<IEnumerable<T>>)getTypes(false, false, false, true);
        }
        else if (typeof(T) == typeof(InteractiveObject))
        {
            return (List<IEnumerable<T>>)getTypes(true, true, true, false);
        }
        else if (typeof(T) == typeof(DestructableObject))
        {
            return (List<IEnumerable<T>>)getTypes(true, true, false, false);
        }
        else if (typeof(T) == typeof(IndestructableObject))
        {
            return (List<IEnumerable<T>>)getTypes(false, false, true, false);
        }
        else if (typeof(T) == typeof(Player))
        {
            return (List<IEnumerable<T>>)getTypes(true, false, false, false);
        }
        else if (typeof(T).IsSubclassOf(typeof(NonInteractiveObject))
        {
            List<IEnumerable<T>> items = new List<IEnumerable<T>>();
            foreach (T item in theNonInteractives)
            {
                if (item.GetType().IsSubclassOf(typeof(T)) || item.GetType() == typeof(T))
                {
                    items[0].Add(item);
                }
            }
            return items;
        }
    }
    */

    public void removeFromGame(DestructableObject remove)
    {
        removeDestructables.Add(remove);
    }

    public void removeFromGame(IndestructableObject remove)
    {
        removeIndestructables.Add(remove);
    }

    public void removeFromGame(NonInteractiveObject remove)
    {
        removeNonInteractives.Add(remove);
    }

    public void addToGame(DestructableObject add)
    {
        addDestructables.Add(add);
    }

    public void addToGame(IndestructableObject add)
    {
        addIndestructables.Add(add);
    }

    public void addToGame(NonInteractiveObject add)
    {
        addNonInteractives.Add(add);
    }

    private Player[] initialPlayers = new Player[Controls.MAX_PLAYERS];
    private Player[] thePlayers = new Player[Controls.MAX_PLAYERS];
	public Player[] players
	{
		get 
		{
			return thePlayers;
		}
	}

    protected abstract void createLevel();

    public void create(int numPlayers, float difficulty, int randomSeed, bool pvp)
    {
        if (numPlayers > Controls.MAX_PLAYERS)
        {
            throw new Exception("Too manp players given to Level.create(): " + numPlayers.ToString() + " players given.");
        }

        Controls.get().clearInputs();
        theCurrentLevel = this;

        background = GetComponent<SpriteRenderer>();

        theDifficulty = difficulty;
        thePvp = pvp;
        this.randomSeed = randomSeed;
        theRandom = new System.Random(randomSeed);

        thePlayers = new Player[numPlayers];
        initialPlayers = new Player[numPlayers];

        for (int i = 0; i < numPlayers; i++)
        {
            if (i >= Controls.MAX_PLAYERS)
                break;

            UnityEngine.GameObject obj = Instantiate(Resources.Load("PlayerPF"), 
                new Vector2(gameBounds.center.x - (numPlayers - 1) * 2 + i * 4, gameBounds.center.y) , Quaternion.identity) as GameObject;
            Player current = obj.GetComponent<Player>();

            current.playerNum = i;
            if (pvp)
                current.team = (sbyte)(i + 1);
            else
                current.team = 1;

            thePlayers[i] = current;
            initialPlayers[i] = current.clone();
        }

        foreach (PlayerControls item in Controls.get().players)
        {
            item.clearInputs();
        }

        createLevel();

        backgroundPosition = gameBounds.center;
        transform.localScale = Vector3.one;
        if (gameBounds.width / background.bounds.size.x > gameBounds.height / background.bounds.size.y)
        {
            float scale = gameBounds.width / background.bounds.size.x;
            transform.localScale = new Vector3(scale, scale, 1);

        }
        else
        {
            float scale = gameBounds.height / background.bounds.size.y;
            transform.localScale = new Vector3(scale, scale, 1);
        }

        GameStates.gameState = GameStates.GameState.Playing;
    }

    protected abstract void updateLevel();
    
    public void FixedUpdate()
    {
        if (currentLevel != this)
        {
            clearLevel();
            Destroy(this.gameObject);
            return;
        }

        Options.updateOptions();

        theDuration += UnityEngine.Time.fixedDeltaTime;

        if (GameStates.gameState == GameStates.GameState.Replay && updateFile != null)
        {
            Controls.get().updateFromFile(updateFile);
        }
        else
        {
            Controls.get().updateFromInput();
        }

        int playersRemaining = 0;

        foreach (Player item in players)
        {
            if (item != null && item.active)
            {
                playersRemaining++;
            }
        }

        if (won() && (!pvp || pvp && playersRemaining == 1) && duration.TotalSeconds > 2 && theCurrentLevel == this)
        {
            if (GameStates.gameState == GameStates.GameState.Replay)
            {
                GameStates.gameState = GameStates.GameState.LoadReplay;
            }
            else if (User.user.isTrial && levelNumber >= TRIAL_LEVELS || !levelExists(levelNumber + 1))
            {
                GameStates.gameState = GameStates.GameState.WonGame;

                if (!User.user.isTrial)
                    saveToLeaderboard();
            }
            else
            {
                save();
                GameStates.gameState = GameStates.GameState.LevelComplete;

                if (!User.user.isTrial)
                    saveToLeaderboard();
            }
        }

        if (lost() || playersRemaining == 0)
        {
            if (GameStates.gameState == GameStates.GameState.Replay)
            {
                GameStates.gameState = GameStates.GameState.LoadReplay;
            }
            else
            {
                GameStates.gameState = GameStates.GameState.LostGame;
            }
        }
        
        if (!Controls.get().staticLevel && playersRemaining > 0)
        {
            float xUpperLimit = theGameBounds.xMin;
            float xLowerLimit = theGameBounds.xMax;
            float yUpperLimit = theGameBounds.yMin;
            float yLowerLimit = theGameBounds.yMax;

            foreach (Player item in players)
            {
                if (item != null && item.active)
                {
                    if (item.position.x > xUpperLimit)
                        xUpperLimit = item.position.x;
                    if (item.position.x < xLowerLimit)
                        xLowerLimit = item.position.x;
                    if (item.position.y > yUpperLimit)
                        yUpperLimit = item.position.y;
                    if (item.position.y < yLowerLimit)
                        yLowerLimit = item.position.y;
                }
            }
            theGameBounds.center = new Vector2((xUpperLimit - xLowerLimit) / 2.0f + xLowerLimit, (yUpperLimit - yLowerLimit) / 2.0f + yLowerLimit);
        }

        updateObjectLists();
    }

    private void saveToLeaderboard()
    {
        int score = 0;

        score = -(int)duration.TotalMilliseconds;

        CRUD.crud.SaveUserData(score, levelNumber, (int)duration.TotalMilliseconds, players.Length, (float)Math.Round(difficulty, 2), pvp);
    }

    public virtual string progress
    {
        get
        {
            int remaining = 0;

            foreach (DestructableObject item in destructables)
            {
                if (item.team <= 0)
                {
                    remaining++;
                }
            }

            return remaining.ToString() + " enemies remaining.";
        }
    }

    protected virtual bool won()
    {
        foreach (DestructableObject item in destructables)
        {
            if (item.team <= 0)
            {
                return false;
            }
        }

        return true;
    }

    protected virtual bool lost()
    {
        return false;
    }

    private void updateObjectLists()
    {
        foreach (DestructableObject item in addDestructables)
        {
            theDestructables.AddLast(item);
        }
        addDestructables.Clear();

        foreach (IndestructableObject item in addIndestructables)
        {
            theIndestructables.AddLast(item);
        }
        addIndestructables.Clear();

        foreach (NonInteractiveObject item in addNonInteractives)
        {
            theNonInteractives.AddLast(item);
        }
        addNonInteractives.Clear();

        foreach (DestructableObject item in removeDestructables)
        {
            theDestructables.Remove(item);
        }
        removeDestructables.Clear();

        foreach (IndestructableObject item in removeIndestructables)
        {
            theIndestructables.Remove(item);
        }
        removeIndestructables.Clear();

        foreach (NonInteractiveObject item in removeNonInteractives)
        {
            theNonInteractives.Remove(item);
        }
        removeNonInteractives.Clear();
    }

    private void OnDestroy()
    {
        clearLevel();

        if (theCurrentLevel == this)
        {
            theCurrentLevel = null;
        }
    }

    private void clearLevel()
    {
        updateObjectLists();

        foreach (Player item in thePlayers)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }
        thePlayers = new Player[Controls.MAX_PLAYERS];

        foreach (DestructableObject item in theDestructables)
        {
            Destroy(item.gameObject);
        }
        theDestructables.Clear();

        foreach (IndestructableObject item in theIndestructables)
        {
            Destroy(item.gameObject);
        }
        theIndestructables.Clear();

        foreach (NonInteractiveObject item in theNonInteractives)
        {
            Destroy(item.gameObject);
        }
        theNonInteractives.Clear();
    }

    private void saveItems(System.IO.StreamWriter save)
    {
        saveItems(save, thePlayers);
    }

    private void saveItems(System.IO.StreamWriter save, Player[] players)
    {
        foreach (Player player in players)
        {
            foreach (Item item in player.items)
            {
                if (item == null)
                {
                    save.WriteLine("");
                    save.WriteLine("");
                }
                else
                {
                    string name = item.ToString();

                    name = name.Substring(0, name.IndexOf('('));
                    save.WriteLine(name);
                    save.WriteLine(item.getValues());
                }
            }
        }
    }

    private void loadItems(System.IO.StreamReader load)
    {
        foreach (Player player in players)
        {
            for (int i = 0; i < player.items.Length; i++)
            {
                string name = load.ReadLine();
                string values = load.ReadLine();

                if (name == "")
                {
                    player.items[i] = null;
                }
                else
                {
                    Item item = (Item)createObject(name, Vector2.zero, 0);
                    item.loadValues(values);
                    item.pickup(player, i);
                }
            }
        }
    }

    public Level restartLevel()
    {
        Level lvl = getLevel(levelNumber);

        if (lvl != null)
        {
            lvl.create(thePlayers.Length, theDifficulty, randomSeed, thePvp);

            for (int i = 0; i < thePlayers.Length; i++)
            {
                for (int j = 0; j < thePlayers[i].items.Length; j++)
                {
                    if (initialPlayers[i].items[j] != null)
                    {
                        initialPlayers[i].items[j].pickup(lvl.thePlayers[i], j);
                        lvl.theNonInteractives.AddLast(lvl.thePlayers[i].items[j]);
                        theNonInteractives.Remove(lvl.thePlayers[i].items[j]);
                    }
                }
                lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
            }

            clearLevel();
            Destroy(this.gameObject);
        }

        return lvl;
    }
    
    /// <summary>
    /// Makes an autosave for the next Level
    /// Creates the next Level and destroys the current one
    /// Should only be called when the current Level is completed
    /// </summary>
    /// <returns>The next Level</returns>
    public Level nextLevel()
    {
        Level lvl = getLevel(levelNumber + 1);

        if (lvl != null)
        {
            lvl.create(thePlayers.Length, theDifficulty, theRandom.Next(), thePvp);

            for (int i = 0; i < thePlayers.Length; i++)
            {
                for (int j = 0; j < thePlayers[i].items.Length; j++)
                {
                    if (thePlayers[i].items[j] != null)
                    {
                        thePlayers[i].items[j].pickup(lvl.thePlayers[i], j);
                        lvl.theNonInteractives.AddLast(lvl.thePlayers[i].items[j]);
                        theNonInteractives.Remove(lvl.thePlayers[i].items[j]);
                    }
                }
                lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
            }

            clearLevel();
            Destroy(this.gameObject);
        }

        return lvl;
    }

    private bool save()
    {
        System.IO.Directory.CreateDirectory(SAVE_PATH);
        string[] filePaths = System.IO.Directory.GetFiles(SAVE_PATH, "*" + AUTO_SAVE_EXTENTION);

        Array.Sort(filePaths);

        for (int i = 0; i < filePaths.Length - Options.maxAutosaves - 1; i++)
        {
            System.IO.File.Delete(filePaths[i]);
        }

        return save(DateTime.Now.ToLocalTime().ToString("yy-MM-dd-HH-mm-ss"), true);
    }

    private bool save(string fileName, bool autoSave)
    {
        System.IO.Directory.CreateDirectory(SAVE_PATH);
        System.IO.StreamWriter file;

        if (autoSave)
        {
            file = new System.IO.StreamWriter(SAVE_PATH + fileName + AUTO_SAVE_EXTENTION, false);
        }
        else
        {
            file = new System.IO.StreamWriter(SAVE_PATH + fileName + SAVE_EXTENTION, false);
        }

        file.WriteLine(Convert.ToString(levelNumber + 1));
        file.WriteLine(Convert.ToString(thePlayers.Length));
        file.WriteLine(Convert.ToString(theDifficulty));
        file.WriteLine(Convert.ToString(thePvp));
        file.WriteLine(Convert.ToString(random.Next()));

        saveItems(file);

        file.Close();

        return true;
    }

    /// <summary>
    /// Makes a save for the next Level with the given filename
    /// Should only be called when the current level is completed
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>True if successful</returns>
    public bool save(string fileName)
    {
        return save(fileName, false);
    }

    public bool saveReplay(string fileName)
    {
        System.IO.Directory.CreateDirectory(SAVE_PATH);
        System.IO.StreamWriter save = new System.IO.StreamWriter(SAVE_PATH + fileName + REPLAY_EXTENTION, false);

        save.WriteLine(Convert.ToString(levelNumber));
        save.WriteLine(Convert.ToString(thePlayers.Length));
        save.WriteLine(Convert.ToString(theDifficulty));
        save.WriteLine(Convert.ToString(thePvp));
        save.WriteLine(Convert.ToString(randomSeed));

        saveItems(save, initialPlayers);

        Controls.get().saveInputsToFile(save);

        save.Close();

        return true;
    }

    public static bool levelExists(int levelNum)
    {
        Level lvl = getLevel(levelNum);

        if (lvl == null)
        {
            return false;
        }
        else
        {
            Destroy(lvl.gameObject);
            return true;
        }
    }

    public static Level getLevel(int levelNum)
    {
        UnityEngine.GameObject obj;

        try
        {
            switch (levelNum)
            {
                case 1:
                    obj = Instantiate(Resources.Load("Level1PF")) as GameObject;
                    break;
                case 2:
                    obj = Instantiate(Resources.Load("Level2PF")) as GameObject;
                    break;
                case 3:
                    obj = Instantiate(Resources.Load("Level3PF")) as GameObject;
                    break;
                case 4:
                    obj = Instantiate(Resources.Load("Level4PF")) as GameObject;
                    break;
                case 5:
                    obj = Instantiate(Resources.Load("Level5PF")) as GameObject;
                    break;
                case 6:
                    obj = Instantiate(Resources.Load("Level6PF")) as GameObject;
                    break;
                case 7:
                    obj = Instantiate(Resources.Load("Level7PF")) as GameObject;
                    break;
                case 8:
                    obj = Instantiate(Resources.Load("Level8PF")) as GameObject;
                    break;
                case 9:
                    obj = Instantiate(Resources.Load("Level9PF")) as GameObject;
                    break;
                default:
                    try
                    {
                        obj = Instantiate(Resources.Load("Level" + levelNum.ToString() + "PF")) as GameObject;
                    }
                    catch
                    {
                        return null;
                    }
                    break;
            }

            return obj.GetComponent<Level>();
        }
        catch
        {
            throw new Exception("Error loading level " + levelNum.ToString());
        }
    }

    public static Level loadLevel(string fileName)
    {
        System.IO.StreamReader save = new System.IO.StreamReader(fileName);

        Level lvl = getLevel(Convert.ToInt32(save.ReadLine()));

        if (lvl != null)
        {

            int players = Convert.ToInt32(save.ReadLine());
            int difficulty = Convert.ToInt32(save.ReadLine());
            bool pvp = Convert.ToBoolean(save.ReadLine());
            int randomSeed = Convert.ToInt32(save.ReadLine());

            lvl.create(players, difficulty, randomSeed, pvp);

            lvl.loadItems(save);

            lvl.initialPlayers = new Player[lvl.players.Length];
            for (int i = 0; i < lvl.thePlayers.Length; i++)
            {
                lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
            }

            save.Close();

            GameStates.gameState = GameStates.GameState.Playing;

        }

        return lvl;
    }


    public static Level loadReplay(string fileName)
    {
        System.IO.StreamReader replay = new System.IO.StreamReader(fileName);

        Level lvl = getLevel(Convert.ToInt32(replay.ReadLine()));

        int players = Convert.ToInt32(replay.ReadLine());
        int difficulty = Convert.ToInt32(replay.ReadLine());
        int randomSeed = Convert.ToInt32(replay.ReadLine());
        bool pvp = Convert.ToBoolean(replay.ReadLine());
        lvl.create(players, difficulty, randomSeed, pvp);

        lvl.loadItems(replay);

        lvl.initialPlayers = new Player[lvl.players.Length];
        for (int i = 0; i < lvl.thePlayers.Length; i++)
        {
            lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
        }
        
        lvl.updateFile = replay;

        GameStates.gameState = GameStates.GameState.Replay;

        replay.Close();

        return lvl;
    }
}
