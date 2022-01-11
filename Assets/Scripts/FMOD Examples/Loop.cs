using UnityEngine;
public class Loop : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Update()
    {
        playerControls.Movement.Jump.performed += _ => StartLoop();
    }

    private void StartLoop()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Your Body");
        instance.start();
    }

    private void EndLoop()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
