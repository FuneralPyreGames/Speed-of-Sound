using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BlockTrigger : MonoBehaviour
{
    public GameObject blockToMove;
    private void OnTriggerEnter(Collider other)
    {
        if(blockToMove.CompareTag("Player Two Block"))
        {
            LeanTween.moveLocalX(blockToMove, 50, 1f);
        }
        else
        {
            LeanTween.moveLocalX(blockToMove, -50, 1f);
        }
    }
}