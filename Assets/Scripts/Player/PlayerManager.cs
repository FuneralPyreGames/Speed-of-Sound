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
    private SpawnPoint[] spawnPoints;
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
        spawnPoints = spawnManager.GetSpawnPoint();
        spawnPoint = PhotonNetwork.IsMasterClient switch
        {
            true => spawnPoints[0].transform,
            false => spawnPoints[1].transform
        };
        playerController = PhotonNetwork.Instantiate("PlayerController", spawnPoint.position, spawnPoint.rotation, 0, new object[] {pv.ViewID});
    }
    public void BackToSpawn()
    {
        if(spawnPoint == true)
        {
            playerController.transform.position = spawnPoints[0].transform.position;
            playerController.transform.rotation = spawnPoints[0].transform.rotation;
        }
        else if(spawnPoint == false)
        {
            playerController.transform.position = spawnPoints[1].transform.position;
            playerController.transform.rotation = spawnPoints[1].transform.rotation;
        }
    }
}