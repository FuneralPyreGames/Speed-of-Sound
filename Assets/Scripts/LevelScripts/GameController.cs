using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class GameController : MonoBehaviour
{
    public int currentLevel;
    public GameObject[] playerControllers;
    public PlayerController firstPlayerControllerComponents, secondPlayerControllerComponents, sortPlayerControllerComponents;
    public bool gameStarted = false;
    public bool playerOneExited, playerTwoExited;
    public int isLocalPlayer;
    public GameObject playerOneExitCheck, playerTwoExitCheck, playerOneLevelFinishUI, playerTwoLevelFinishUI;
    private string timerString;
    private string rankEarned;
    private int secondsToComplete;
    private int starsEarned;
    private StarTracker starTracker;
    public TMP_Text playerOneEndLevelTimerString, playerOneEndLevelRankString, playerOneEndLevelStarsString, playerTwoEndLevelTimerString, playerTwoEndLevelRankString, playerTwoEndLevelStarsString;
    [Header("Times")]
    public int platinumTime;
    public int goldTime;
    public int silverTime;
    public int bronzeTime;
    public int participationTime;
    private PhotonView pv;
    private void Awake()
    {
        //The game controller has to get the photon view component to ensure that the photon view can make calls as needed
        pv = GetComponent<PhotonView>();
        //Upon awakening, the function GetPlayerControllers is called. This ensures that the GameController has the proper references to each player controller that it needs, so it knows that both have spawned in, and the game can start
        GetPlayerControllers();
    }
    private void GetPlayerControllers()
    {
        //This code makes an array of every game object with the tag player controller. Then, it checks if there is an object in array positions 0 and 1. If there is no object, the function calls itself again, ensuring the code after it will only be called once the code has both player controllers
        playerControllers = GameObject.FindGameObjectsWithTag("Player Controller");
        if(playerControllers.Length != 2)
        {
            StartCoroutine(WaitToRecurseGetPlayerControllers());
            return;
        }
        if (playerControllers[0] == null)
        {
            StartCoroutine(WaitToRecurseGetPlayerControllers());
            return;
        }
        if (playerControllers[1] == null)
        {
            StartCoroutine(WaitToRecurseGetPlayerControllers());
            return;
        }
        //Now, the script gets the components of both player controllers, and stores them inside an array of player controller components.
        firstPlayerControllerComponents = playerControllers[0].GetComponent<PlayerController>();
        secondPlayerControllerComponents = playerControllers[1].GetComponent<PlayerController>();
        //Now, the script checks which player is actually the master player, and resorts the order of the player who is not the master player, as their components will be swapped!
        if(!PhotonNetwork.IsMasterClient)
        {
            sortPlayerControllerComponents = secondPlayerControllerComponents;
            secondPlayerControllerComponents = firstPlayerControllerComponents;
            firstPlayerControllerComponents = sortPlayerControllerComponents;
            isLocalPlayer = 1;
            secondPlayerControllerComponents.amMasterPlayer = false;
        }    
        else if(PhotonNetwork.IsMasterClient)
        {
            sortPlayerControllerComponents = secondPlayerControllerComponents;
            sortPlayerControllerComponents = firstPlayerControllerComponents;
            sortPlayerControllerComponents = firstPlayerControllerComponents;
            isLocalPlayer = 0;
            firstPlayerControllerComponents.amMasterPlayer = true;
        }
        //Now we can start the countdown!
        StartCountdown();
    }
    private void StartCountdown()
    {
        switch (isLocalPlayer)
        {
            case 0:
                firstPlayerControllerComponents.BeginCountdown();
                break;
            case 1:
                secondPlayerControllerComponents.BeginCountdown();
                break;
        }
    }
    public void StartGame()
    {
        gameStarted = true;
    }
    private void Update()
    {
        //Update begins by ensuring that the game has started. If it hasn't, the function will return to ensure that the code inside will not run
        if (gameStarted == false)
        {
            return;
        }
        //If player one or player two exit the level, the game controller will mark it, and then make a call over the network to let the other player know you have exited the level
        if(firstPlayerControllerComponents.isLevelExited && !playerOneExited)
        {
            Debug.Log("Player one exited level");
            PlayerOneExitedLevel();
        }
        if(secondPlayerControllerComponents.isLevelExited && !playerTwoExited)
        {
            Debug.Log("Player two exited level");
            PlayerTwoExitedLevel();
        }
        if (playerOneExited && playerTwoExited)
        {
            CalculateStars();
        }
    }
    private void PlayerOneExitedLevel()
    {
        pv.RPC("RPC_PlayerOneExitedLevel", RpcTarget.All);
    }
    private void PlayerTwoExitedLevel()
    {
        pv.RPC("RPC_PlayerTwoExitedLevel", RpcTarget.All);
    }
    private void CalculateStars()
    {
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
            {
                firstPlayerControllerComponents.StopTimer();
                secondsToComplete = firstPlayerControllerComponents.totalSeconds;
                timerString = firstPlayerControllerComponents.timePlayingStr;
                if(secondsToComplete - platinumTime <= 0)
                {
                    starsEarned = 5;
                    rankEarned = "Platinum";
                }
                else if(secondsToComplete - goldTime <= 0)
                {
                    starsEarned = 4;
                    rankEarned = "Gold";
                }
                else if(secondsToComplete - silverTime <= 0)
                {
                    starsEarned = 3;
                    rankEarned = "Silver";
                }
                else if(secondsToComplete - bronzeTime <= 0)
                {
                    starsEarned = 2;
                    rankEarned = "Bronze";
                }
                else if(secondsToComplete - participationTime <= 0)
                {
                    starsEarned = 1;
                    rankEarned = "Participation";
                }
                else
                {
                    starsEarned = 0;
                    rankEarned = "Try Again";
                }
                pv.RPC("RPC_CommunicateTime", RpcTarget.All, rankEarned, timerString, starsEarned);
                break;
            }
            case false:
                secondPlayerControllerComponents.StopTimer();
                break;
        }
    }
    private void EndGame()
    {
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
                playerOneEndLevelTimerString.text = "Time- " + timerString;
                playerOneEndLevelStarsString.text = "Stars- " + starsEarned.ToString() + " Stars";
                playerOneEndLevelRankString.text = "Rank- " + rankEarned;
                playerOneLevelFinishUI.SetActive(true);
                TrackStars();
                break;
            case false:
                playerTwoEndLevelTimerString.text = "Time- " + timerString;
                playerTwoEndLevelStarsString.text = "Stars- " + starsEarned.ToString() + " Stars";
                playerTwoEndLevelRankString.text = "Rank- " + rankEarned;
                playerTwoLevelFinishUI.SetActive(true);
                break;
        }
    }
    private void TrackStars()
    {
        starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
        switch(currentLevel)
        {
            case 1:
                if(starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level1Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 2:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level2Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 3:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level3Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 4:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level4Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 5:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level5Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 6:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level6Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 7:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level7Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 8:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level8Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 9:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level9Stars = starsEarned;
                starTracker.SaveStars();
                return;
            case 10:
                if (starsEarned < starTracker.level1Stars)
                {
                    return;
                }
                starTracker.level10Stars = starsEarned;
                starTracker.SaveStars();
                return;
        }
    }
    [PunRPC]
    private void RPC_CommunicateTime(string rank, string timer, int stars)
    {
        rankEarned = rank;
        timerString = timer;
        starsEarned = stars;
        EndGame();
    }
    [PunRPC]
    private void RPC_PlayerOneExitedLevel()
    {
        playerOneExited = true;
        playerOneExitCheck.SetActive(true);
    }
    [PunRPC]
    private void RPC_PlayerTwoExitedLevel()
    {
        playerTwoExited = true;
        playerTwoExitCheck.SetActive(true);
    }
    //This just ensures that GetPlayerControllers cannot run too many times
    private IEnumerator WaitToRecurseGetPlayerControllers()
    {
        yield return new WaitForSeconds(1f);
        GetPlayerControllers();
    }
    public void ExitLevel()
    {
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
}