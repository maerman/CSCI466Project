// written by: Thomas Stewart, Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart, Shane Barry, Metin Erman

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using static User;

/// <summary>
/// Level is a MonoBehavior that is used to control and keep track of a level's settings 
/// and SpaceObjects in the level. It has methods to do things like save, load and restart the 
/// Level. Derived classes are used to describe a particular Level, like what SpaceObjects it 
/// contains and what the Level's win and loss conditions are. 
/// Works similar to a singleton in that the static variable 'current' holds the current Level
/// and when create() is called on a Level, 'current' is set to that Level. 
/// </summary>
public abstract class Level : MonoBehaviour
{
    public const float PRECISION = 1024;
    public const string LEVEL_PATH = "levels/";
    public const string SAVE_PATH = "saves/";
    public const string AUTO_SAVE_EXTENTION = ".NEBULA";
    public const string SAVE_EXTENTION = ".nebula";
    public const string REPLAY_EXTENTION = ".replay";
    public const int TRIAL_LEVELS = 5;

    //Used if the Level is a replay
    private System.IO.StreamReader updateFile;

    //To be implimented in derived classes to give information about the Level
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
    /// <summary>
    /// Allows derived classes to change the size of the Level.
    /// </summary>
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

    //The Level that is currently being played. Is changed when create() is called.
    private static Level theCurrentLevel;
    public static Level current
    {
        get
        {
            return theCurrentLevel;
        }
    }

    //CameraController moves the background to always be centered on the camera.
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

    //Amount of time the this Level has been active.
    private float theDuration = 0;
    public TimeSpan duration
    {
        get
        {
            return new TimeSpan(0, 0, (int)theDuration);
        }
    }

    //The number of times FixedUpdate() is called by Unity each second. 
    //All update methods in Levels and SpaceObjects are called from a FixedUpdate().
    public float updatesPerSec
    {
        get
        {
            return 1.0f / Time.fixedDeltaTime;
        }
    }

    //The part of a second between each time Unity calls FixedUpdate()
    public float secsPerUpdate
    {
        get
        {
            return Time.fixedDeltaTime;
        }
    }

    //The difficulty of the Level. SpaceObjects may use this to adjust part of their 
    //behavior depending on the difficulty. 
    private float theDifficulty = 1;
    public float difficulty
    {
        get
        {
            return theDifficulty;
        }
    }

    //If Players are competing with eachother and on the same team or not and 
    //if they can damage eachother. 
    private bool thePvp = false;
    public bool pvp
    {
        get
        {
            return thePvp;
        }
    }

    //The following methods give simple ways to get random values of different types.
    //All Levels and SpaceObjects should use these to get random numbers, so replays sync up. 
    #region random number helper methods
    private int randomSeed;
    private System.Random theRandom;
    public System.Random random
    {
        get
        {
            return theRandom;
        }
    }

    /// <summary>
    /// Get a random angle in degrees between 0 and 360.
    /// </summary>
    /// <returns>A random angle in degrees.</returns>
    public float getRandomAngle()
    {
        return random.Next((int)(360 * PRECISION)) / PRECISION;
    }

    /// <summary>
    /// Get a Vector2 with random values between 0 and the given number.
    /// </summary>
    /// <param name="maxValues">Max value of the Vector2's parts.</param>
    /// <returns>A Vector2 with random values.</returns>
    public Vector2 getRandomVector2(float maxValues)
    {
        return new Vector2(random.Next((int)(maxValues * PRECISION)) / PRECISION,
            random.Next((int)(maxValues * PRECISION)) / PRECISION);
    }

    /// <summary>
    /// Get a Vector2 with random values between the given numbers.
    /// </summary>
    /// <param name="maxValues">Max value of the Vector2's parts.</param>
    /// <param name="minValues">Min value of the Vector2's parts.</param>
    /// <returns>A Vector2 with random values.</returns>
    public Vector2 getRandomVector2(float minValues, float maxValues)
    {
        return new Vector2(random.Next((int)(minValues * PRECISION), (int)(maxValues * PRECISION)) / PRECISION,
           random.Next((int)(minValues * PRECISION), (int)(maxValues * PRECISION)) / PRECISION);
    }

