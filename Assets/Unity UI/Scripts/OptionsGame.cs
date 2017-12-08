// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// OptionsGame is a MonoBehavior that controls the Game Options menu and has methods
/// that are called when its input UI items are changed, which edit their corropsonding Options values
/// </summary>
public class OptionsGame : MonoBehaviour
{
    //initilzed in editor
    public Slider autoSaves;
    public Slider interfaceAlpha;
    public Slider healthbarAlpha;
    public Slider cameraEdgeBuffer;
    public Slider zoomSpeed;
    public Slider keyDeadzone;
    public Slider keyActivationThreshold;
    public Toggle fixedGameBounds;
    public Toggle xboxNames;

    private Options options
    {
        get
        {
            return Options.get();
        }
    }

    /// <summary>
    /// Set values in the menu to the values in Options
    /// </summary>
    private void refresh()
    {
        autoSaves.value = options.levelMaxAutosaves;
        interfaceAlpha.value = options.ingameInterfaceAlpha;
        healthbarAlpha.value = options.healthBarAlpha;
        cameraEdgeBuffer.value = options.cameraEdgeBufferSize;
        zoomSpeed.value = options.cameraZoomSpeed;
        keyDeadzone.value = options.keyDeadZone;
        keyActivationThreshold.value = options.keyActivationThreshold;
        fixedGameBounds.isOn = options.levelStatic;
        xboxNames.isOn = options.keyXboxNames;
    }

    void Start()
    {
        refresh();
    }

    private void OnEnable()
    {
        refresh();
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Options value and refresh this menu.
    public void AutoSaves()
    {
        options.levelMaxAutosaves = (int)autoSaves.value;
        refresh();
    }
    public void InterfaceAlpha()
    {
        options.ingameInterfaceAlpha = interfaceAlpha.value;
        refresh();
    }
    public void HealthbarAlpha()
    {
        options.healthBarAlpha = healthbarAlpha.value;
        refresh();
    }
    public void CameraEdgeBuffer()
    {
        options.cameraEdgeBufferSize = cameraEdgeBuffer.value;
        refresh();
    }
    public void ZoomSpeed()
    {
        options.cameraZoomSpeed = zoomSpeed.value;
        refresh();
    }
    public void KeyDeadzone()
    {
        options.keyDeadZone = keyDeadzone.value;
        refresh();
    }
    public void KeyActivationThreshold()
    {
        options.keyActivationThreshold = keyActivationThreshold.value;
        refresh();
    }
    public void FixedGameBounds()
    {
        options.levelStatic = fixedGameBounds.isOn;
        refresh();
    }
    public void XBox360Names()
    {
        options.keyXboxNames = xboxNames.isOn;
        refresh();
    }

    /// <summary>
    /// Method called by the Reset button, resets the Options to their default values and 
    /// refreshes this menu
    /// </summary>
    public void ResetButton()
    {
        options.setDefaultOptions();
        refresh();
    }

    /// <summary>
    /// Method called by the Back button, changes the screen to OptionsHub screen
    /// </summary>
    public void Back()
    {
        GameStates.gameState = GameStates.GameState.OptionsHub;
    }


}
