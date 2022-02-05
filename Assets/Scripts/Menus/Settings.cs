using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private FMOD.Studio.VCA vca;
    public bool fullscreenStatus = false;
    public Toggle fullscreenToggle;
    public void Awake()
    {
        vca = FMODUnity.RuntimeManager.GetVCA("vca:/Main");
        fullscreenStatus = Screen.fullScreen;
        fullscreenToggle.isOn = fullscreenStatus;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenStatus = Screen.fullScreen;
    }
    public void SetVolume(float setVolume)
    {
        vca.setVolume(setVolume);
    }

    public void SetSensitivity(float setSens)
    {
        GameObject.Find("StarTracker").GetComponent<StarTracker>().playerSensitivity = setSens;
    }
}
