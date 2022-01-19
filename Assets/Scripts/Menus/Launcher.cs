using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    public MenuTweening menuTweening;
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
        MainMenuAudio mainMenuAudio = GameObject.Find("MainMenuAudio").GetComponent<MainMenuAudio>();
        mainMenuAudio.PlayConnectionSoundEffect();
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        menuTweening.ConnectingOverlayToConnectionMenu();
    }
    public void SetNickname()
    {
        PhotonNetwork.NickName = nicknameInputField.text;
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
        if(PhotonNetwork.IsMasterClient)
        {
            playerOneName.text = PhotonNetwork.NickName;
        }
        else
        {
            playerOneName.text = PhotonNetwork.MasterClient.NickName;
            playerTwoName.text = PhotonNetwork.NickName;
        }
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        startGameButton.SetActive(PhotonNetwork.IsMasterClient && PhotonNetwork.CountOfPlayers > 1);
        menuTweening.TweenCreateRoomToRoomMenu();
        MainMenuAudio mainMenuAudio = GameObject.Find("MainMenuAudio").GetComponent<MainMenuAudio>();
        mainMenuAudio.PlayJoinRoomSoundEffect();
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
        PhotonNetwork.LoadLevel("StartGame");
    }
}