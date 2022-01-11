using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class BlockTrigger : MonoBehaviour
{
    public GameObject blockToMove;
    private PhotonView photonView;
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Block trigger entered");
        if (blockToMove.CompareTag("Player Two Block"))
        {
            LeanTween.moveLocalX(blockToMove, 500, 1f);
        }
        else
        {
            LeanTween.moveLocalX(blockToMove, -500, 1f);
        }
    }
}