using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Manual2DExample : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void Update()
    {
        playerControls.Movement.Jump.performed += _ => RunInstance();
    }
    private void RunInstance()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Your Body");
        instance.start();
        instance.release();
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