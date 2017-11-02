using UnityEngine;
using System.Collections;

public static class VectorExtention
{
    public static Vector2 mod(this Vector2 dividend, Vector2 divisor)
    {
        return new Vector2(dividend.x % divisor.x, dividend.y % divisor.y);
    }

    public static Vector3 mod(this Vector3 dividend, Vector3 divisor)
    {
        return new Vector3(dividend.x % divisor.x, dividend.y % divisor.y, dividend.z % divisor.z);
    }

    public static Vector2 dot(this Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x * v2.x, v1.y * v2.y);
    }

    public static Vector3 dot(this Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    public static Vector2 div(this Vector2 dividend, Vector2 divisor)
    {
        return new Vector2(dividend.x / divisor.x, dividend.y / divisor.y);
    }

    public static Vector3 div(this Vector3 dividend, Vector3 divisor)
    {
        return new Vector3(dividend.x / divisor.x, dividend.y / divisor.y, dividend.z / divisor.z);
    }

    public static Vector2 rotate(this Vector2 toRotate, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        return new Vector2((float)(cos * toRotate.x - sin * toRotate.y), (float)(sin * toRotate.x + cos * toRotate.y));
    }

    public static float angleFrom(this Vector2 position, Vector2 positionTo)
    {
        return Mathf.Rad2Deg * (Mathf.Atan2(position.y - positionTo.y, position.x - positionTo.x) + Mathf.PI / 2);
    }

    public static Vector2 toAngle(this Vector2 original, float angle)
    {
        original.toAngle(angle, original.magnitude);
        return original;
    }

    public static Vector2 toAngle(this Vector2 original, float angle, float magnitude)
    {
        original.x = -Mathf.Sin(angle * Mathf.Deg2Rad) * magnitude;
        original.y = Mathf.Cos(angle * Mathf.Deg2Rad) * magnitude;
        return original;
    }

    public static float getAngle(this Vector2 angleFrom)
    {
        if (angleFrom.x == 0f)
        {
            //straight up
            if (angleFrom.y > 0f)
            {
                return 0;
            }
            //straight down
            else if (angleFrom.y < 0f)
            {
                return 180f;
            }
            //at origin, no input
            else
            {
                return float.NaN;
            }
        }
        else if (angleFrom.y == 0f)
        {
            //straight right
            if (angleFrom.x > 0f)
            {
                return 90f;
            }
            //straight left
            else //angleFrom.x < 0
            {
                return 270f;
            }
        }
        else if (angleFrom.y > 0f)
        {
            //quadrent 1
            if (angleFrom.x > 0f)
            {
                return Mathf.Atan(angleFrom.x / angleFrom.y) * Mathf.Rad2Deg;
            }
            //quadrent 4
            else //angleFrom.x < 0
            {
                return 360f - Mathf.Atan(-angleFrom.x / angleFrom.y) * Mathf.Rad2Deg;
            }
        }
        else //angleFrom.y < 0
        {
            //quadrent 2
            if (angleFrom.x > 0f)
            {
                return 180f - Mathf.Atan(angleFrom.x / -angleFrom.y) * Mathf.Rad2Deg;
            }
            //quadrent 3
            else //angleFrom.x < 0
            {
                return 180f + Mathf.Atan(-angleFrom.x / -angleFrom.y) * Mathf.Rad2Deg;
            }
        }
    }
}
