using System;
using System.Collections.Generic;
using UnityEngine;

class Options
{
    public const string OPTIONS_FILE = "Nebula.options";

    public float ingameInterfaceAlpha = 0.7f;
    public float healthBarAlpha = 0.7f;
    public int levelMaxAutosaves = 10;
    public bool levelStatic = false;
    public float volumeEffects = 50;
    public float volumeMusic = 50;
    public float keyActivationThreshold = 0.5f;
    public float keyDeadZone = 0.1f;
    public bool keyXboxNames = true;
    public float cameraEdgeBufferSize = 10f;
    public float cameraZoomSpeed = 0.02f;
    
    public bool fullScreen
    {
        get
        {
            return Screen.fullScreen;
        }
        set
        {
            Screen.fullScreen = value;
        }
    }

    public Resolution resolution
    {
        get
        {
            return Screen.currentResolution;
        }
        set
        {
            Screen.SetResolution(value.width, value.height, Screen.fullScreen);
        }
    }



    private Options()
    {
        try
        {
            loadOptions();
        }
        catch
        {
            Debug.Log("Error loading options, setting default options.");
            setDefaultOptions();
        }
    }

    public void setDefaultOptions()
    {
        ingameInterfaceAlpha = 0.7f;
        healthBarAlpha = 0.7f;
        levelMaxAutosaves = 10;
        levelStatic = false;
        volumeEffects = 50f;
        volumeMusic = 50f;
        keyActivationThreshold = 0.5f;
        keyDeadZone = 0.1f;
        keyXboxNames = true;
        cameraEdgeBufferSize = 10f;
        cameraZoomSpeed = 0.02f;
        fullScreen = false;
        //resolution
    }

    public void loadOptions()
    {
        loadOptions(OPTIONS_FILE);
    }

    public void loadOptions(string fileName)
    {
        System.IO.StreamReader file = null;
        try
        {
            file = new System.IO.StreamReader(fileName);

            ingameInterfaceAlpha = System.Convert.ToSingle(file.ReadLine());
            healthBarAlpha = System.Convert.ToSingle(file.ReadLine());
            levelMaxAutosaves = System.Convert.ToInt32(file.ReadLine());
            levelStatic = System.Convert.ToBoolean(file.ReadLine());
            volumeEffects = System.Convert.ToSingle(file.ReadLine());
            volumeMusic = System.Convert.ToSingle(file.ReadLine());
            keyActivationThreshold = System.Convert.ToSingle(file.ReadLine());
            keyDeadZone = System.Convert.ToSingle(file.ReadLine());
            keyXboxNames = System.Convert.ToBoolean(file.ReadLine());
            cameraEdgeBufferSize = System.Convert.ToSingle(file.ReadLine());
            cameraZoomSpeed = System.Convert.ToSingle(file.ReadLine());
            fullScreen = System.Convert.ToBoolean(file.ReadLine());
            //resolution

            file.Close();
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Probelm loading controls from file.", e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void saveOptions()
    {
        saveOptions(OPTIONS_FILE);
    }

    public void saveOptions(string fileName)
    {
        System.IO.StreamWriter file = null;
        try
        {
            file = new System.IO.StreamWriter(fileName, false);

            file.WriteLine(ingameInterfaceAlpha);
            file.WriteLine(healthBarAlpha);
            file.WriteLine(levelMaxAutosaves);
            file.WriteLine(levelStatic);
            file.WriteLine(volumeEffects);
            file.WriteLine(volumeMusic);
            file.WriteLine(keyActivationThreshold);
            file.WriteLine(keyDeadZone);
            file.WriteLine(keyXboxNames);
            file.WriteLine(cameraEdgeBufferSize);
            file.WriteLine(cameraZoomSpeed);
            file.WriteLine(fullScreen);
            //resolution

            file.Close();
        }
        catch (System.Exception e)
        {
            throw new System.Exception("Probelm saving controls to file.", e);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    private static Options singleton;
    public static Options get()
    {
        if (singleton == null)
        {
            singleton = new Options();
        }
        return singleton;
    }
}
