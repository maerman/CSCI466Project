using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class extendVector
{
    public static Vector2 mod(this Vector2 dividend, Vector2 divisor)
    {
        return new Vector2(dividend.x % divisor.x, dividend.y % divisor.y);
    }

    public static Vector3 mod(this Vector3 dividend, Vector3 divisor)
    {
        return new Vector3(dividend.x % divisor.x, dividend.y % divisor.y, dividend.z % divisor.z);
    }

    public static Vector2 rotate(this Vector2 toRotate, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        return new Vector2((float)(cos * toRotate.x - sin * toRotate.y), (float)(sin * toRotate.x + cos * toRotate.y));
    }
}

public abstract class SpaceObject : MonoBehaviour {
    //probably need to change this and put it somewhere else
    public float maxSpeed = 10;

    public abstract Vector2 position { get; set; }

    public abstract Vector2 velocity { get; set; }

    public abstract float angularVelocity { get; set; }

    public abstract Vector2 scale { get; set; }

    public abstract float mass { get; set; }

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
                velocity = new Vector2(0, value);
            }
            else if (velocity.y == 0)
            {
                velocity = new Vector2(value, 0);
            }
            {
                velocity = new Vector2(velocity.x / speed * value, velocity.y / speed * value);
            }
        }
    }

    public void modifySpeed(float change)
    {
        velocity = new Vector2(velocity.x / speed * change, velocity.y / speed * change);
    }

    public abstract float angle { get; set; }

    public float distanceFrom(Vector2 from)
    {
        return (float)System.Math.Sqrt((position.y - from.y) * (position.y - from.y) +
            (position.x - from.x) * (position.x - from.x));
    }

    public float distanceFrom(SpaceObject from)
    {
        return (float)System.Math.Sqrt((position.y - from.position.y) * (position.y - from.position.y) +
            (position.x - from.position.x) * (position.x - from.position.x));
    }

    public void moveTowards(Vector2 moveTo, float speed)
    {
        modifyVelocityAbsolute((moveTo - position) / distanceFrom(moveTo) * speed);
    }

    public void moveTowards(SpaceObject moveTo, float speed)
    {
        modifyVelocityAbsolute((moveTo.position - position) / distanceFrom(moveTo) * speed);
    }

    public void turnTowards(Vector2 turnTo)
    {
        angle = (float)Math.Atan2(position.y - turnTo.y, position.x - turnTo.x) + (float)Math.PI;
    }

    public void turnTowards(SpaceObject turnTo)
    {
        angle = (float)Math.Atan2(position.y - turnTo.position.y, position.x - turnTo.position.x) + (float)Math.PI;
    }

    public void turnAway(Vector2 awayFrom)
    {
        angle = (float)Math.Atan2(position.y - awayFrom.y, position.x - awayFrom.x);
    }

    public void turnAway(SpaceObject awayFrom)
    {
        angle = (float)Math.Atan2(position.y - awayFrom.position.y, position.x - awayFrom.position.x);
    }

    public void moveForward(float speed)
    {
        moveDirection(speed, angle);
    }

    public void moveDirection(float speed, float direction)
    {
        velocity += new Vector2(-(float)Math.Sin(direction * Mathf.Deg2Rad) * speed, (float)Math.Cos(direction * Mathf.Deg2Rad) * speed);
    }

    //Not sure if works, needs tested
    public void rotateTowards(Vector2 rotateTo, float amount)
    {
        float angleTo = (float)Math.Atan2(position.y - rotateTo.y, position.x - rotateTo.x) + (float)Math.PI;
        float angleDiff = angleTo - angle;
        angleDiff %= (float)(2 * Math.PI);

        if (angleDiff < Math.PI)
        {
            if (angleDiff < amount)
            {
                angle = angleTo;
            }
            else
            {
                angle -= amount;
            }
        }
        else
        {
            if (Math.PI * 2 - angleDiff < amount)
            {
                angle = angleTo;
            }
            else
            {
                angle += amount;
            }
        }
    }

    public void rotateTowards(SpaceObject rotateTo, float amount)
    {
        rotateTowards(rotateTo.position, amount);
    }

    public virtual void destroyThis()
    {
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
    // Use this for initialization
    void Start ()
    {
        startObject();
	}

    protected abstract void updateObject();
	// Update is called once per frame
	void Update ()
    {
        updateObject();

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        if (position.y > Level.GAME_SIZE.y || position.x > Level.GAME_SIZE.x || position.y < 0 || position.x < 0)
        {
            Vector2 newPos = position.mod(Level.GAME_SIZE);

            if (newPos.x < 0)
            {
                newPos.x += Level.GAME_SIZE.x;
            }

            if (newPos.y < 0)
            {
                newPos.y += Level.GAME_SIZE.y;
            }

            position = newPos;
        }
	}
}
