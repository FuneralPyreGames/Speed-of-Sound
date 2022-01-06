using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviour
{
    public GameObject[] playerControllers;
    public PlayerController firstPlayerControllerComponents, secondPlayerControllerComponents, sortPlayerControllerComponents;
    public bool gameStarted = false;
    public bool playerOneExited, playerTwoExited;
    public int isLocalPlayer;
    public GameObject playerOneExitCheck, playerTwoExitCheck, playerOneLevelFinishUI, playerTwoLevelFinishUI;
    PhotonView PV;
    void Awake()
    {
        //The game controller has to get the photon view component to ensure that the photon view can make calls as needed
        PV = GetComponent<PhotonView>();
        //Upon awakening, the function GetPlayerControllers is called. This ensures that the GameController has the proper references to each player controller that it needs, so it knows that both have spawned in, and the game can start
        GetPlayerControllers();
    }
    void GetPlayerControllers()
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
    void StartCountdown()
    {
        if(isLocalPlayer == 0)
        {
            firstPlayerControllerComponents.BeginCountdown();
        }
        else if(isLocalPlayer == 1)
        {
            secondPlayerControllerComponents.BeginCountdown();
        }
    }
    public void StartGame()
    {
        gameStarted = true;
    }
    void Update()
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
            EndGame();
        }
    }
    public void PlayerOneExitedLevel()
    {
        PV.RPC("RPC_PlayerOneExitedLevel", RpcTarget.All);
    }
    public void PlayerTwoExitedLevel()
    {
        PV.RPC("RPC_PlayerTwoExitedLevel", RpcTarget.All);
    }
    public void EndGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            playerOneLevelFinishUI.SetActive(true);
            firstPlayerControllerComponents.StopTimer();
            firstPlayerControllerComponents.timerUI.SetActive(false);
        }
        if(!PhotonNetwork.IsMasterClient)
        {
            playerTwoLevelFinishUI.SetActive(true);
            secondPlayerControllerComponents.StopTimer();
            secondPlayerControllerComponents.timerUI.SetActive(false);
        }
    }
    [PunRPC]
    void RPC_PlayerOneExitedLevel()
    {
        playerOneExited = true;
        playerOneExitCheck.SetActive(true);
    }
    [PunRPC]
    void RPC_PlayerTwoExitedLevel()
    {
        playerTwoExited = true;
        playerTwoExitCheck.SetActive(true);
    }
    //This just ensures that GetPlayerControllers cannot run too many times
    IEnumerator WaitToRecurseGetPlayerControllers()
    {
        yield return new WaitForSeconds(1f);
        GetPlayerControllers();
    }
}