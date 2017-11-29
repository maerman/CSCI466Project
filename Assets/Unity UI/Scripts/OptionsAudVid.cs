using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionsAudVid : MonoBehaviour
{
    //initilizedInEditor
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
        string resolutionString = resolution.options[resolution.value].text;

        try
        {
            char[] seperators = new char[1];
            seperators[0] = ' ';
            string[] parts = resolutionString.Split(seperators);

            Resolution selectedResolution = new UnityEngine.Resolution();
            selectedResolution.width = System.Convert.ToInt32(parts[0]);
            selectedResolution.height = System.Convert.ToInt32(parts[2]);

            options.resolution = selectedResolution;

            refresh();
        }
        catch
        {
            throw new System.Exception("Problem loading resolution: " + resolutionString);
        }
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
