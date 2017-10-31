﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceObject : MonoBehaviour
{
    public float maxSpeed = 15;

    public bool inPlay = true;

    public sbyte team = 0;

    public abstract Vector2 position { get; set; }

    public abstract Vector2 velocity { get; set; }

    public abstract float angle { get; set; }

    public abstract float angularVelocity { get; set; }

    public abstract float mass { get; set; }

    public abstract Bounds bounds { get; }

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

    public bool enabled
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

    public void modifyVelocityAbsolute(float changeX, float changeY)
    {
        modifyVelocityAbsolute(new Vector2(changeX, changeY));
    }

    public void modifyVelocityAbsolute(Vector2 change)
    {
        GetComponent<Rigidbody2D>().velocity += change;
    }

    public void modifyVelocityRelative(float changeX, float changeY)
    {
        modifyVelocityRelative(new Vector2(changeX, changeY));

    }

    public void modifyVelocityRelative(Vector2 change)
    {
        GetComponent<Rigidbody2D>().velocity += change.rotate(angle);
    }

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

    public void modifySpeed(float change)
    {
        velocity = new Vector2(velocity.x / speed * change, velocity.y / speed * change);
    }

    public float distanceFromScreenPosition(Vector2 from)
    {
        return (float)System.Math.Sqrt(distanceFromScreenPositionSquared(from));
    }

    public float distanceFromScreenPositionSquared(Vector2 from)
    {
        return (float)(position.y - from.y) * (position.y - from.y) +
            (position.x - from.x) * (position.x - from.x);
    }

    public static Vector2[] mirrorsOfPosition(Vector2 position)
    {
        Vector2[] mirrors = new Vector2[9];

        mirrors[0] = position;
        mirrors[1] = new Vector2(position.x, position.y - Level.currentLevel.gameBounds.height);
        mirrors[2] = new Vector2(position.x, position.y + Level.currentLevel.gameBounds.height);
        mirrors[3] = new Vector2(position.x - Level.currentLevel.gameBounds.width, position.y);
        mirrors[4] = new Vector2(position.x + Level.currentLevel.gameBounds.width, position.y);
        mirrors[5] = new Vector2(position.x - Level.currentLevel.gameBounds.width, position.y - Level.currentLevel.gameBounds.height);
        mirrors[6] = new Vector2(position.x + Level.currentLevel.gameBounds.width, position.y - Level.currentLevel.gameBounds.height);
        mirrors[7] = new Vector2(position.x - Level.currentLevel.gameBounds.width, position.y + Level.currentLevel.gameBounds.height);
        mirrors[8] = new Vector2(position.x + Level.currentLevel.gameBounds.width, position.y + Level.currentLevel.gameBounds.height);

        return mirrors;
    }

    public Vector2 closestMirrorOfPosition(Vector2 position)
    {
        Vector2 closestPosition = position;
        float shortestDistance = float.PositiveInfinity;

        foreach (Vector2 item in mirrorsOfPosition(position))
        {
            float tempDistance = distanceFromScreenPosition(item);
            if (tempDistance < shortestDistance)
            {
                closestPosition = item;
                shortestDistance = tempDistance;
            }
        }

        return closestPosition;
    }

    public float distanceFrom(Vector2 from)
    {
        return distanceFromScreenPosition(closestMirrorOfPosition(from));
    }

    public float distanceFrom(SpaceObject from)
    {
        return distanceFrom(from.position);
    }

    public float distanceFromSquared(Vector2 from)
    {
        return distanceFromScreenPositionSquared(closestMirrorOfPosition(from));
    }

    public float distanceFromSquared(SpaceObject from)
    {
        return distanceFromSquared(from.position);
    }

    public Vector2 vector2From(Vector2 from)
    {
        return closestMirrorOfPosition(from) - position;
    }

    public Vector2 vector2From(SpaceObject from)
    {
        return vector2From(from.position);
    }

    public T closestObject<T>(IEnumerable<IEnumerable<T>> objectLists) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
        {
            T temp = closestObject<T>(item);

            if (temp != null )
            { 
                float tempDistance = distanceFromSquared(temp);

                if (tempDistance < closestDistance)
                {
                    closest = temp;

                } closestDistance = tempDistance;
            }
        }

        return closest;
    }

    public T closestObject<T>(IEnumerable<T> objectList) where T: SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item.inPlay && item != this)
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

    public T closestObject<T>(IEnumerable<IEnumerable<T>> objectLists, bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
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

        return closest;
    }

    public T closestObject<T>(IEnumerable<T> objectList, bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (((sameTeam && item.team == team) || (!sameTeam && item.team != team)) && item != this && item.inPlay)
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

    public T closestObjectInDirection<T>(IEnumerable<IEnumerable<T>> objectLists, float direction) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
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

        return closest;
    }

    public T closestObjectInDirection<T>(IEnumerable<T> objectList, float direction) where T : SpaceObject
    {
        Ray ray = new Ray(position, new Vector2(0, 1).rotate(direction));

        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (item.inPlay && item != this)
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

    public T closestObjectInDirection<T>(IEnumerable<IEnumerable<T>> objectLists, float direction,  bool sameTeam) where T : SpaceObject
    {
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (IEnumerable<T> item in objectLists)
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

        return closest;
    }

    public T closestObjectInDirection<T>(IEnumerable<T> objectList, float direction, bool sameTeam) where T : SpaceObject
    {
        Ray ray = new Ray(position, new Vector2(0, 1).rotate(direction));
        T closest = null;
        float closestDistance = float.MaxValue;

        foreach (T item in objectList)
        {
            if (((sameTeam && item.team == team) || (!sameTeam && item.team != team)) && item != this && item.inPlay)
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

    public void moveTowards(Vector2 moveTo, float speed)
    {
        moveTo = closestMirrorOfPosition(moveTo);
        modifyVelocityAbsolute((moveTo - position) / distanceFrom(moveTo) * speed);
    }

    public void moveTowards(SpaceObject moveTo, float speed)
    {
        moveTowards(moveTo.position, speed);
    }

    public void turnTowards(Vector2 turnTo)
    {
        turnTo = closestMirrorOfPosition(turnTo);
        angle = Mathf.Rad2Deg * (Mathf.Atan2(position.y - turnTo.y, position.x - turnTo.x) + Mathf.PI / 2);
    }

    public void turnTowards(SpaceObject turnTo)
    {
        turnTowards(turnTo.position);
    }

    public void turnAway(Vector2 awayFrom)
    {
        awayFrom = closestMirrorOfPosition(awayFrom);
        angle = Mathf.Rad2Deg * (Mathf.Atan2(position.y - awayFrom.y, position.x - awayFrom.x) - Mathf.PI / 2);
    }

    public void turnAway(SpaceObject awayFrom)
    {
        turnAway(awayFrom.position);
    }

    public void moveForward(float speed)
    {
        moveDirection(speed, angle);
    }

    public void moveDirection(float speed, float direction)
    {
        velocity += new Vector2(-Mathf.Sin(direction * Mathf.Deg2Rad) * speed, Mathf.Cos(direction * Mathf.Deg2Rad) * speed);
    }


    //has bugs, needs fixed
    public void rotateTowards(Vector2 rotateTo, float amount)
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

        int direction;
        if (angleDiff > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        while (angleDiff < 0)
        {
            angleDiff += 360;
        }
        while (angleDiff > 360)
        {
            angleDiff -= 360;
        }

        if (angleDiff < amount || 360.0f - angle < amount)
        {
            angle = angleTo;
        }
        else
        {
            angle = theAngle - amount * direction;
        }

    }

    public void rotateTowards(SpaceObject rotateTo, float amount)
    {
        rotateTowards(rotateTo.position, amount);
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
    protected Vector3 intersectPosTime(SpaceObject intersect, float speed)
    {
        return intersectPosTime(intersect, speed, position);
    }

    protected static Vector3 intersectPosTime(SpaceObject intersect, float speed, Vector2 position)
    {
        float time = float.PositiveInfinity;
        Vector2 mirror = Vector2.positiveInfinity;

        foreach (Vector2 pos in mirrorsOfPosition(intersect.position))
        {
            float a = intersect.velocity.x * intersect.velocity.x + intersect.velocity.y * intersect.velocity.y - speed * speed;

            float b = 2 * (intersect.velocity.x * (pos.x - position.x)
             + intersect.velocity.y * (pos.y - position.y));

            float c = (pos.x - position.x) * (pos.x - position.x) +
                (pos.y - position.y) * (pos.y - position.y);

            float disc = b * b - 4 * a * c;

            if (disc < 0)
            {
                return new Vector3(0, 0, -1);
            }
            else
            {
                float tempTime;
                if (disc == 0)
                {
                    tempTime = -b / (2 * a);
                }
                else
                {
                    tempTime = (-b + Mathf.Sqrt(disc)) / (2 * a);
                    float time2 = (-b - Mathf.Sqrt(disc)) / (2 * a);

                    if (tempTime < 0 || time2 < tempTime && time2 >= 0)
                    {
                        tempTime = time2;
                    }
                }
                
                if (tempTime < time)
                {
                    time = tempTime;
                    mirror = pos;
                }
            }
        }

        mirror = new Vector2(time * intersect.velocity.x + mirror.x,
                time * intersect.velocity.y + mirror.y);

        if (time < 0 || time > 10)
        {
            return new Vector3(0, 0, -1);
        }
        else
        {
            return new Vector3(mirror.x, mirror.y, time);
        }
    }

    public void destroyThis()
    {
        inPlay = false;
        Destroy(gameObject);
    }

    protected Level level
    {
        get
        {
            return Level.currentLevel;
        }
    }

    protected abstract void startObject();
    protected void Start ()
    {
        startObject();
	}

    protected abstract void updateObject();
	protected void FixedUpdate ()
    {
        updateObject();

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        Vector2 pos = position;
        if (pos.x > level.gameBounds.xMax)
        {
            pos.x -= level.gameBounds.width;
        }
        else if (pos.x < level.gameBounds.xMin)
        {
            pos.x += level.gameBounds.width;
        }
        if (pos.y > level.gameBounds.yMax)
        {
            pos.y -= level.gameBounds.height;
        }
        else if (pos.y < level.gameBounds.yMin)
        {
            pos.y += level.gameBounds.height;
        }

        if (position != pos)
        {
            position += velocity * level.secsPerUpdate;
            position = pos;
        }
	}

    protected abstract void destroyObject();
    protected void OnDestroy()
    {
        destroyObject();
        inPlay = false;
    }
}
