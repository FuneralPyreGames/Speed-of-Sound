using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviourPunCallbacks
{
    public GameObject divisionChoice;
    [FormerlySerializedAs("racecarDivision")] public GameObject raceCarDivision;
    public GameObject jetDivision;
    public GameObject rocketDivision;
    public GameObject bonusContent;
    public GameObject soundChampionship;
    public TMP_Text jetDivisionUnlockCount, rocketDivisionUnlockCount, soundChampionshipUnlockCount, starCount, level9UnlockStatus, level10UnlockStatus;
    public TMP_Text level1StarCount, level2StarCount, level3StarCount, level4StarCount, level5StarCount, level6StarCount, level7StarCount, level8StarCount, level9StarCount, level10StarCount;
    StarTracker starTracker;
    public int starCountInt;

    public int jetDivStarUnlockCount = 7;

    public int rocketDivStarUnlockCount = 12;

    public int soundChampionshipStarUnlockCount = 20;

    public int testLevelStarUnlockCount = 25;

    public int theStormStarUnlockCount = 45;
    //public PhotonView photonView;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            starTracker = GameObject.Find("StarTracker").GetComponent<StarTracker>();
            starTracker.LoadStars();
            starCountInt = starTracker.GetTotalStars();
            jetDivStarUnlockCount = starTracker.jetDivStarUnlockCount;
            rocketDivStarUnlockCount = starTracker.rocketDivStarUnlockCount;
            soundChampionshipStarUnlockCount = starTracker.soundChampionshipStarUnlockCount;
            testLevelStarUnlockCount = starTracker.testLevelStarUnlockCount;
            theStormStarUnlockCount = starTracker.theStormStarUnlockCount;
            if(starCountInt >= jetDivStarUnlockCount)
            {
                jetDivisionUnlockCount.text = "Unlocked!";
            }
            else
            {
                jetDivisionUnlockCount.text = "Unlocks at " + jetDivStarUnlockCount.ToString() + " stars!";
            }
            if (starCountInt >= rocketDivStarUnlockCount)
            {
                rocketDivisionUnlockCount.text = "Unlocked!";
            }
            else
            {
                rocketDivisionUnlockCount.text = "Unlocks at " + rocketDivStarUnlockCount.ToString() + " stars!";
            }
            if (starCountInt >= soundChampionshipStarUnlockCount)
            {
                soundChampionshipUnlockCount.text = "Unlocked!";
            }
            else
            {
                soundChampionshipUnlockCount.text = "Unlocks at " + soundChampionshipStarUnlockCount.ToString() + " stars!";
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
        divisionChoice.GetComponent<CanvasGroup>().interactable = false;
        raceCarDivision.GetComponent<CanvasGroup>().interactable = true;
    }
    public void CloseRacecarDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(raceCarDivision, -2000, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = true;
        raceCarDivision.GetComponent<CanvasGroup>().interactable = false;
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
        if(starCountInt<jetDivStarUnlockCount)
        {
            return;
        }
        level4StarCount.text = starTracker.level4Stars.ToString() + " Stars";
        level5StarCount.text = starTracker.level5Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(jetDivision, 0, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = false;
        jetDivision.GetComponent<CanvasGroup>().interactable = true;
    }
    public void CloseJetDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(jetDivision, -2000, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = true;
        jetDivision.GetComponent<CanvasGroup>().interactable = false;
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
        if (starCountInt < rocketDivStarUnlockCount)
        {
            return;
        }
        level6StarCount.text = starTracker.level6Stars.ToString() + " Stars";
        level7StarCount.text = starTracker.level7Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(rocketDivision, 0, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = false;
        rocketDivision.GetComponent<CanvasGroup>().interactable = true;
    }
    public void CloseRocketDivision()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(rocketDivision, -2000, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = true;
        rocketDivision.GetComponent<CanvasGroup>().interactable = false;
    }
    public void LoadLevel6()
    {
        PhotonNetwork.LoadLevel("Level 6 - Super Parkour Galaxy");
    }

    public void LoadLevel7()
    {
        PhotonNetwork.LoadLevel("Level 7 - Up In Space");
    }

    public void OpenSoundChampionship()
    {
        if (starCountInt < soundChampionshipStarUnlockCount)
        {
            return;
        }

        level8StarCount.text = starTracker.level8Stars.ToString() + " Stars";
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(soundChampionship, 0, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = false;
        soundChampionship.GetComponent<CanvasGroup>().interactable = true;
    }

    public void CloseSoundChampionship()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(soundChampionship, -2000, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = true;
        soundChampionship.GetComponent<CanvasGroup>().interactable = false;
    }

    public void LoadLevel8()
    {
        PhotonNetwork.LoadLevel("Level 8 - The Championship");
    }

    public void OpenBonusContent()
    {
        level9StarCount.text = starTracker.level9Stars.ToString() + " Stars";
        level10StarCount.text = starTracker.level10Stars.ToString() + " Stars";
        if (starCountInt >= testLevelStarUnlockCount)
        {
            level9UnlockStatus.text = "Unlocked!";
        }
        else
        {
            level9UnlockStatus.text = "Unlocks at " + testLevelStarUnlockCount.ToString() + " stars!";
        }
        if (starCountInt >= theStormStarUnlockCount)
        {
            level10UnlockStatus.text = "Unlocked!";
        }
        else
        {
            level10UnlockStatus.text = "Unlocks at " + theStormStarUnlockCount.ToString() + " stars!";
        }
        LeanTween.moveLocalY(divisionChoice, 1500, .35f);
        LeanTween.moveLocalX(bonusContent, 0, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = false;
        bonusContent.GetComponent<CanvasGroup>().interactable = true;
    }
    public void CloseBonusContent()
    {
        LeanTween.moveLocalY(divisionChoice, 0, .35f);
        LeanTween.moveLocalX(bonusContent, -2000, .35f);
        divisionChoice.GetComponent<CanvasGroup>().interactable = true;
        bonusContent.GetComponent<CanvasGroup>().interactable = false;
    }

    public void LoadLevel9()
    {
        if (starCountInt < testLevelStarUnlockCount)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Level 9 - Test Level");
    }

    public void LoadLevel10()
    {
        if (starCountInt < theStormStarUnlockCount)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Level 10 - The Storm");
    }
    public void ExitGame()
    {
        photonView.RPC("RPC_Disconnect", RpcTarget.All);
    }
    [PunRPC]
    void RPC_Disconnect()
    {
        LeanTween.moveLocalY(divisionChoice, -2000f, 1f);
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Main Menu");
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        photonView.RPC("RPC_Disconnect", RpcTarget.All);
    }
}
