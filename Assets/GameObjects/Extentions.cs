// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Thomas Stewart

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Adds some useful methods various classes associated with GameObjects
/// </summary>
public static class GameObjectExtentions
{
    /// <summary>
    /// Applies the given aplha color channel to sprites, images and texts attached to this Transform.
    /// </summary>
    /// <param name="applyTo"></param>
    /// <param name="alpha">What to change the alpha channel to.</param>
    public static void applyAlpha(this Transform applyTo, float alpha)
    {
        SpriteRenderer sprite = applyTo.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            Color color = sprite.color;
            color.a = alpha;
            sprite.color = color;
        }

        Image image = applyTo.GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        Text text = applyTo.GetComponent<Text>();
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    /// <summary>
    /// Applies the given aplha color channel to this Text.
    /// </summary>
    /// <param name="applyTo"></param>
    /// <param name="alpha">What to change the alpha channel to.</param>
    public static void applyAlpha(this Text applyTo, float alpha)
    { 
            Color color = applyTo.color;
            color.a = alpha;
            applyTo.color = color;
    }

    /// <summary>
    /// Applies the given aplha color channel to this SpriteRenderer.
    /// </summary>
    /// <param name="applyTo"></param>
    /// <param name="alpha">What to change the alpha channel to.</param>
    public static void applyAlpha(this SpriteRenderer applyTo, float alpha)
    {
        Color color = applyTo.color;
        color.a = alpha;
        applyTo.color = color;
    }

    /// <summary>
    /// Applies the given aplha color channel to this Image.
    /// </summary>
    /// <param name="applyTo"></param>
    /// <param name="alpha">What to change the alpha channel to.</param>
    public static void applyAlpha(this Image applyTo, float alpha)
    {
        Color color = applyTo.color;
        color.a = alpha;
        applyTo.color = color;
    }

    /// <summary>
    /// Applies the given aplha color channel to sprites, images and texts attached to this GameObject.
    /// </summary>
    /// <param name="applyTo"></param>
    /// <param name="alpha">What to change the alpha channel to.</param>
    public static void applyAlphaToChildren(this GameObject applyTo, float alpha)
    {
        foreach (Transform item in applyTo.transform)
        {
            item.applyAlpha(alpha);
        }
    }
}

/// <summary>
/// Adds some usefull methods to the Vector2 and Vector3
/// </summary>
public static class VectorExtentions
{
    /// <summary>
    /// Adds the given float to both parts of this Vector2
    /// </summary>
    /// <param name="addend">this</param>
    /// <param name="toAdd">Amount to add</param>
    /// <returns>The result of the addition</returns>
    public static Vector2 add(this Vector2 addend, float toAdd)
    {
        return new Vector2(addend.x + toAdd, addend.y + toAdd);
    }

    /// <summary>
    /// Subtracts the given float from both parts of this Vector2
    /// </summary>
    /// <param name="addend">this</param>
    /// <param name="subtrahend">Amount to subtract</param>
    /// <returns>The result of the subtraction</returns>
    public static Vector2 sub(this Vector2 minuend, float subtrahend)
    {
        return new Vector2(minuend.x + subtrahend, minuend.y + subtrahend);
    }

    /// <summary>
    /// Mods each part this Vector2 by the corrosponding part of the given Vector2
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to mod by</param>
    /// <returns>The result of the mod</returns>
    public static Vector2 mod(this Vector2 dividend, Vector2 divisor)
    {
        return new Vector2(dividend.x % divisor.x, dividend.y % divisor.y);
    }

    /// <summary>
    /// Mods each part this Vector3 by the corrosponding part of the given Vector3
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to mod by</param>
    /// <returns>The result of the mod</returns>
    public static Vector3 mod(this Vector3 dividend, Vector3 divisor)
    {
        return new Vector3(dividend.x % divisor.x, dividend.y % divisor.y, dividend.z % divisor.z);
    }

    /// <summary>
    /// Multipies each part this Vector2 by the corrosponding part of the given Vector2
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to dot by</param>
    /// <returns>The result of the dot product</returns>
    public static Vector2 dot(this Vector2 v1, Vector2 v2)
    {
        return new Vector2(v1.x * v2.x, v1.y * v2.y);
    }

    /// <summary>
    /// Multipies each part this Vector3 by the corrosponding part of the given Vector3
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to dot by</param>
    /// <returns>The result of the dot product</returns>
    public static Vector3 dot(this Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    /// <summary>
    /// Divides each part this Vector2 by the corrosponding part of the given Vector2
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to divide by</param>
    /// <returns>The result of the division</returns>
    public static Vector2 div(this Vector2 dividend, Vector2 divisor)
    {
        return new Vector2(dividend.x / divisor.x, dividend.y / divisor.y);
    }

    /// <summary>
    /// Divides each part this Vector3 by the corrosponding part of the given Vector3
    /// </summary>
    /// <param name="dividend">this</param>
    /// <param name="divisor">What to divide by</param>
    /// <returns>The result of the division</returns>
    public static Vector3 div(this Vector3 dividend, Vector3 divisor)
    {
        return new Vector3(dividend.x / divisor.x, dividend.y / divisor.y, dividend.z / divisor.z);
    }

    /// <summary>
    /// Rotates this Vector2 clockwise in a 2d plane by the given angle in degrees
    /// </summary>
    /// <param name="toRotate">this</param>
    /// <param name="angle">Amount to rotate in degrees</param>
    /// <returns>The result of the rotation</returns>
    public static Vector2 rotate(this Vector2 toRotate, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        return new Vector2((float)(cos * toRotate.x - sin * toRotate.y), (float)(sin * toRotate.x + cos * toRotate.y));
    }

    /// <summary>
    /// Finds the angle in degrees between this and the given Vector2 if they were in the same 2d plane
    /// </summary>
    /// <param name="position">this</param>
    /// <param name="positionTo">What to find the angle between.</param>
    /// <returns>The angle between this and the given Vector2</returns>
    public static float angleFrom(this Vector2 position, Vector2 positionTo)
    {
        return Mathf.Rad2Deg * (Mathf.Atan2(position.y - positionTo.y, position.x - positionTo.x) + Mathf.PI / 2);
    }

    /// <summary>
    /// Creates a Vector2 with this Vector2's magnitude that is pointing in the given direction 
    /// </summary>
    /// <param name="original">this</param>
    /// <param name="direction">direction to point the Vector2 in</param>
    /// <returns>A Vector2 with the same magnitude in the given direction</returns>
    public static Vector2 toAngle(this Vector2 original, float direction)
    {
        float magnitude = original.magnitude;
        original.x = -Mathf.Sin(direction * Mathf.Deg2Rad) * magnitude;
        original.y = Mathf.Cos(direction * Mathf.Deg2Rad) * magnitude;
        return original;
    }

    /// <summary>
    /// Finds the angle in degrees this Vector2 is pointing in a 2d plane
    /// </summary>
    /// <param name="angleFrom">this</param>
    /// <returns>The angle this Vector2 is pointing</returns>
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
            //at origin, default angle of zero
            else
            {
                return 0f;
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
