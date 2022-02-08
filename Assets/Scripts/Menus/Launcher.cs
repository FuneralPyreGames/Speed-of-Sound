using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    public MenuTweening menuTweening;
    public PhotonView bhotonView;
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text roomNameText;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private Transform roomListContent;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private TMP_Text playerOneName;
    [SerializeField] private TMP_Text playerTwoName;
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        menuTweening.ConnectionMenuToMainMenu();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Main Menu/Connected");
        menuTweening.ConnectingOverlayToConnectionMenu();
    }
    public void SetNickname()
    {
        PhotonNetwork.NickName = nicknameInputField.text;
        if (nicknameInputField.text == "For Testing Purposes")
        {
            var starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
            starTracker.level1Stars = 5;
            starTracker.level2Stars = 5;
            starTracker.level3Stars = 5;
            starTracker.level4Stars = 5;
            starTracker.level5Stars = 5;
            starTracker.level6Stars = 5;
            starTracker.level7Stars = 5;
            starTracker.level8Stars = 1;
            starTracker.level9Stars = 5;
            starTracker.level10Stars = 5;
            starTracker.bonusStars = 4;
            starTracker.bonusStar1Got = true;
            starTracker.bonusStar2Got = true;
            starTracker.bonusStar3Got = true;
            starTracker.bonusStar4Got = true;
            starTracker.SaveStars();
        }
    }
    public void CreateRoom()
    {
        var roomOptions = new RoomOptions
        {
            MaxPlayers = 2
        };
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        menuTweening.TweenRoomMenuToConnectionMenu();
    }
    public override void OnJoinedRoom()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Main Menu/Room Joined");
        if(PhotonNetwork.IsMasterClient)
        {
            playerOneName.text = PhotonNetwork.NickName;
        }
        else
        {
            playerOneName.text = PhotonNetwork.MasterClient.NickName;
            playerTwoName.text = PhotonNetwork.NickName;
            menuTweening.MakeCurrentSection6();
        }
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        startGameButton.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CountOfPlayers > 1);
        menuTweening.TweenCreateRoomToRoomMenu();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(newPlayer.IsMasterClient)
        {
            playerOneName.text = newPlayer.NickName;
        }
        else
        {
            playerTwoName.text = newPlayer.NickName;
            startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        }
        menuTweening.MakeCurrentSection6();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i= 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        menuTweening.TweenFindRoomToRoomMenu();        
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            menuTweening.StopMainMenuSong();
            bhotonView.RPC("RPC_StopMainMenuSong", RpcTarget.Others);
        }
        menuTweening.TweenRoomMenuToNothing();
        PhotonNetwork.LoadLevel("StartGame");
    }

    [PunRPC]
    private void RPC_StopMainMenuSong()
    {
        menuTweening.StopMainMenuSong();
    }
}