using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject divisionChoice;
    public GameObject racecarDivision;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            LeanTween.moveLocalY(divisionChoice, 0, 1f);
        }
    }
    public void OpenRacecarDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(racecarDivision, 0, .35f);
    }
    public void CloseRacecarDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(racecarDivision, -2000, .35f);
    }
    public void LoadLevel1()
    {
        PhotonNetwork.LoadLevel("Level 1 - Starting Out");
    }
    public void LoadLevel2()
    {
        PhotonNetwork.LoadLevel("Level 2 - Through The Walls");
    }
}
