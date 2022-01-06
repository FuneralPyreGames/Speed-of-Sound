using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject playerController;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }
    void CreateController()
    {
        playerController = PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
    }
}
