using System;
using System.Collections.Generic;
using UnityEngine;

class Options
{
    public static void updateOptions()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Controls.get().staticLevel = !Controls.get().staticLevel;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Key.XBoxControllerNames = !Key.XBoxControllerNames;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            foreach (PlayerControls item in Controls.get().players)
            {
                item.setRelativeMovement(!item.relativeMovement);
            }
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            foreach (PlayerControls item in Controls.get().players)
            {
                item.setTurns(!item.turns);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            HealthBar.showHealth = !HealthBar.showHealth;
        }
    }
}
