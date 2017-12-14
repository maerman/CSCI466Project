// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// OptionsAudVid is a MonoBehavior that controls the Audio menu and has methods
/// that are called when its input UI items are changed, which edit their corropsonding Options values
/// </summary>
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

    /// <summary>
    /// Set values in the menu to the values in Options
    /// </summary>
    private void refresh()
    {
        master.value = options.volumeMaster;
        soundEffects.value = options.volumeEffects;
        music.value = options.volumeMusic;
        fullscreen.isOn = options.fullScreen;
    }

    /// <summary>
    /// Method called by Unity shortly after this is created, refreshes this menu then 
    /// find all resolutions supported by the monitor and adds them to resolutions Dropdown
    /// </summary>
    void Start()
    {
        refresh();

        //find supported resolutions and current resolution
        Resolution[] supportedResolutions = Screen.resolutions;
        
        Resolution currentResolution = Screen.currentResolution;

        //Converts the resolutions to the format the Dropdown will take and finds where in the list
        //the current resolution is located
        int resolutionNumber = 0;
        Dropdown.OptionData[] resolutionOptions = new Dropdown.OptionData[supportedResolutions.Length];
        for (int i = 0; i < supportedResolutions.Length; i++)
        {
            //convert
            resolutionOptions[supportedResolutions.Length - i - 1] = new Dropdown.OptionData(supportedResolutions[i].ToString());

            //find current resolution number
            if (supportedResolutions[i].ToString().Equals(currentResolution.ToString()))
            {
                resolutionNumber = supportedResolutions.Length - i - 1;
            }
        }

        resolution.options = new List<Dropdown.OptionData>(resolutionOptions);
        //resolution.value = resolutionNumber; //causes Unity to crash, I have no idea why
    }

    private void OnEnable()
    {
        refresh();
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Options value and refresh this menu.
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
        //refresh();
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
