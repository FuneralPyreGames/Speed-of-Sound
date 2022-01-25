using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartGame : MonoBehaviour
{
    public GameObject startGame;
    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            LeanTween.moveLocalY(startGame, 0, 1f);
        }
    }
    public void StartEasyGame()
    {
        StarTracker starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
        starTracker.jetDivStarUnlockCount = 6;
        starTracker.rocketDivStarUnlockCount = 10;
        starTracker.soundChampionshipStarUnlockCount = 16;
        starTracker.testLevelStarUnlockCount = 20;
        starTracker.theStormStarUnlockCount = 35;
        starTracker.ResetStars();
        PhotonNetwork.LoadLevel("Intro");
    }
    public void StartHardGame()
    {
        StarTracker starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
        starTracker.jetDivStarUnlockCount = 8;
        starTracker.rocketDivStarUnlockCount = 16;
        starTracker.soundChampionshipStarUnlockCount = 24;
        starTracker.testLevelStarUnlockCount = 25;
        starTracker.theStormStarUnlockCount = 50;
        starTracker.ResetStars();
        PhotonNetwork.LoadLevel("Intro");
    }
    public void LoadGame()
    {
        StarTracker starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
        starTracker.LoadStars();
        if(starTracker.GetTotalStars() == 0)
        {
            return;
        }
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
}
