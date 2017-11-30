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
    public float volumeMaster
    {
        get
        {
            return AudioListener.volume;
        }
        set
        {
            AudioListener.volume = value;
        }
    }
    public float volumeEffects = 0.5f;
    public float volumeMusic = 0.5f;
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

    public void setResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void setResolution(string toString)
    {
        try
        {
            char[] seperators = new char[1];
            seperators[0] = ' ';
            string[] parts = toString.Split(seperators);

            setResolution(System.Convert.ToInt32(parts[0]), System.Convert.ToInt32(parts[2]));
        }
        catch
        {
            Debug.Log("Problem setting resolution from string: " + toString);
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
        volumeMaster = 1f;
        volumeEffects = 0.5f;
        volumeMusic = 0.5f;
        keyActivationThreshold = 0.5f;
        keyDeadZone = 0.1f;
        keyXboxNames = true;
        cameraEdgeBufferSize = 10f;
        cameraZoomSpeed = 0.02f;
        //use whatever fullscreen it started as
        //use whatever resolution it started as
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
            volumeMaster = System.Convert.ToSingle(file.ReadLine());
            volumeEffects = System.Convert.ToSingle(file.ReadLine());
            volumeMusic = System.Convert.ToSingle(file.ReadLine());
            keyActivationThreshold = System.Convert.ToSingle(file.ReadLine());
            keyDeadZone = System.Convert.ToSingle(file.ReadLine());
            keyXboxNames = System.Convert.ToBoolean(file.ReadLine());
            cameraEdgeBufferSize = System.Convert.ToSingle(file.ReadLine());
            cameraZoomSpeed = System.Convert.ToSingle(file.ReadLine());
            fullScreen = System.Convert.ToBoolean(file.ReadLine());
            setResolution(file.ReadLine());

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
            file.WriteLine(volumeMaster);
            file.WriteLine(volumeEffects);
            file.WriteLine(volumeMusic);
            file.WriteLine(keyActivationThreshold);
            file.WriteLine(keyDeadZone);
            file.WriteLine(keyXboxNames);
            file.WriteLine(cameraEdgeBufferSize);
            file.WriteLine(cameraZoomSpeed);
            file.WriteLine(fullScreen);
            file.WriteLine(resolution.ToString());

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
