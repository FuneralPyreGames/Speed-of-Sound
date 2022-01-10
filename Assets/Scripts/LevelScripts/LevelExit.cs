using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelExit : MonoBehaviour
{
    PlayerController playerController;
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player Controller")) return;
        playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController.ExitLevel();
    }
}