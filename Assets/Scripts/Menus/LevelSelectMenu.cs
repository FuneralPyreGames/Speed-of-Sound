using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.Serialization;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject divisionChoice;
    [FormerlySerializedAs("racecarDivision")] public GameObject raceCarDivision;
    public TMP_Text jetDivisionUnlockCount, rocketDivisionUnlockCount, soundChampionshipUnlockCount, starCount;
    public TMP_Text level1StarCount, level2StarCount, level3StarCount, level4StarCount, level5StarCount, level6StarCount, level7StarCount, level8StarCount, level9StarCount, level10StarCount;
    StarTracker starTracker;
    public int starCountInt;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
            starTracker.LoadStars();
            starCountInt = starTracker.GetTotalStars();
            if(starCountInt >= 8)
            {
                jetDivisionUnlockCount.text = "Unlocked!";
                if (starCountInt >= 16)
                {
                    rocketDivisionUnlockCount.text = "Unlocked!";
                    if (starCountInt >= 24)
                    {
                        soundChampionshipUnlockCount.text = "Unlocked!";
                    }
                }
            }
            starCount.text = starCountInt.ToString() + " Stars";
            LeanTween.moveLocalY(divisionChoice, 0, 1f);
        }
    }
    public void OpenRacecarDivision()
    {
        level1StarCount.text = starTracker.level1Stars.ToString() + " Stars";
        level2StarCount.text = starTracker.level2Stars.ToString() + " Stars";
        level3StarCount.text = starTracker.level3Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(raceCarDivision, 0, .35f);
    }
    public void CloseRacecarDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(raceCarDivision, -2000, .35f);
    }
    public void LoadLevel1()
    {
        PhotonNetwork.LoadLevel("Level 1 - Starting Out");
    }
    public void LoadLevel2()
    {
        PhotonNetwork.LoadLevel("Level 2 - Through The Walls");
    }
    public void LoadLevel3()
    {
        PhotonNetwork.LoadLevel("Level 3 - Relay Race");
    }
}
