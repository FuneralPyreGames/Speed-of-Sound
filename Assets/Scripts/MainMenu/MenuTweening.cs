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
    public void Awake()
    {
        LeanTween.moveLocalX(onlineNotice, 0, 1f);
    }
    public void TweenOnlineNoticeToMainMenu()
    {
        LeanTween.moveY(onlineNotice, 2000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToHelp()
    {
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 250, .35f);
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
        LeanTween.moveLocalY(mainMenuTop, 250, .35f);
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
        LeanTween.moveLocalY(mainMenuTop, 250, .35f);
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
        LeanTween.moveLocalY(mainMenuTop, 250, .35f);
        launcher.Connect();
    }
    public void ConnectingOverlayToConnectionMenu()
    {
        LeanTween.moveLocalY(connectingOverlay, 750, .35f);
        LeanTween.moveLocalY(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToMainMenu()
    {
        LeanTween.moveLocalY(connectionMenu, -1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void ConnectionMenuToFindRoom()
    {
        launcher.SetNickname();
        LeanTween.moveLocalX(findRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void FindRoomToConnectionMenu()
    {
        LeanTween.moveLocalX(findRoom, 2000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToCreateRoom()
    {
        launcher.SetNickname();
        LeanTween.moveLocalX(createRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void CreateRoomToConnectionMenu()
    {
        LeanTween.moveLocalX(createRoom, 2000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
    public void TweenCreateRoomToRoomMenu()
    {
        LeanTween.moveLocalX(createRoom, 2000, .35f);
        LeanTween.moveLocalY(roomMenu, 0, .35f);
    }
    public void TweenFindRoomToRoomMenu()
    {
        LeanTween.moveLocalX(findRoom, 2000, .35f);
        LeanTween.moveLocalY(roomMenu, 0, .35f);
    }
    public void TweenRoomMenuToConnectionMenu()
    {
        LeanTween.moveLocalY(roomMenu, -1000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
}