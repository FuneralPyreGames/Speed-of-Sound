using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BonusStar : MonoBehaviour
{
    public int bonusStarNumber;
    public PlayerController playerController;
    private void OnTriggerEnter(Collider collision)
    {
        //To begin, the bonus star script checks to see if the master client got the star, and if it doesn't, it uses a rpc call to ensure the master client does
        if (!collision.gameObject.CompareTag("Player Controller"))
        {
            return;
        }
        playerController = collision.gameObject.GetComponent<PlayerController>();
        Debug.Log("Player Controller");
        Debug.Log("Is Master Client= " + PhotonNetwork.IsMasterClient);
        switch (PhotonNetwork.IsMasterClient)
        {
            case true:
                playerController.SaveBonusStar(bonusStarNumber);
                break;
            case false:
                playerController.RPCToMainPlayer(bonusStarNumber);
                break;
        }
    }
}
