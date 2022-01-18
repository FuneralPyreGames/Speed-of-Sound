using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChampionshipWinScreen : MonoBehaviour
{
    public GameObject backToLevelSelectButton;
    private void Awake()
    {
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
                backToLevelSelectButton.SetActive(true);
                break;
            case false:
                backToLevelSelectButton.SetActive(false);
                break;
        }
    }

    public void GoBackToLevelSelect()
    {
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
}
