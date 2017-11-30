using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionsAudVid : MonoBehaviour
{
    //initilizedInEditor
    public Slider master;
    public Slider soundEffects;
    public Slider music;
    public Dropdown resolution;
    public Toggle fullscreen;

    private Options options
    {
        get
        {
            return Options.get();
        }
    }

    private void refresh()
    {
        master.value = options.volumeMaster;
        soundEffects.value = options.volumeEffects;
        music.value = options.volumeMusic;
        fullscreen.isOn = options.fullScreen;
    }

    void Start()
    {
        refresh();

        Resolution[] supportedResolutions = Screen.resolutions;
        Dropdown.OptionData[] resolutionOptions = new Dropdown.OptionData[supportedResolutions.Length];
        Resolution currentResolution = Screen.currentResolution;
        int resolutionNumber = 0;

        for (int i = 0; i < supportedResolutions.Length; i++)
        {
            resolutionOptions[supportedResolutions.Length - i - 1] = new Dropdown.OptionData(supportedResolutions[i].ToString());

            if (supportedResolutions[i].ToString().Equals(currentResolution.ToString()))
            {
                resolutionNumber = supportedResolutions.Length - i - 1;
            }
        }

        resolution.options = new List<Dropdown.OptionData>(resolutionOptions);
        //resolution.value = resolutionNumber; //causes unity to crash, I have no idea why
    }

    private void OnEnable()
    {
        refresh();
    }

    public void MasterVolume()
    {
        options.volumeMaster = master.value;
        refresh();
    }

    public void SoundEffectVolume()
    {
        options.volumeEffects = soundEffects.value;
        refresh();
    }

    public void MusicVolume()
    {
        options.volumeMusic = music.value;
        refresh();
    }

    public void Resolution()
    {
        options.setResolution(resolution.options[resolution.value].text);
        refresh();
    }

    public void FullScreen()
    {
        options.fullScreen = fullscreen.isOn;
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
