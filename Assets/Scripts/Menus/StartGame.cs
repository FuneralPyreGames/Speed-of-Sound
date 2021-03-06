using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviourPunCallbacks
{
    public GameObject startGame;
    public FMODUnity.EventReference fmodEvent;
    private FMOD.Studio.EventInstance instance;
    //public PhotonView photonView;
    private void Awake()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
            instance.start();
            LeanTween.moveLocalY(startGame, 0, 1f);
        }
    }
    public void StartEasyGame()
    {
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
        LeanTween.moveLocalY(startGame, -2000, 1f);
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
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
        LeanTween.moveLocalY(startGame, -2000, 1f);
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
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
        LeanTween.moveLocalY(startGame, -2000, 1f);
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
    public void ExitGame()
    {
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
        photonView.RPC("RPC_Disconnect", RpcTarget.All);
    }
    [PunRPC]
    void RPC_Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Main Menu");
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        photonView.RPC("RPC_Disconnect", RpcTarget.All);
    }
}
