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
    public GameObject jetDivision;
    public GameObject rocketDivision;
    public GameObject bonusContent;
    public TMP_Text jetDivisionUnlockCount, rocketDivisionUnlockCount, soundChampionshipUnlockCount, starCount, level9UnlockStatus, level10UnlockStatus;
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
    public void OpenJetDivision()
    {
        if(starCountInt<8)
        {
            return;
        }
        level4StarCount.text = starTracker.level4Stars.ToString() + " Stars";
        level5StarCount.text = starTracker.level5Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(jetDivision, 0, .35f);
    }
    public void CloseJetDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(jetDivision, -2000, .35f);
    }
    public void LoadLevel4()
    {
        PhotonNetwork.LoadLevel("Level 4 - Into The Woods");
    }
    public void LoadLevel5()
    {
        PhotonNetwork.LoadLevel("Level 5 - Beyond The Earth");
    }
    public void OpenRocketDivision()
    {
        if (starCountInt < 16)
        {
            return;
        }
        level6StarCount.text = starTracker.level6Stars.ToString() + " Stars";
        level7StarCount.text = starTracker.level7Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(rocketDivision, 0, .35f);
    }
    public void CloseRocketDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(rocketDivision, -2000, .35f);
    }
    public void LoadLevel6()
    {
        PhotonNetwork.LoadLevel("Level 6 - Super Parkour Galaxy");
    }

    public void LoadLevel7()
    {
        PhotonNetwork.LoadLevel("Level 7 - Up In Space") ;
    }

    public void OpenBonusContent()
    {
        level9StarCount.text = starTracker.level9Stars.ToString() + " Stars";
        level10StarCount.text = starTracker.level10Stars.ToString() + " Stars";
        if (starCountInt >= 25)
        {
            level9UnlockStatus.text = "Unlocked!";
            if (starCountInt >= 45)
            {
                level10UnlockStatus.text = "Unlocked!";
            }
        }
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(bonusContent, 0, .35f);
    }
    public void CloseBonusContent()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(rocketDivision, -2000, .35f);
    }

    public void LoadLevel9()
    {
        if (starCountInt < 25)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Level 9 - Test Level");
    }

    public void LoadLevel10()
    {
        if (starCountInt < 45)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Level 10 - The Storm");
    }
}
