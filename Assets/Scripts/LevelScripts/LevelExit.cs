using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelExit : MonoBehaviour
{
    PlayerController playerController;
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player Controller")
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.ExitLevel();
        }
    }
}