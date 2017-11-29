using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public void ResetButton()
    {
        options.setDefaultOptions();
        refresh();
    }

    public void Back()
    {
        GameStates.gameState = GameStates.GameState.OptionsHub;
    }


}
