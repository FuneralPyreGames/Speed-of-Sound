using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerManager : MonoBehaviour
{
    private PhotonView pv;
    private GameObject playerController;
    private SpawnManager spawnManager;
    private Transform spawnPoint;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if(pv.IsMine)
        {
            CreateController();
        }
    }
    private void CreateController()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        var spawnPoints = spawnManager.GetSpawnPoint();
        spawnPoint = PhotonNetwork.IsMasterClient switch
        {
            true => spawnPoints[0].transform,
            false => spawnPoints[1].transform
        };
        playerController = PhotonNetwork.Instantiate("PlayerController", spawnPoint.position, spawnPoint.rotation);
    }
}