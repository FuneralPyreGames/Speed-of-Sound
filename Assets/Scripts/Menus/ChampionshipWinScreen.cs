using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChampionshipWinScreen : MonoBehaviour
{
    public GameObject backToLevelSelectButton;
    public GameObject menuText;
    private void Awake()
    {
        LeanTween.moveLocalY(menuText, -412, .35f);
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
                backToLevelSelectButton.SetActive(true);
                LeanTween.moveLocalX(backToLevelSelectButton, 0, .35f);
                break;
            case false:
                backToLevelSelectButton.SetActive(false);
                break;
        }
    }

    public void GoBackToLevelSelect()
    {
        LeanTween.moveLocalY(menuText, 2000, .35f);
        LeanTween.moveLocalX(backToLevelSelectButton, 2000, .35f);
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
}
