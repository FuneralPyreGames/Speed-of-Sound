using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BonusStar : MonoBehaviour
{
    private int bonusStarNumber;
    private void OnTriggerEnter(Collider other) 
    {
        //To begin, the bonus star script checks to see if the master client got the star, and if it doesn't, it uses a rpc call to ensure the master client does
        var playerController = other.gameObject.GetComponent<PlayerController>();
        if (PhotonNetwork.IsMasterClient)
        {
            playerController.SaveBonusStar(bonusStarNumber);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
            playerController.RPCToMainPlayer(bonusStarNumber);
        }
    }
}
