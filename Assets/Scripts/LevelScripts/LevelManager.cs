using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelManager : MonoBehaviour
{
    public GameObject gameController;
    void Awake()
    {
        PhotonNetwork.Instantiate(("PlayerManager"), Vector3.zero, Quaternion.identity);
        gameController.SetActive(true);
    }
}
