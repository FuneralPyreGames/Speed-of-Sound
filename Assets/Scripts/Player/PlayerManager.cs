using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject playerController;
    SpawnManager spawnManager;
    Transform spawnpoint;
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
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        SpawnPoint[] spawnPoints = spawnManager.GetSpawnPoint();
        if(PhotonNetwork.IsMasterClient)
        {
            spawnpoint = spawnPoints[0].transform;
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            spawnpoint = spawnPoints[1].transform;
        }
        playerController = PhotonNetwork.Instantiate("PlayerController", spawnpoint.position, spawnpoint.rotation);
    }
}
