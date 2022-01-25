using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool fullscreenStatus = false;
    public Toggle fullscreenToggle;
    public void Awake()
    {
        fullscreenStatus = Screen.fullScreen;
        fullscreenToggle.isOn = fullscreenStatus;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreenStatus = Screen.fullScreen;
    }
}