    /// <summary>
    /// Get a Vector2 representing a random position in this Level.
    /// </summary>
    /// <returns>A random position in this Level.</returns>
    public Vector2 getRandomPosition()
    {
        return new Vector2(random.Next((int)(gameBounds.width * PRECISION)) / PRECISION,
            random.Next((int)(gameBounds.height * PRECISION)) / PRECISION) + gameBounds.min;
    }

    /// <summary>
    /// Get a Vector2 representing a random positoin on the edge of this Level. 
    /// </summary>
    /// <returns></returns>
    public Vector2 getRandomGameEdge()
    {
        int edge = random.Next(3);

        //Determine which edge to use then find a random position on that edge. 
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

    /// <summary>
    /// Get a Vector2 pointing in a random angle with a random magnitude between 0 and the given value. 
    /// </summary>
    /// <param name="maxSpeed">Maximum magnitude of velocity</param>
    /// <returns>A random velocity.</returns>
    public Vector2 getRandomVelocity(int maxSpeed)
    {
        float angle = getRandomAngle() * Mathf.Deg2Rad;
        float speed = random.Next(maxSpeed);

        return new Vector2(random.Next((int)(speed * PRECISION)) / PRECISION * Mathf.Cos(angle * Mathf.Deg2Rad),
            random.Next((int)(speed * PRECISION)) / PRECISION) * Mathf.Sin(angle * Mathf.Deg2Rad);
    }

    /// <summary>
    /// Get a Vector2 pointing in a random angle with the given magnitude. 
    /// </summary>
    /// <param name="speed">Magnitude of velocity</param>
    /// <returns>A velocity in a random direction.</returns>
    public Vector2 getVelocityInRandomDirection(float speed)
    {
        float angle = getRandomAngle() * Mathf.Deg2Rad;

        return new Vector2(random.Next((int)(speed * PRECISION)) / PRECISION * Mathf.Cos(angle * Mathf.Deg2Rad),
            random.Next((int)(speed * PRECISION)) / PRECISION) * Mathf.Sin(angle * Mathf.Deg2Rad);
    }
    #endregion

    //The following methods give simple ways to create a GameObject and get that 
    //GameObject's SpaceObject script.
    #region createObject helper methods
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
    #endregion

    //The following methods and variables are used to keep track of the SpaceObjects in this Level
    #region SpaceObjects in this Level
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

    /// <summary>
    /// A simple way to get a list of all wanted categories of SpaceObjects in this Level.
    /// </summary>
    /// <param name="players">Should the list contain Players?</param>
    /// <param name="destructables">Should the list contain non-Player DestructableObjects?</param>
    /// <param name="indestructables">Should the list contain IndestructableObjects?</param>
    /// <param name="nonInteractives">Should the list contain NonInteractiveObjects?</param>
    /// <returns>A list of lists containing all the selected types of SpaceObjects</returns>
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

    /* Doesn't work
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

    //The following methods remove or add the given DestructableObject to the correct list.
    public void removeFromGame(DestructableObject remove)
    {
        if (remove.GetType() == typeof(Player))
            remove.destroyThis();
        else
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
    #endregion

    //The following methods, accesors and variables provide ways to play music and audio effects in this Level
    #region audio
    private float theMusicVolume = 1;
    private AudioSource theMusicAudio;
    protected AudioSource musicAudio
    {
        get
        {
            return theMusicAudio;
        }
    }
    protected void musicPlay(AudioClip clip)
    {
        theMusicAudio.clip = clip;
        theMusicAudio.loop = true;
        theMusicAudio.Play();
    }
    protected void musicPlay(string clipName)
    {
        AudioClip clip = Resources.Load(clipName) as AudioClip;

        if (clip != null)
            musicPlay(clip);
        else
            Debug.Log("Couldn't load audioclip: " + clipName);
    }
    protected float musicVolume
    {
        get
        {
            return theMusicVolume;
        }
        set
        {
            theMusicVolume = Mathf.Clamp01(value);
        }
    }

    private float theEffectVolume = 1;
    private AudioSource theEffectAudio;
    public AudioSource effectAudio
    {
        get
        {
            return theEffectAudio;
        }
    }
    public void effectPlay(AudioClip clip)
    {
        theEffectAudio.clip = clip;
        theEffectAudio.Play();
    }
    protected void effectPlay(string clipName)
    {
        AudioClip clip = Resources.Load(clipName) as AudioClip;

        if (clip != null)
            effectPlay(clip);
        else
            Debug.Log("Couldn't load audioclip: " + clipName);
    }
    public float effectVolume
    {
        get
        {
            return theEffectVolume;
        }
        set
        {
            theEffectVolume = Mathf.Clamp01(value);
        }
    }

    #endregion

    /// <summary>
    /// Abstract method for derived Levels to impliment to do things when the Level is created, 
    /// like adding enemies to the this Level. Called by create() method below.
    /// </summary>
    protected abstract void createLevel();

    /// <summary>
    /// Method called to create this Level with the given settings. Sets Level.current to this Level.
    /// </summary>
    /// <param name="numPlayers">Number of Players in this Level.</param>
    /// <param name="difficulty">Difficulty of this Level.</param>
    /// <param name="randomSeed">A number to start the RandomNumbers, used so that replay can sync up.</param>
    /// <param name="pvp">If Players are competing in this Level or not.</param>
    public void create(int numPlayers, float difficulty, int randomSeed, bool pvp)
    {
        if (numPlayers > Controls.MAX_PLAYERS)
        {
            throw new Exception("Too manp players given to Level.create(): " + numPlayers.ToString() + " players given.");
        }

        Controls.get().clearInputs();
        theCurrentLevel = this;
        
        background = GetComponent<SpriteRenderer>();

        //save the given settings
        theDifficulty = difficulty;
        thePvp = pvp;
        this.randomSeed = randomSeed;
        theRandom = new System.Random(randomSeed);

        //create the Players
        thePlayers = new Player[numPlayers];
        initialPlayers = new Player[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            if (i >= Controls.MAX_PLAYERS)
                break;

            UnityEngine.GameObject obj = Instantiate(Resources.Load("PlayerPF"), 
                new Vector2(gameBounds.center.x - (numPlayers - 1) * 2 + i * 4, gameBounds.center.y) , Quaternion.identity) as GameObject;
            Player current = obj.GetComponent<Player>();

            current.playerNum = (byte)i;
            if (pvp)
                current.team = (sbyte)(i + 1);
            else
                current.team = 1;

            thePlayers[i] = current;

            //save the initial Player so that it can be saved in if a replay is made
            initialPlayers[i] = current.clone();
        }

        //reset Player inputs
        foreach (PlayerControls item in Controls.get().players)
        {
            item.clearInputs();
        }

        theMusicAudio = gameObject.AddComponent<AudioSource>();
        theEffectAudio = gameObject.AddComponent<AudioSource>();
        
        //Let derived class do things at Level creation
        createLevel();

        //set the scale of the background so that it covers this entire Level
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
    }

    /// <summary>
    /// Abstract method for derived Levels to impliment to do things every update. 
    /// Called by the FixedUpdate() method below.
    /// </summary>
    protected abstract void updateLevel();
    
    /// <summary>
    /// Method calld by Unity 50 times a second. This method is used to keep track of the 
    /// state of this Level, like check if it has been completed or lost, update the input, ect. 
    /// </summary>
    public void FixedUpdate()
    {
        //If this Level is being update but it is not the current Level, it should not exist
        //so destroy it.
        if (current != this)
        {
            clearLevel();
            Destroy(this.gameObject);
            return;
        }

        theDuration += UnityEngine.Time.fixedDeltaTime;

        //Let derived class do things at Level update
        updateLevel();

        //set the volumes depending on the user controlled volume and code controlled volume.
        theMusicAudio.volume = Options.get().volumeMusic * musicVolume;
        theEffectAudio.volume = Options.get().volumeMusic * effectVolume;

        if (GameStates.gameState == GameStates.GameState.Replay)
        {
            if (updateFile == null)
            {
                throw new Exception("updateFile set to null when updating in Replay mode!");
            }
            //if there is more to in the updateFile, then update using it
            else if (updateFile.Peek() >= 0)
            {
                Controls.get().updateFromFile(updateFile);
            }
            else
            {
                Debug.Log("Reached end of input file; Level should have ended but it has not.");
                GameStates.gameState = GameStates.GameState.LoadReplay;
                return;
            }
        }
        else
        {
            Controls.get().updateFromInput();
        }

        int playersRemaining = 0;

        //find the number of Players still alive
        foreach (Player item in players)
        {
            if (item != null && item.active)
            {
                playersRemaining++;
            }
        }

        //The Level is won if the won() method returns true, there is only 1 Player remaining if pvp 
        //and 2 seconds has past
        if (won() && (!pvp || pvp && playersRemaining == 1) && duration.TotalSeconds > 2)
        {
            if (GameStates.gameState == GameStates.GameState.Replay)
            {
                GameStates.gameState = GameStates.GameState.LoadReplay;
            }
            //if the user is on a trail account and this is the last trial Level 
            //or if this is the last Level, then go to the WonGame screen
            else if (User.user.isTrial && levelNumber >= TRIAL_LEVELS || !levelExists(levelNumber + 1))
            {
                GameStates.gameState = GameStates.GameState.WonGame;

                if (!User.user.isTrial)
                    saveToLeaderboard();
            }
            //if this is not the last Level, then autosave and go to the LevelComplete screen
            else
            {
                save();
                GameStates.gameState = GameStates.GameState.LevelComplete;

                if (!User.user.isTrial)
                    saveToLeaderboard();
            }
        }

        //The Level is lost if lost() returns true or there are no Players remaining
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
        
        //If the static Level setting is not turned on, then set the center of this Level
        //to the center of the Player's positions
        if (!Options.get().levelStatic && playersRemaining > 0)
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

    /// <summary>
    /// Calculate the score and save it and this Level's settings to the leaderboard.
    /// </summary>
    private void saveToLeaderboard()
    {
        int score = 0;

        score = -(int)duration.TotalMilliseconds;

        CRUD.crud.SaveUserData(score, levelNumber, (int)duration.TotalMilliseconds, players.Length, (float)Math.Round(difficulty, 2), pvp);
    }

    /// <summary>
    /// An accessor that derived classes can overwrite the returns a string describing the progress
    /// though this Level. By default, tells the number of DestructableObject enemies remaining in this Level.
    /// </summary>
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

    /// <summary>
    /// A method that derived classes can overrite that determines if this Level's 
    /// objectives have been completed yet. By default, returns true if no more enemy 
    /// DestructableObjects remain in this Level.
    /// </summary>
    /// <returns>If the Level has been won or not.</returns>
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

    /// <summary>
    /// A method that dreived classes can overrite that determines if this Level's
    /// objectives have been lost yet. By default, always returns false. 
    /// </summary>
    /// <returns>If this Level has been lost or not yet.</returns>
    protected virtual bool lost()
    {
        return false;
    }

    /// <summary>
    /// Updates the lists of SpaceObjects in this Level, adding and removing any 
    /// that have been set to be added or removed. 
    /// </summary>
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

    /// <summary>
    /// Abstract method for derived Levels to impliment to do things right before this Level is destroyed. 
    /// Called by the OnDestroy() method below.
    /// </summary>
    protected abstract void endLevel();

    /// <summary>
    /// Called by Unity right before this Level is destoryed. Destroys all SpaceObjects in 
    /// this Level, closes resources being used and make sure this is not set as Level.current. 
    /// </summary>
    private void OnDestroy()
    {
        endLevel();

        clearLevel();

        if (updateFile != null)
        {
            updateFile.Close();
        }

        if (theCurrentLevel == this)
        {
            theCurrentLevel = null;
        }
    }

    
    /// Destroys all SpaceObjects in this Level
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

    /// <summary>
    /// Saves the Players' current Items to the given file. 
    /// </summary>
    /// <param name="save">File to save Items to.</param>
    private void saveItems(System.IO.StreamWriter save)
    {
        saveItems(save, thePlayers);
    }

    /// <summary>
    /// Saves the Items in the given Players' Item lists to the given file. 
    /// </summary>
    /// <param name="save">File to save Items to.</param>
    /// <param name="players">The Players holding the Items to be saved.</param>
    private void saveItems(System.IO.StreamWriter save, Player[] players)
    {
        foreach (Player player in players)
        {
            foreach (Item item in player.items)
            {   
                //save a blank Item slot
                if (item == null)
                {
                    save.WriteLine("");
                    save.WriteLine("");
                }
                //save the Item
                else
                {
                    //get and save the Item's Prefab's name.
                    string name = item.ToString();
                    name = name.Substring(0, name.IndexOf('('));
                    save.WriteLine(name);

                    //save additional Item values. 
                    save.WriteLine(item.getValues());
                }
            }
        }
    }

    /// <summary>
    /// Loads Items from the given file into the Players' Item lists. 
    /// </summary>
    /// <param name="load">File to load the Items from.</param>
    private void loadItems(System.IO.StreamReader load)
    {
        foreach (Player player in players)
        {
            for (int i = 0; i < player.items.Length; i++)
            {
                string name = load.ReadLine();
                string values = load.ReadLine();

                //if the Item was a blank Item slot, set the slot to null
                if (name == "")
                {
                    player.items[i] = null;
                }
                //if it was an actual Item, create it and set it to the slot
                else
                {
                    //create the Item
                    Item item = (Item)createObject(name, Vector2.zero, 0);

                    //load any additional values it saved
                    item.loadValues(values);

                    //put the Item into the correct Item slot
                    item.pickup(player, i);
                }
            }
        }
    }

    /// <summary>
    /// Creates a new Level with this Level's starting settings.
    /// Returns null if there was a problem creating the Level.
    /// </summary>
    /// <returns>A Level with this Level's starting settings or null.</returns>
    public Level restartLevel()
    {
        Level lvl;

        //if this is not a replay
        if (updateFile == null)
        {
            //get the same number Level
            lvl = getLevel(levelNumber);

            if (lvl != null)
            {
                //create the Level using the same settings
                lvl.create(thePlayers.Length, theDifficulty, randomSeed, thePvp);

                //copy the Item's the Players had at the begining of the Level
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

                //destroy this Level
                clearLevel();
                Destroy(this.gameObject);
            }
        }
        //if this is a replay
        else
        {
            //set the replay file to the begining and reload the replay
            updateFile.DiscardBufferedData();
            updateFile.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            lvl = loadReplay(updateFile);

            //if the replay was loaded successfuly, then destroy this Level
            if (lvl != null)
            {
                updateFile = null;
                clearLevel();
                Destroy(this.gameObject);
            }
        }

        return lvl;
    }
    
    /// <summary>
    /// Makes an autosave for the next Level
    /// Creates the next Level and if successful, returns it, if not returns null
    /// Should only be called when the current Level is completed
    /// </summary>
    /// <returns>The next Level or null</returns>
    public Level nextLevel()
    {
        //find the next Level
        Level lvl = getLevel(levelNumber + 1);

        //if there is a next Level
        if (lvl != null)
        {
            //create it with the same settings and a new random seed
            lvl.create(thePlayers.Length, theDifficulty, theRandom.Next(), thePvp);

            //copy the Item's the Players had at the end of this Level into the next one
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

            //destroy this Level
            clearLevel();
            Destroy(this.gameObject);
        }

        return lvl;
    }

    /// <summary>
    /// Save information needed to create the next Level, the settings and Player's items, 
    /// to an autosave file. 
    /// </summary>
    /// <returns>True if the save was successful, false otherwise.</returns>
    private bool save()
    {
        System.IO.Directory.CreateDirectory(SAVE_PATH);

        //make sure there aren't too many autosaves, delete the oldest first
        string[] filePaths = System.IO.Directory.GetFiles(SAVE_PATH, "*" + AUTO_SAVE_EXTENTION);
        Array.Sort(filePaths);
        for (int i = 0; i < filePaths.Length - Options.get().levelMaxAutosaves - 1; i++)
        {
            System.IO.File.Delete(filePaths[i]);
        }

        //create the save with the autosave filename
        return save(DateTime.Now.ToLocalTime().ToString("yy-MM-dd-HH-mm-ss"), true);
    }

    /// <summary>
    /// Save information needed to create the next Level, the settings and Player's items, 
    /// to a file with the given name.
    /// </summary>
    /// <param name="fileName">Filename of the file to save to.</param>
    /// <param name="autoSave">Should this use the auto-save extention or the normal one?</param>
    /// <returns>True if the save was successful, false otherwise.</returns>
    private bool save(string fileName, bool autoSave)
    {
        System.IO.StreamWriter file = null;

        try
        {
            System.IO.Directory.CreateDirectory(SAVE_PATH);


            if (autoSave)
            {
                file = new System.IO.StreamWriter(SAVE_PATH + fileName + AUTO_SAVE_EXTENTION, false);
            }
            else
            {
                file = new System.IO.StreamWriter(SAVE_PATH + fileName + SAVE_EXTENTION, false);
            }

            //save the Level's setting to the file
            file.WriteLine(Convert.ToString(levelNumber + 1));
            file.WriteLine(Convert.ToString(thePlayers.Length));
            file.WriteLine(Convert.ToString(theDifficulty));
            file.WriteLine(Convert.ToString(thePvp));
            file.WriteLine(Convert.ToString(random.Next()));

            //save the Players' Items to the file
            saveItems(file);

            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            if (file != null)
                file.Close();
        } 
    }

    /// <summary>
    /// Makes a save for the next Level with the given filename
    /// Should only be called when the current level is completed
    /// </summary>
    /// <param name="fileName">Filename of the file to save to.</param>
    /// <returns>True if successful</returns>
    public bool save(string fileName)
    {
        return save(fileName, false);
    }

    /// <summary>
    /// Save information needed to play a replay of this Level's playthrough, the initial settings and Player's items
    /// and all the user's inputs, to a file with the given name.
    /// </summary>
    /// <param name="fileName">Name of the file to save the replay to.</param>
    /// <returns>True if the save was successful, false otherwise.</returns>
    public bool saveReplay(string fileName)
    {
        System.IO.StreamWriter file = null;

        try
        {
            System.IO.Directory.CreateDirectory(SAVE_PATH);
            file = new System.IO.StreamWriter(SAVE_PATH + fileName + REPLAY_EXTENTION, false);

            //save the Level's setting to the file
            file.WriteLine(Convert.ToString(levelNumber));
            file.WriteLine(Convert.ToString(thePlayers.Length));
            file.WriteLine(Convert.ToString(theDifficulty));
            file.WriteLine(Convert.ToString(thePvp));
            file.WriteLine(Convert.ToString(randomSeed));

            //save the Players' initial Items to the file
            saveItems(file, initialPlayers);

            //save the Player's input to the file
            Controls.get().saveInputsToFile(file);

            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            if (file != null)
                file.Close();
        }        
    }

    /// <summary>
    /// Checks if a Level with the given number exists.
    /// </summary>
    /// <param name="levelNum">Number of the Level.</param>
    /// <returns>True if the Level exists, false otherwise.</returns>
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

    /// <summary>
    /// Returns a Level with the given number if a Level with that number exists, 
    /// returns null otherwise. 
    /// </summary>
    /// <param name="levelNum">Number of the Level.</param>
    /// <returns>A Level of the given number of null.</returns>
    public static Level getLevel(int levelNum)
    {
        UnityEngine.GameObject obj;

        try
        {
            switch (levelNum)
            {
                case 1:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level1PF")) as GameObject;
                    break;
                case 2:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level2PF")) as GameObject;
                    break;
                case 3:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level3PF")) as GameObject;
                    break;
                case 4:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level4PF")) as GameObject;
                    break;
                case 5:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level5PF")) as GameObject;
                    break;
                case 6:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level6PF")) as GameObject;
                    break;
                case 7:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level7PF")) as GameObject;
                    break;
                case 8:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level8PF")) as GameObject;
                    break;
                case 9:
                    obj = Instantiate(Resources.Load(LEVEL_PATH + "Level9PF")) as GameObject;
                    break;
                default:
                    try
                    {
                        obj = Instantiate(Resources.Load(LEVEL_PATH + "Level" + levelNum.ToString() + "PF")) as GameObject;
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

    /// <summary>
    /// Loads a Level from the save file with the given name. 
    /// Returns null if there was a problem creating the Level. 
    /// </summary>
    /// <param name="fileName">Name of the save file.</param>
    /// <returns>A Level loaded from the save file or null.</returns>
    public static Level loadLevel(string fileName)
    {
        System.IO.StreamReader file = null;

        try
        {
            file = new System.IO.StreamReader(fileName);

            //Create a Level with the saved Level number
            Level lvl = getLevel(Convert.ToInt32(file.ReadLine()));

            //if a Level with that number exists, create it
            if (lvl != null)
            {
                //get the Level's setting from the file
                int players = Convert.ToInt32(file.ReadLine());
                int difficulty = Convert.ToInt32(file.ReadLine());
                bool pvp = Convert.ToBoolean(file.ReadLine());
                int randomSeed = Convert.ToInt32(file.ReadLine());

                //create the Level with the settings
                lvl.create(players, difficulty, randomSeed, pvp);

                //load the Players' Items from the file
                lvl.loadItems(file);

                //save the Player's initial settings
                lvl.initialPlayers = new Player[lvl.players.Length];
                for (int i = 0; i < lvl.thePlayers.Length; i++)
                {
                    lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
                }
            }

            return lvl;
        }
        catch
        {
            return null;
        }
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    /// <summary>
    /// Loads a replay Level from the replay file with the given name. 
    /// Returns null if there was a problem creating the replay Level. 
    /// </summary>
    /// <param name="fileName">Name of the replay file.</param>
    /// <returns>A Level replay loaded from the replay file or null.</returns>
    public static Level loadReplay(string fileName)
    {
        try
        {
            return loadReplay(new System.IO.StreamReader(fileName));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Loads a replay Level from the given replay file. 
    /// Returns null if there was a problem creating the replay Level. 
    /// </summary>
    /// <param name="file">The replay file.</param>
    /// <returns>A Level replay loaded from the replay file or null.</returns>
    public static Level loadReplay(System.IO.StreamReader file)
    {
        try
        {
            //Create a Level with the saved Level number
            Level lvl = getLevel(Convert.ToInt32(file.ReadLine()));

            //get the Level's setting from the file
            int players = Convert.ToInt32(file.ReadLine());
            int difficulty = Convert.ToInt32(file.ReadLine());
            bool pvp = Convert.ToBoolean(file.ReadLine());
            int randomSeed = Convert.ToInt32(file.ReadLine());

            //create the Level with the settings
            lvl.create(players, difficulty, randomSeed, pvp);

            //load the Players' Items from the file
            lvl.loadItems(file);

            //save the Player's initial settings
            lvl.initialPlayers = new Player[lvl.players.Length];
            for (int i = 0; i < lvl.thePlayers.Length; i++)
            {
                lvl.initialPlayers[i] = lvl.thePlayers[i].clone();
            }

            //set the Level's replay file to the given file so it can read the inputs from the file as needed.
            lvl.updateFile = file;

            return lvl;
        }
        catch
        {
            return null;
        }
    }
}
