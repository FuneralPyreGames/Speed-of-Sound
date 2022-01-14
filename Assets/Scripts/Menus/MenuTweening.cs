using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTweening : MonoBehaviour
{
    private MainMenuAudio mainMenuAudio;
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
        mainMenuAudio = GameObject.Find("MainMenuAudio").GetComponent<MainMenuAudio>();
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveY(onlineNotice, 2000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToHelp()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalY(helpOverlay, 0, .35f);
    }
    public void TweenHelpToMainMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(helpOverlay, 1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToSettings()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalX(settingsOverlay, 0, .35f);
    }
    public void TweenSettingsToMainMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalX(settingsOverlay, 1600, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void TweenMainMenuToCredits()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        LeanTween.moveLocalY(creditsOverlay, 0, .35f);
    }
    public void TweenCreditsToMainMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(creditsOverlay, -1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void QuitGame()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        Application.Quit();
    }
    public void TweenMainMenuToConnectingOverlay()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(connectingOverlay, 0, .35f);
        LeanTween.moveLocalX(mainMenuLeft, -350, .35f);
        LeanTween.moveLocalX(mainMenuRight, 175, .35f);
        LeanTween.moveLocalY(mainMenuTop, 700, .35f);
        launcher.Connect();
    }
    public void ConnectingOverlayToConnectionMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(connectingOverlay, 750, .35f);
        LeanTween.moveLocalY(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToMainMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(connectionMenu, -1000, .35f);
        LeanTween.moveLocalX(mainMenuLeft, 0, .35f);
        LeanTween.moveLocalX(mainMenuRight, 0, .35f);
        LeanTween.moveLocalY(mainMenuTop, 0, .35f);
    }
    public void ConnectionMenuToFindRoom()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        launcher.SetNickname();
        LeanTween.moveLocalX(findRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void FindRoomToConnectionMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalX(findRoom, 2000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
    public void ConnectionMenuToCreateRoom()
    {
        mainMenuAudio.PlayButtonSoundEffect();
        launcher.SetNickname();
        LeanTween.moveLocalX(createRoom, 0, .35f);
        LeanTween.moveLocalX(connectionMenu, -1600, .35f);
    }
    public void CreateRoomToConnectionMenu()
    {
        mainMenuAudio.PlayButtonSoundEffect();
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
        mainMenuAudio.PlayButtonSoundEffect();
        LeanTween.moveLocalY(roomMenu, -1000, .35f);
        LeanTween.moveLocalX(connectionMenu, 0, .35f);
    }
}