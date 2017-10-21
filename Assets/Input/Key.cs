using UnityEngine;
using System.Collections;

public class Key
{
    public const float ACTIVATION_THRESHOLD = 0.5f;
    public const float DEAD_ZONE = 0.1f;
    public const int START_AXIS = 1024;
    public const int NUM_AXIS = 20;
    public const int NUM_CONTROLLERS = 4;

    private int value;

    public Key(KeyCode key)
    {
        value = (int)key;
    }

    public Key(int controller, int axis, bool positive)
    {
        if (controller < NUM_CONTROLLERS && axis < NUM_AXIS)
        {
            controller--;
            axis--;

            if (positive)
            {
                value = START_AXIS + controller * NUM_AXIS + axis * 2;
            }
            else
            {
                value = START_AXIS + controller * NUM_AXIS + axis * 2 + 1;
            }
        }
        else
        {
            throw new System.Exception("There is no " + axis.ToString() + " axis on controller " + controller.ToString());
        }
    }

    public Key(int key)
    {
        foreach (KeyCode item in System.Enum.GetValues(typeof(KeyCode)))
        {
            if ((int)item == key)
            {
                value = (int)item;
                return;
            }
        }

        if (key >= START_AXIS && key < START_AXIS + NUM_AXIS * NUM_CONTROLLERS)
        {
            value = key;
        }
        else
        {
            throw new System.Exception(key.ToString() + " is not a valid Key value!");
        }
    }

    public Key(Key key)
    {
        value = key.value;
    }

    public int getValue()
    {
        return value;
    }

    public bool isPressed()
    {
        try
        {
            if (value >= START_AXIS)
            {
                string name = ToString();

                if (name[0] == '+')
                {
                    return (Input.GetAxis(name.Substring(1)) > ACTIVATION_THRESHOLD);
                }
                else if (name[0] == '-')
                {
                    return (Input.GetAxis(name.Substring(1)) < -ACTIVATION_THRESHOLD);
                }
                else
                {
                    throw new System.Exception();
                }
            }
            else
            {
                return Input.GetKey((KeyCode)value);
            }
        }
        catch
        {
            throw new System.Exception(value.ToString() + " is not a valid Key value!");
        }
    }

    public float getAxis()
    {
        try
        {
            if (value >= START_AXIS)
            {
                string name = ToString();
                float axisValue = Input.GetAxis(name.Substring(1));

                if (name[0] == '+')
                {
                    if (axisValue > 0 && axisValue > DEAD_ZONE)
                    {
                        axisValue = (axisValue - DEAD_ZONE) / (1 - DEAD_ZONE);
                        return axisValue;
                    }
                    else
                    {
                        return 0f;
                    }
                }
                else if (name[0] == '-')
                {
                    if (axisValue < 0 && axisValue < -DEAD_ZONE)
                    {
                        axisValue = -axisValue;
                        axisValue = (axisValue - DEAD_ZONE) / (1 - DEAD_ZONE);
                        return axisValue;
                    }
                    else
                    {
                        return 0f;
                    }
                }
                else
                {
                    throw new System.Exception();
                }
            }
            else if (Input.GetKey((KeyCode)value))
            {
                return 1.0f;
            }
            else
            {
                return 0f;
            }
        }
        catch
        {
            throw new System.Exception(value.ToString() + " is not a valid Key value!");
        }
    }

    public override string ToString()
    {
        try
        {
            if (value >= START_AXIS)
            {
                int tempValue = value - START_AXIS;

                int controller = tempValue / NUM_AXIS + 1;
                int axis = tempValue % NUM_AXIS;
                bool positive = (tempValue % 2) == 0;
                axis = axis / 2 + 1;

                return (getAxisString(controller, axis, positive));
            }
            else
            {
                return ((KeyCode)value).ToString();
            }
        }
        catch
        {
            throw new System.Exception(value.ToString() + " is not a valid Key value!");
        }
    }

    public static string getAxisString(int controller, int axis)
    {
        return ("Con" + controller.ToString() + "Axis" + axis.ToString());
    }

    public static string getAxisString(int controller, int axis, bool positive)
    {
        if (positive)
        {
            return ("+" + getAxisString(controller, axis));
        }
        else
        {
            return ("-" + getAxisString(controller, axis));
        }
    }

    public static Key activatedKey()
    {
        foreach (KeyCode item in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(item))
            {
                return new Key(item);
            }
        }

        for (int i = 0; i < NUM_CONTROLLERS; i++)
        {
            for (int j = 0; j < NUM_AXIS; j++)
            {
                float axisValue = Input.GetAxis(getAxisString(i, j));
                if (System.Math.Abs(axisValue) > ACTIVATION_THRESHOLD)
                {
                    if (axisValue > 0)
                    {
                        return new Key(i, j, true);
                    }
                    else
                    {
                        return new Key(i, j, false);
                    }
                }
            }
        }

        return null;
    }
}
