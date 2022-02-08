using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using Photon.Pun;

public class ChampionshipWinScreen : MonoBehaviour
{
    public GameObject backToLevelSelectButton;
    public GameObject menuText;
    public FMODUnity.EventReference fmodEvent;
    private FMOD.Studio.EventInstance instance;
    private void Awake()
    {
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
                instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
                instance.start();
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
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
        LeanTween.moveLocalY(menuText, 2000, .35f);
        LeanTween.moveLocalX(backToLevelSelectButton, 2000, .35f);
        PhotonNetwork.LoadLevel("LevelSelectMenu");
    }
}
