using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public float maxSpeed = 15f;

    public float maxAngularSpeed = 360f;

    public sbyte team = 0;

    //accessors for derived classes to impliment depending on if they use physics or not
    public abstract Vector2 position { get; set; }
    public abstract Vector2 velocity { get; set; }
    public abstract float angle { get; set; }
    public abstract float angularVelocity { get; set; }
    public abstract float mass { get; set; }
    public abstract Bounds bounds { get; }

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

    public Vector2 scale
    {
        get
        {
            return transform.localScale;
        }

        set
        {
            transform.localScale = new Vector3(value.x, value.y, transform.localScale.z);
        }
    }

    public Vector2 size
    {
        get
        {
            return spriteSize.dot(scale);
        }
        set
        {
            scale = value.div(spriteSize);
        }
    }

    public float drawDepth
    {
        get
        {
            return transform.position.z;
        }

        set
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
    }

    public Sprite sprite
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
        set
        {
            GetComponent<SpriteRenderer>().sprite = value;
        }
    }

    public int sortOrder
    {
        get
        {
            return GetComponent<SpriteRenderer>().sortingOrder;
        }
        set
        {
            GetComponent<SpriteRenderer>().sortingOrder = value;
        }
    }

    public Vector2 spriteSize
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite.bounds.size;
        }
    }

    public Vector2 spritePivot
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite.pivot / GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
    }

    public Color color
    {
        get
        {
            return GetComponent<SpriteRenderer>().color;
        }
        set
        {
            GetComponent<SpriteRenderer>().color = value;
        }
    }

    public float transparancy
    {
        get
        {
            return GetComponent<SpriteRenderer>().color.a;
        }
        set
        {
            Color tempColor = GetComponent<SpriteRenderer>().color;
            tempColor.a = value;
            GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    public SpriteDrawMode drawMode
    {
        get
        {
            return GetComponent<SpriteRenderer>().drawMode;
        }
        set
        {
            GetComponent<SpriteRenderer>().drawMode = value;
        }
    }

    public virtual bool active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }

    /// <summary>
    /// If this object collides with other GameObjects or not. 
    /// Disabling it will make Collision methods not be called and physics collision to not be called.
    /// </summary>
    public bool collides
    {
        get
        {
            return !GetComponent<Collider2D>().enabled;
        }
        set
        {
            GetComponent<Collider2D>().enabled = !value;
        }
    }

    /// <summary>
    /// Modifies the velocity by directly adding the given values to it.
    /// </summary>
    /// <param name="changeX">Change X value of velocity by, right being positive</param>
    /// <param name="changeY">Change Y value of velocity by, up being positive</param>
    public void modifyVelocityAbsolute(float changeX, float changeY)
    {
        modifyVelocityAbsolute(new Vector2(changeX, changeY));
    }

    /// <summary>
    /// Modifies the velocity by directly add the given Vector2 to it
    /// </summary>
    /// <param name="change">Change velocity by, +x being right and +y being up</param>
    public void modifyVelocityAbsolute(Vector2 change)
    {
        GetComponent<Rigidbody2D>().velocity += change;
    }

    /// <summary>
    /// Modifes the velocity by changing it by the given values relative to the direction it is facing.
    /// The given values are made into a Vector2 then rotated by this SpaceObject's angle.
    /// </summary>
    /// <param name="changeX">How much forward and back to modify the velocity, right being positive.</param>
    /// <param name="changeY">How much left and right to modifty the velocity, forward being positive.</param>
    public void modifyVelocityRelative(float changeX, float changeY)
    {
        modifyVelocityRelative(new Vector2(changeX, changeY));

    }

    /// <summary>
    /// Modifes the velocity by changing it by the given Vector2 relative to the direction it is facing.
    /// The given Vector2 is rotated by this SpaceObject's angle.
    /// </summary>
    /// <param name="change">How much to modify the velocity, +x being right and +y being forward.</param>
    public void modifyVelocityRelative(Vector2 change)
    {
        GetComponent<Rigidbody2D>().velocity += change.rotate(angle);
    }

    /// <summary>
    /// The magnitude of this SpaceObject's speed. Changing the value moves it in the 
    /// same direction (or reverse if negative) at the given speed and if the current speed is 0, 
    /// its moved forward by the given value. 
    /// </summary>
    public float speed
    {
        get
        {
            return velocity.magnitude;
        }

        set
        {
            if (speed == 0)
            {
                moveForward(value);
            }
            else if (velocity.x == 0)
            {
                if (velocity.y > 0)
                    velocity = new Vector2(0, value);
                else
                    velocity = new Vector2(0, -value);
            }
            else if (velocity.y == 0)
            {
                if (velocity.x > 0)
                    velocity = new Vector2(value, 0);
                else
                    velocity = new Vector2(-value, 0);
            }
            else
            {
                velocity = new Vector2(velocity.x / speed * value, velocity.y / speed * value);
            }
        }
    }

    /// <summary>
    /// Modifies this SpaceObject's current speed by the given value. If its current speed is 0,
    /// then its moved foward by the given value. 
    /// </summary>
    /// <param name="change"></param>
    public void modifySpeed(float change)
    {
        if (change != 0)
        {
            if (speed == 0)
            {
                moveForward(change);
            }
            else
            {
                speed += change;
            }
        }
    }

    /// <summary>
    /// The distance this SpaceObject is from the givin position, as it is on the screen.
    /// (As opposed to the distance to the position in the game, which might
    /// be through one of the Level's bound's edges.)
    /// </summary>
    /// <param name="from">The given position.</param>
    /// <returns>The distance between this SpaceObject and the given position on the screen.</returns>
    public float distanceFromScreenPosition(Vector2 from)
    {
        return (float)System.Math.Sqrt(distanceFromScreenPositionSquared(from));
    }

    /// <summary>
    /// The distance squared this SpaceObject is from the givin position, as it is on the screen.
    /// (As opposed to the distance squared to the position in the game, which might
    /// be through one of the Level's bound's edges.)
    /// Use this if you are comparing distances, since it is more efficent to calculate since it does not use sqrt. 
    /// </summary>
    /// <param name="from">The given position.</param>
    /// <returns>The distance squared between this SpaceObject and the given position on the screen.</returns>
    public float distanceFromScreenPositionSquared(Vector2 from)
    {
        return (float)(position.y - from.y) * (position.y - from.y) +
            (position.x - from.x) * (position.x - from.x);
    }

    /// <summary>
    /// Finds all 9 mirrors of the given position. These mirrors are where the position would be if 
    /// it was in the Level's bounds that was shifted to each of the 4 sides, each of the 4 diagonals and 
    /// the given position itself in the actual Level's bounds.
    /// These mirrors are used because when a SpaceObject goes past one of the game's edges, it comes
    /// on the opposite side. So, for example, if the distance between two SpaceObjects is to be found,
    /// each of these 9 mirrors need to be checked to find the shortest distance. 
    /// </summary>
    /// <param name="position">The given position to find mirrors of.</param>
    /// <returns>Alll 9 mirrors of the given position.</returns>
    public static Vector2[] mirrorsOfPosition(Vector2 position)
    {
        Vector2[] mirrors = new Vector2[9];

        //actual
        mirrors[0] = position;

        //side mirrors
        mirrors[1] = new Vector2(position.x, position.y - Level.current.gameBounds.height);
        mirrors[2] = new Vector2(position.x, position.y + Level.current.gameBounds.height);
        mirrors[3] = new Vector2(position.x - Level.current.gameBounds.width, position.y);
        mirrors[4] = new Vector2(position.x + Level.current.gameBounds.width, position.y);

        //diagonal mirrors
        mirrors[5] = new Vector2(position.x - Level.current.gameBounds.width, position.y - Level.current.gameBounds.height);
        mirrors[6] = new Vector2(position.x + Level.current.gameBounds.width, position.y - Level.current.gameBounds.height);
        mirrors[7] = new Vector2(position.x - Level.current.gameBounds.width, position.y + Level.current.gameBounds.height);
        mirrors[8] = new Vector2(position.x + Level.current.gameBounds.width, position.y + Level.current.gameBounds.height);

        return mirrors;
    }

    /// <summary>
    /// Finds which of the 9 mirrors of the given position is the closest to this SpaceObject. 
    /// Used to find the actual distance between this SpaceObject and the given position 
    /// since SpaceObject can go over one side of the Level's bounds and come out the other.
    /// </summary>
    /// <param name="position">Postion to find the closest mirror of.</param>
    /// <returns>The closest mirror of the given position.</returns>
    public Vector2 closestMirrorOfPosition(Vector2 position)
    {
        Vector2 closestPosition = position;
        float shortestDistance = float.PositiveInfinity;

        foreach (Vector2 item in mirrorsOfPosition(position))
        {
            float tempDistance = distanceFromScreenPositionSquared(item);
            if (tempDistance < shortestDistance)
            {
                closestPosition = item;
                shortestDistance = tempDistance;
            }
        }

        return closestPosition;
    }

    /// <summary>
    /// Finds the ingame distance this SpaceObject is from the given position.
    /// The ingame distance is the distance from the closest mirror of the given position.
    /// </summary>
    /// <param name="from">Position to find the distance from.</param>
    /// <returns>Returns the ingame distance from the given position.</returns>
    public float distanceFrom(Vector2 from)
    {
        return distanceFromScreenPosition(closestMirrorOfPosition(from));
    }

    /// <summary>
    /// Finds the ingame distance this SpaceObject is from the given SpaceObject.
    /// The ingame distance is the distance from the closest mirror of the given SpaceObject's position.
    /// </summary>
    /// <param name="from">SpaceObject to find the distance from.</param>
    /// <returns>Returns the ingame distance from the given SpaceObject's position.</returns>
    public float distanceFrom(SpaceObject from)
    {
        return distanceFrom(from.position);
    }

    /// <summary>
    /// Finds the ingame distance squared this SpaceObject is from the given position.
    /// The ingame distance squared is the distance squared from the closest mirror of the given position.
    /// Use this if you are comparing distances, since it is more efficent to calculate since it does not use sqrt.
    /// </summary>
    /// <param name="from">Position to find the distance squared from.</param>
    /// <returns>Returns the ingame distance squared from the given position.</returns>
    public float distanceFromSquared(Vector2 from)
    {
        return distanceFromScreenPositionSquared(closestMirrorOfPosition(from));
    }

    /// <summary>
    /// Finds the ingame distance squared this SpaceObject is from the given SpaceObject's position.
    /// The ingame distance squared is the distance squared from the closest mirror of the given SpaceObject's position.
    /// Use this if you are comparing distances, since it is more efficent to calculate since it does not use sqrt.
    /// </summary>
    /// <param name="from">SpaceObject to find the distance squared from.</param>
    /// <returns>Returns the ingame distance squared from the given SpaceObject's position.</returns>
    public float distanceFromSquared(SpaceObject from)
    {
        return distanceFromSquared(from.position);
    }

    /// <summary>
    /// Finds the ingame Vector2 betwen this SpaceObject and the given position.
    /// The ingame Vector2 is the Vector2 difference between the closest mirror of the given position.
    /// </summary>
    /// <param name="from">Position to find the Vector2 from.</param>
    /// <returns>Returns the ingame Vector2 from the given position.</returns>
    public Vector2 vector2From(Vector2 from)
    {
        return closestMirrorOfPosition(from) - position;
    }

    /// <summary>
    /// Finds the ingame Vector2 betwen this SpaceObject and the given SpaceObject's position.
    /// The ingame Vector2 is the Vector2 difference between the closest mirror of the given SpaceObject's position.
    /// </summary>
    /// <param name="from">Position to find the Vector2 from.</param>
    /// <returns>Returns the ingame Vector2 from the given position.</returns>
    public Vector2 vector2From(SpaceObject from)
    {
        return vector2From(from.position);
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given lists.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObject<T>(IEnumerable<IEnumerable<T>> objectLists) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            if (item != null)
            {
                T temp = closestObject<T>(item);

                if (temp != null)
                {
                    float tempDistance = distanceFromSquared(temp);

                    if (tempDistance < closestDistance)
                    {
                        closest = temp;

                    }
                    closestDistance = tempDistance;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given list.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObject<T>(IEnumerable<T> objectList) where T: SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item != null && item.active && item != this)
            {
                float distance = this.distanceFromSquared(item);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = item;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given lists that is 
    /// either on the same team or on a different team depending on the value of sameTeam.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="sameTeam">If ture, only looks for SpaceObjects on the same team. If false, only looks for SpaceObjects on a different team.</param>
    /// <returns>Return the cloest SpaceObject that meets the criteria or null if no possibilites were found.</returns>
    public T closestObject<T>(IEnumerable<IEnumerable<T>> objectLists, bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            if (item != null)
            {
                T temp = closestObject<T>(item, sameTeam);

                if (temp != null)
                {
                    float tempDistance = distanceFromSquared(temp);

                    if (tempDistance < closestDistance)
                    {
                        closest = temp;

                    }
                    closestDistance = tempDistance;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given list that is 
    /// either on the same team or on a different team depending on the value of sameTeam.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="sameTeam">If ture, only looks for SpaceObjects on the same team. If false, only looks for SpaceObjects on a different team.</param>
    /// <returns>Return the cloest SpaceObject that meets the criteria or null if no possibilites were found.</returns>
    public T closestObject<T>(IEnumerable<T> objectList, bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item != null && ((sameTeam && item.team == team) || (!sameTeam && item.team != team)) && item != this && item.active)
            {
                float distance = this.distanceFromSquared(item);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = item;
                }
            }
        }
        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given lists in the
    /// given direction of this SpaceObject
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="direction">Direction of this SpaceObject to look.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObjectInDirection<T>(IEnumerable<IEnumerable<T>> objectLists, float direction) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            if (item != null)
            {
                T temp = closestObjectInDirection<T>(item, direction);

                if (temp != null)
                {
                    float tempDistance = distanceFromSquared(temp);

                    if (tempDistance < closestDistance)
                    {
                        closest = temp;

                    }
                    closestDistance = tempDistance;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given list in the
    /// given direction of this SpaceObject
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="direction">Direction of this SpaceObject to look.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObjectInDirection<T>(IEnumerable<T> objectList, float direction) where T : SpaceObject
    {
        Ray ray = new Ray(position, new Vector2(0, 1).rotate(direction));

        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item != null && item.active && item != this)
            {
                float distance = this.distanceFromSquared(item);

                if (distance < closestDistance && item.bounds.IntersectRay(ray))
                {
                    closestDistance = distance;
                    closest = item;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given lists in the
    /// given direction of this SpaceObject that is 
    /// either on the same team or on a different team depending on the value of sameTeam.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="direction">Direction of this SpaceObject to look.</param>
    /// <param name="sameTeam">If ture, only looks for SpaceObjects on the same team. If false, only looks for SpaceObjects on a different team.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObjectInDirection<T>(IEnumerable<IEnumerable<T>> objectLists, float direction,  bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            if (item != null)
            {
                T temp = closestObjectInDirection<T>(item, direction, sameTeam);

                if (temp != null)
                {
                    float tempDistance = distanceFromSquared(temp);

                    if (tempDistance < closestDistance)
                    {
                        closest = temp;

                    }
                    closestDistance = tempDistance;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Finds the closest SpaceObject to this SpaceObject in the given lists in the
    /// given direction of this SpaceObject that is 
    /// either on the same team or on a different team depending on the value of sameTeam.
    /// Will return null if no possibilis were found.
    /// </summary>
    /// <typeparam name="T">Type of SpaceObject.</typeparam>
    /// <param name="objectLists">SpaceObjects to look for closest in.</param>
    /// <param name="direction">Direction of this SpaceObject to look.</param>
    /// <param name="sameTeam">If ture, only looks for SpaceObjects on the same team. If false, only looks for SpaceObjects on a different team.</param>
    /// <returns>Return the cloest SpaceObject or null if no possibilites were found.</returns>
    public T closestObjectInDirection<T>(IEnumerable<T> objectList, float direction, bool sameTeam) where T : SpaceObject
    {
        Ray ray = new Ray(position, new Vector2(0, 1).rotate(direction));
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item != null && ((sameTeam && item.team == team) || (!sameTeam && item.team != team)) && item != this && item.active)
            {
                float distance = this.distanceFromSquared(item);

                if (distance < closestDistance && item.bounds.IntersectRay(ray))
                {
                    closestDistance = distance;
                    closest = item;
                }
            }
        }
        return closest;
    }

    /// <summary>
    /// Move this SpaceObject towards the given position by the given amount
    /// </summary>
    /// <param name="moveTo">Position to move towards</param>
    /// <param name="speed">Speed to move towards position</param>
    public void moveTowards(Vector2 moveTo, float speed)
    {
        moveTo = closestMirrorOfPosition(moveTo);
        modifyVelocityAbsolute((moveTo - position) / distanceFrom(moveTo) * speed);
    }

    /// <summary>
    /// Move this SpaceObject towards the given SpaceObject's position by the given amount
    /// </summary>
    /// <param name="moveTo">SpaceObject to move towards</param>
    /// <param name="speed">Speed to move towards the given SpaceObject</param>
    public void moveTowards(SpaceObject moveTo, float speed)
    {
        moveTowards(moveTo.position, speed);
    }

    /// <summary>
    /// Turn this SpaceObject to face the given position
    /// </summary>
    /// <param name="turnTo">Position to turn to</param>
    public void turnTowards(Vector2 turnTo)
    {
        turnTo = closestMirrorOfPosition(turnTo);
        angle = Mathf.Rad2Deg * (Mathf.Atan2(position.y - turnTo.y, position.x - turnTo.x) + Mathf.PI / 2);
    }

    /// <summary>
    /// Turn this SpaceObject to face the given SpaceObject's position
    /// </summary>
    /// <param name="turnTo">SpaceObject to turn to</param>
    public void turnTowards(SpaceObject turnTo)
    {
        turnTowards(turnTo.position);
    }

    /// <summary>
    /// Turn this SpaceObject towards the given position by the given amount
    /// If the given amount is more than is needed to face the position, 
    /// then this SpaceObject will turn to face the postion
    /// Use a negative value to turn away.
    /// </summary>
    /// <param name="rotateTo">Position to turn towards</param>
    /// <param name="amount">Amount to turn, in degrees</param>
    public void turnTowards(Vector2 rotateTo, float amount)
    {
        float theAngle = angle;
        while (theAngle < 0)
        {
            theAngle += 360;
        }
        while (theAngle > 360)
        {
            theAngle -= 360;
        }

        rotateTo = closestMirrorOfPosition(rotateTo);
        float angleTo = (float)Math.Atan2(position.y - rotateTo.y, position.x - rotateTo.x) + 0.5f * Mathf.PI;
        angleTo *= Mathf.Rad2Deg;

        while (angleTo < 0)
        {
            angleTo += 360;
        }
        while (angleTo > 360)
        {
            angleTo -= 360;
        }

        float angleDiff = angleTo - theAngle;

        while (angleDiff < 0)
        {
            angleDiff += 360;
        }
        while (angleDiff > 360)
        {
            angleDiff -= 360;
        }

        if (angleDiff < amount)
        {
            angle = angleTo;
        }
        else if (angleDiff > 180)
        {
            angle = theAngle - amount;
        }
        else
        {
            angle = theAngle + amount;
        }
    }

    /// <summary>
    /// Turn this SpaceObject towards the given SpaceObject's position by the given amount
    /// If the given amount is more than is needed to face the given SpaceObject, 
    /// then this SpaceObject will turn to face the given SpaceObject
    /// Use a negative value to turn away.
    /// </summary>
    /// <param name="rotateTo">SpaceObject to turn towards</param>
    /// <param name="amount">Amount to turn, in degrees</param>
    public void turnTowards(SpaceObject rotateTo, float amount)
    {
        turnTowards(rotateTo.position, amount);
    }

    /// <summary>
    /// Turn this SpaceObject to face away from the given position
    /// </summary>
    /// <param name="turnTo">Position to turn away from</param>
    public void turnAway(Vector2 awayFrom)
    {
        awayFrom = closestMirrorOfPosition(awayFrom);
        angle = Mathf.Rad2Deg * (Mathf.Atan2(position.y - awayFrom.y, position.x - awayFrom.x) - Mathf.PI / 2);
    }

    /// <summary>
    /// Turn this SpaceObject to face away from the given SpaceObject's position
    /// </summary>
    /// <param name="turnTo">SpaceObject to turn away from</param>
    public void turnAway(SpaceObject awayFrom)
    {
        turnAway(awayFrom.position);
    }

    /// <summary>
    /// Finds the angle between this SpaceObject and the given position.
    /// </summary>
    /// <param name="to">Position to find angle between.</param>
    /// <returns>Return the angle between this SpaceObject and the given position.</returns>
    public float angleToAbsolute(Vector2 to)
    {
        return position.angleFrom(to);
    }

    /// <summary>
    /// Finds the angle between this SpaceObject and the given SpaceObject's position.
    /// </summary>
    /// <param name="to">Position to find angle between.</param>
    /// <returns>Return the angle between this SpaceObject and the given SpaceObject's position.</returns>
    public float angleToAbsolute(SpaceObject to)
    {
        return angleToAbsolute(to.position);
    }

    /// <summary>
    /// Finds the angle between the direction this SpaceObject is facing and the given position.
    /// </summary>
    /// <param name="to">Position to find angle between.</param>
    /// <returns>Returns the angle between the direction this SpaceObject is facing the given position.</returns>
    public float angleToRelative(Vector2 to)
    {
        return position.angleFrom(to) - angle;
    }

    /// <summary>
    /// Finds the angle between the direction this SpaceObject is facing and the given SpaceObject's position.
    /// </summary>
    /// <param name="to">Position to find angle between.</param>
    /// <returns>Returns the angle between the direction this SpaceObject is facing the given SpaceObject's position.</returns>
    public float angleToRelative(SpaceObject to)
    {
        return angleToRelative(to.position);
    }

    /// <summary>
    /// Move this SpaceObject in the direction it is facing by the given amount. 
    /// </summary>
    /// <param name="speed">Amount to move.</param>
    public void moveForward(float speed)
    {
        moveDirection(speed, angle);
    }

    /// <summary>
    /// Move this SpaceObject in the given direction the given amount.
    /// </summary>
    /// <param name="speed">Amount to move.</param>
    /// <param name="direction">Direction to move.</param>
    public void moveDirection(float speed, float direction)
    {
        velocity += new Vector2(-Mathf.Sin(direction * Mathf.Deg2Rad) * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * speed);
    }

    /// <summary>
    /// Finds the point and time that an intersection will take place between 
    /// the given target (at its current speed, direction) and this object (going at the given speed)
    /// it is assumed that this object can turn any direction to intersect
    /// The time (z) returned is -1 if an intersection cannot be found
    /// </summary>
    /// <param name="intersect">Object to intersect with</param>
    /// <param name="speed">Speed this object will travel to intersect</param>
    /// <returns>(x, y) is the intersect position, (z) is the intersect time in seconds</returns>
    public Vector3 intersectPosTime(SpaceObject intersect, float speed)
    {
        return intersectPosTime(intersect, speed, position);
    }

    /// <summary>
    /// Finds the point and time that an intersection will take place between 
    /// the given target (at its current speed, direction) and the given position (going at the given speed)
    /// it is assumed that the object at the given position can turn any direction to intersect
    /// The time (z) returned is -1 if an intersection cannot be found
    /// </summary>
    /// <param name="intersect">SpaceObject to intersect with</param>
    /// <param name="speed">Speed object at position will travel to intersect</param>
    /// <param name="position">Position an object moving at the given speed will start from.</param>
    /// <returns>(x, y) is the intersect position, (z) is the intersect time in seconds</returns>
    public static Vector3 intersectPosTime(SpaceObject intersect, float speed, Vector2 position)
    {
        float time = float.PositiveInfinity;
        Vector2 mirror = Vector2.positiveInfinity;

        //solve the quadratic equation
        foreach (Vector2 pos in mirrorsOfPosition(intersect.position))
        {
            //find a in the quadratic equation
            float a = intersect.velocity.x * intersect.velocity.x + intersect.velocity.y * intersect.velocity.y - speed * speed;

            //find b in the quadratic equation
            float b = 2 * (intersect.velocity.x * (pos.x - position.x)
             + intersect.velocity.y * (pos.y - position.y));

            //find c in the quadratic equation
            float c = (pos.x - position.x) * (pos.x - position.x) +
                (pos.y - position.y) * (pos.y - position.y);

            //find under the sqrt in the quadratic equation
            float disc = b * b - 4 * a * c;

            //if under the sqrt is < 0, then there is no solution, so return a time value of -1
            if (disc < 0)
            {
                return new Vector3(0, 0, -1);
            }
            else
            {
                float tempTime;

                //if disc == 0, then there is only one possible solution
                if (disc == 0)
                {
                    tempTime = -b / (2 * a);
                }
                //if disc != 0, then find both possible solutions and choose the quicker of the two
                else
                {
                    tempTime = (-b + Mathf.Sqrt(disc)) / (2 * a);
                    float time2 = (-b - Mathf.Sqrt(disc)) / (2 * a);

                    if (tempTime < 0 || time2 < tempTime && time2 >= 0)
                    {
                        tempTime = time2;
                    }
                }
                
                //see if this solution is the quickest solution out of the position mirrors already checked
                //if so, set the solution as the quickest solution
                if (tempTime < time && tempTime > 0)
                {
                    time = tempTime;
                    mirror = pos;
                }
            }
        }

        //if the quickest solution is negative or greater than 10 seconds, there is no
        //viable solution, so return a time value of -1
        if (time < 0 || time > 10)
        {
            return new Vector3(0, 0, -1);
        }

        //if there is a viable solution, find where the intersect position will be by 
        //plugging the time into the intersect SpaceObject's current velocity
        //then return the intersect position and seconds to intersect
        else
        {
            mirror = new Vector2(time * intersect.velocity.x + mirror.x,
                time * intersect.velocity.y + mirror.y);

            return new Vector3(mirror.x, mirror.y, time);
        }
    }

    /// <summary>
    /// Disables this SpaceObject. If this SpaceObject is not a Player, then also destroy it. 
    /// </summary>
    public void destroyThis()
    {
        active = false;
        if (GetType() == typeof(Player))
        {
            destroyObject();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected Level level
    {
        get
        {
            return Level.current;
        }
    }

    /// <summary>
    /// Returns the Level's difficulty if this SpaceObject is not on a Player team.
    /// Returns 1 if this SpaceObject is on a Player's team.
    /// </summary>
    public float difficultyModifier
    {
        get
        {
            if (team > 0)
                return 1;
            else
                return level.difficulty;
        }
    }

    //called shortly after this SpaceObject is created
    protected abstract void startObject();
    protected void Start ()
    {
        theEffectAudio = gameObject.AddComponent<AudioSource>();
        startObject();
	}

    // Called every time the game is FixedUpated, 50 times a second by default
    protected abstract void updateObject();
	protected void FixedUpdate ()
    {
        updateObject();

        //make sure this is not moving faster than its max values
        if (speed > maxSpeed)
            speed = maxSpeed;
        if (angularVelocity > maxAngularSpeed)
            angularVelocity = maxAngularSpeed;


        Vector2 pos = position;

        //See if this is inside the Level's bounds
        if (pos.x > level.gameBounds.xMax)
            pos.x -= level.gameBounds.width;
        else if (pos.x < level.gameBounds.xMin)
            pos.x += level.gameBounds.width;
        if (pos.y > level.gameBounds.yMax)
            pos.y -= level.gameBounds.height;
        else if (pos.y < level.gameBounds.yMin)
            pos.y += level.gameBounds.height;

        //if its not inside the Level's bounds, change its position to make it inside the bounds
        if (position != pos)
        {
            position += velocity * level.secsPerUpdate;
            position = pos;
        }

        //update the effects volume
        theEffectAudio.volume = Options.get().volumeMusic * effectVolume;
    }

    // Called right before this SpaceObject is destroyed
    protected abstract void destroyObject();
    protected void OnDestroy()
    {
        destroyObject();
    }
}
