using System;
using System.Collections.Generic;
using UnityEngine;

class Options
{
    public static int maxAutosaves = 20;

    //enable all keys to be changed
    //effects volume
    //music volume
    //resolution
    //CameraController.edgeBufferSize;
    //CameraController.zoomSpeed;

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

        float interfaceAlphaChangeAmount = 0.1f;
        if (Input.GetKeyDown(KeyCode.F5))
        {
            IngameInterface.interfaceAlpha -= interfaceAlphaChangeAmount;

            if (IngameInterface.interfaceAlpha < 0)
                IngameInterface.interfaceAlpha = 0;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            IngameInterface.interfaceAlpha += interfaceAlphaChangeAmount;

            if (IngameInterface.interfaceAlpha > 1)
                IngameInterface.interfaceAlpha = 1;
        }

        float healthBarAlphaChangeAmount = 0.1f;
        if (Input.GetKeyDown(KeyCode.F7))
        {
            HealthBar.healthBarAlpha -= healthBarAlphaChangeAmount;

            if (HealthBar.healthBarAlpha < 0)
                HealthBar.healthBarAlpha = 0;
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            HealthBar.healthBarAlpha += healthBarAlphaChangeAmount;

            if (HealthBar.healthBarAlpha > 1)
                HealthBar.healthBarAlpha = 1;
        }
    }
}
