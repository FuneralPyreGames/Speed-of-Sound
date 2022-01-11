using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BlockTrigger : MonoBehaviour
{
    public GameObject blockToMove;
    [SerializeField] private PhotonView photonView;
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (blockToMove.CompareTag("Player Two Block"))
            {
                var position = blockToMove.gameObject.transform.position;
                position = new Vector3(500, position.y,
                    position.z);
                blockToMove.gameObject.transform.position = position;
                //LeanTween.moveLocalX(blockToMove, 500, 1f);
            }
            else if (blockToMove.CompareTag("Player One Block"))
            {
                var position = blockToMove.gameObject.transform.position;
                position = new Vector3(-500, position.y,
                    position.z);
                blockToMove.gameObject.transform.position = position;
                //LeanTween.moveLocalX(blockToMove, -500, 1f);
            }
        }
        else
        {
            photonView.RPC("RPC_MoveBlock", RpcTarget.Others, blockToMove.transform.position, blockToMove.gameObject.tag);
        }
    }
    [PunRPC]
    private void RPC_MoveBlock(Vector3 blockToMoveRPC, string blockToMoveTag)
    {
        if (blockToMoveTag == "Player Two Block")
        {
            var position = blockToMoveRPC;
            position = new Vector3(500, position.y,
                position.z);
            blockToMove.transform.position = position;
            //LeanTween.moveLocalX(blockToMove, 500, 1f);
        }
        else if (blockToMoveTag == "Player One Block")
        {
            var position = blockToMoveRPC;
            position = new Vector3(-500, position.y,
                position.z);
            blockToMove.transform.position = position;
            //LeanTween.moveLocalX(blockToMove, -500, 1f);
        }
    }
}