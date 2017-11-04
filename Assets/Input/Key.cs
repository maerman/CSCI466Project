using UnityEngine;
using System.Collections;

public class Key
{
    public static bool XBoxControllerNames = true;

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

    public string toShortString()
    {
        string toReturn = ToString();

        if (toReturn.Equals("LeftArrow"))
            return "Left";
        else if (toReturn.Equals("RightArrow"))
            return "Right";

        toReturn = toReturn.Replace("Backspace", "BckSp");
        toReturn = toReturn.Replace("Delete", "Del");
        toReturn = toReturn.Replace("Escape", "Esc");
        toReturn = toReturn.Replace("Keypad", "Num ");
        toReturn = toReturn.Replace("Period", ".");
        toReturn = toReturn.Replace("Divide", "/");
        toReturn = toReturn.Replace("Multiply", "*");
        toReturn = toReturn.Replace("Minus", "-");
        toReturn = toReturn.Replace("Plus", "+");
        toReturn = toReturn.Replace("Equals", "=");
        toReturn = toReturn.Replace("Arrow", "");
        toReturn = toReturn.Replace("Page", "Pg");
        toReturn = toReturn.Replace("Alpha", "");
        toReturn = toReturn.Replace("Exclaim", "!");
        toReturn = toReturn.Replace("DoubleQuote", "\"");
        toReturn = toReturn.Replace("Hash", "#");
        toReturn = toReturn.Replace("Dollar", "$");
        toReturn = toReturn.Replace("Ampersand", "&");
        toReturn = toReturn.Replace("Quote", "'");
        toReturn = toReturn.Replace("LeftParen", "(");
        toReturn = toReturn.Replace("RightParen", ")");
        toReturn = toReturn.Replace("Asterisk", "*");
        toReturn = toReturn.Replace("Comman", ",");
        toReturn = toReturn.Replace("Slash", "/");
        toReturn = toReturn.Replace("Colon", ":");
        toReturn = toReturn.Replace("Semicolon", ";");
        toReturn = toReturn.Replace("Less", "<");
        toReturn = toReturn.Replace("Greater", ">");
        toReturn = toReturn.Replace("Question", "?");
        toReturn = toReturn.Replace("LeftBracket", "[");
        toReturn = toReturn.Replace("Backslash", "\\");
        toReturn = toReturn.Replace("RightBracket", "]");
        toReturn = toReturn.Replace("Caret", "^");
        toReturn = toReturn.Replace("Underscore", "_");
        toReturn = toReturn.Replace("Lock", "");
        toReturn = toReturn.Replace("Right", "R");
        toReturn = toReturn.Replace("Left", "L");
        toReturn = toReturn.Replace("Control", "Con");
        toReturn = toReturn.Replace("Command", "Com");
        toReturn = toReturn.Replace("Apple", "Apl");
        toReturn = toReturn.Replace("Windows", "Win");

        if (XBoxControllerNames)
        {
            if (toReturn.Contains("Button"))
            {
                toReturn = toReturn.Replace("Button0", "A_Bt");
                toReturn = toReturn.Replace("Button1", "B_Bt");
                toReturn = toReturn.Replace("Button2", "X_Bt");
                toReturn = toReturn.Replace("Button3", "Y_Bt");
                toReturn = toReturn.Replace("Button4", "LBmp");
                toReturn = toReturn.Replace("Button5", "RBmp");
                toReturn = toReturn.Replace("Button6", "Back");
                toReturn = toReturn.Replace("Button7", "Strt");
                toReturn = toReturn.Replace("Button8", "LStk");
                toReturn = toReturn.Replace("Button9", "RStk");
                toReturn = toReturn.Replace("Joystick", "");

                toReturn = toReturn.Substring(1); //Remove controller number
            }
            else if (toReturn.Contains("Axis"))
            {
                bool positive = false;
                if (toReturn[0] == '+')
                {
                    positive = true;
                }
                toReturn = toReturn.Substring(1);

                if (positive)
                {
                    toReturn = toReturn.Replace("Axis1", "+XAxs");
                    toReturn = toReturn.Replace("Axis2", "+YAxs");
                    toReturn = toReturn.Replace("Axis3", "RTrig");
                    toReturn = toReturn.Replace("Axis4", "+ZAxs");
                    toReturn = toReturn.Replace("Axis5", "+WAxs");
                    toReturn = toReturn.Replace("Axis6", "DUp");
                    toReturn = toReturn.Replace("Axis7", "DRght");
                    toReturn = toReturn.Replace("Axis8", "??");
                    toReturn = toReturn.Replace("Axis9", "LTrig");
                    toReturn = toReturn.Replace("Axis10", "RTrig");
                }
                else
                {
                    toReturn = toReturn.Replace("Axis1", "-XAxs");
                    toReturn = toReturn.Replace("Axis2", "-YAxs");
                    toReturn = toReturn.Replace("Axis3", "LTrig");
                    toReturn = toReturn.Replace("Axis4", "-ZAxs");
                    toReturn = toReturn.Replace("Axis5", "-WAxs");
                    toReturn = toReturn.Replace("Axis6", "DDown");
                    toReturn = toReturn.Replace("Axis7", "DLeft");
                    toReturn = toReturn.Replace("Axis8", "-Axs8");
                    toReturn = toReturn.Replace("Axis9", "LTrig");
                    toReturn = toReturn.Replace("Axis10", "RTrig");
                }

                toReturn = toReturn.Replace("Con", "");
                toReturn = toReturn.Substring(1); //remove controller number
            }
        }
        else
        {
            toReturn = toReturn.Replace("Joystick", "C");
            toReturn = toReturn.Replace("Button", "B");
            toReturn = toReturn.Replace("Con", "C");
            toReturn = toReturn.Replace("Axis", "A");
        }

        return toReturn;
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
