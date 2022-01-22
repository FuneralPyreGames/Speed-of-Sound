using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTweening : MonoBehaviour
{
    public GameObject onlineNotice;
    public GameObject mainMenuRight;
    public GameObject mainMenuLeft;
    public GameObject mainMenuTop;
    public GameObject mainMenu;
    public GameObject connectingOverlay;
    public GameObject connectionMenu;
    public GameObject findRoom;
    public GameObject createRoom;
    public GameObject helpOverlay;
    public GameObject settingsOverlay;
    public GameObject creditsOverlay;
    public GameObject roomMenu;
    public Launcher launcher;
    private FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent;
    
    public void Awake()
    {
        LeanTween.moveLocalX(onlineNotice, 0, 1f);
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }
    public void TweenOnlineNoticeToMainMenu()
    {
        instance.setParameterByName("CurrentSection", 2);
        LeanTween.moveY(onlineNotice, 2000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToHelp()
    {
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalY(helpOverlay, 0, .35f);
    }
    public void TweenHelpToMainMenu()
    {
        LeanTween.moveLocalY(helpOverlay, 1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToSettings()
    {
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalX(settingsOverlay, 0, .35f);
    }
    public void TweenSettingsToMainMenu()
    {
        LeanTween.moveLocalX(settingsOverlay, 1600, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToCredits()
    {
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalY(creditsOverlay, 0, .35f);
    }
    public void TweenCreditsToMainMenu()
    {
        LeanTween.moveLocalY(creditsOverlay, -1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void TweenMainMenuToConnectingOverlay()
    {
        LeanTween.moveLocalY(connectingOverlay, 0, .35f);
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        launcher.Connect();
    }
    public void ConnectingOverlayToConnectionMenu()
    {
        instance.setParameterByName("CurrentSection", 3);
        LeanTween.moveLocalY(connectingOverlay, 750, .35f);
        LeanTween.moveLocalY(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToMainMenu()
    {
        instance.setParameterByName("CurrentSection", 2);
        LeanTween.moveLocalY(connectionMenu, -1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void ConnectionMenuToFindRoom()
    {
        instance.setParameterByName("CurrentSection", 4);
        launcher.SetNickname();
        LeanTween.moveLocalX(findRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void FindRoomToConnectionMenu()
    {
        instance.setParameterByName("CurrentSection", 3);
        LeanTween.moveLocalX(findRoom, 2000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToCreateRoom()
    {
        instance.setParameterByName("CurrentSection", 4);
        launcher.SetNickname();
        LeanTween.moveLocalX(createRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void CreateRoomToConnectionMenu()
    {
        instance.setParameterByName("CurrentSection", 3);
        LeanTween.moveLocalX(createRoom, 2000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
    public void TweenCreateRoomToRoomMenu()
    {
        instance.setParameterByName("CurrentSection", 5);
        LeanTween.moveLocalX(createRoom, 2000, .35f);
        LeanTween.moveLocalY(roomMenu, 0, .35f);
    }
    public void TweenFindRoomToRoomMenu()
    {
        instance.setParameterByName("CurrentSection", 5);
        LeanTween.moveLocalX(findRoom, 2000, .35f);
        LeanTween.moveLocalY(roomMenu, 0, .35f);
    }
    public void TweenRoomMenuToConnectionMenu()
    {
        instance.setParameterByName("CurrentSection", 3);
        LeanTween.moveLocalY(roomMenu, -1000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }

    public void MakeCurrentSection6()
    {
        instance.setParameterByName("CurrentSection", 6);
    }
}