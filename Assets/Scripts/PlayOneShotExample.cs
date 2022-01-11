using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayOneShotExample : MonoBehaviour
{
    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    void Update()
    {
        playerControls.Movement.Jump.performed += _ => FMODUnity.RuntimeManager.PlayOneShot("event:/Songs/Your Body");
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}