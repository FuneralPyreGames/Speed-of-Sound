using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PlayerControls playerControls;
    public bool isInSinglePlayerTestMode, canMove, amMasterPlayer, amLocalPlayer, isLevelExited;
    [SerializeField] GameController gameController;
    [SerializeField] GameObject cameraHolder, canvasForUI, countdownUI;
    [SerializeField] Transform orientation;
    [SerializeField] private Camera cam;
    [SerializeField] float mouseSensitivity, walkSpeed, sprintSpeed, jumpForce, smoothTime, wallDistance, minimumJumpHeight, wallRunGravity, wallRunJumpForce, fov, wallRunFOV, wallRunFOVTime, camTilt, camTiltTime;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] int countdownTime = 3;
    public float tilt {get; private set;}
    [SerializeField] bool grounded;
    RaycastHit leftWallHit, rightWallHit;
    bool wallLeft, wallRight, wallForward = false;
    float verticalLookRotation;
    float moveSpeed = 5;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody rb;
    PhotonView PV;
    [Header("Timer")]
    [SerializeField] TMP_Text timerText;
    public GameObject timerUI;
    float elapsedTime;
    private bool timerGoing = false;
    private TimeSpan timePlaying;

    void Awake()
    {
        if(!isInSinglePlayerTestMode)
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        if(isInSinglePlayerTestMode)
        {
            canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return;
        }
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                amMasterPlayer = true;
            }
            amLocalPlayer = true;
            return;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(canvasForUI);
        }
    }
    void Update()
    {
        if(canMove == false)
        {
            return;
        }
        if(!isInSinglePlayerTestMode)
        {
            if(!PV.IsMine)
            {
                return;
            }
        }
        Look();
        if (!grounded)
        {
            WallRun();
        }
        Move();
        CheckForJumpInput();
    }
    void WallRun()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
        wallForward = Physics.Raycast(transform.position, orientation.forward, wallDistance);
        if (CanWallRun())
        {
            if (wallForward)
            {
                rb.useGravity = true;
                rb.AddForce(Vector3.down/2, ForceMode.Impulse);
                return;
            }
            if (wallLeft)
            {
                StartWallRun();
            }
            else if (wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }
    bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    void StartWallRun()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFOV, wallRunFOVTime * Time.deltaTime);
        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if(wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }
        playerControls.Movement.Jump.performed += _ => WallRunJump();
    }
    void WallRunJump()
    {
        if (wallLeft)
        {
            Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
        }
        else if (wallRight)
        {
            Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
        }
    }
    void StopWallRun()
    {
        rb.useGravity = true;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFOVTime * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime);
    }
    void Look()
    {
        transform.Rotate((Vector3.up * playerControls.Movement.LookX.ReadValue<float>()) * mouseSensitivity);
        verticalLookRotation += playerControls.Movement.LookY.ReadValue<float>() * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        cameraHolder.transform.Rotate(Vector3.forward * tilt);
    }
    void Move()
    {
        playerControls.Movement.Sprint.started += _ => moveSpeed = sprintSpeed;
        playerControls.Movement.Sprint.canceled += _ => moveSpeed = walkSpeed;
        Vector3 moveDir = new Vector3((playerControls.Movement.GroundMovement.ReadValue<Vector2>().x), 0, (playerControls.Movement.GroundMovement.ReadValue<Vector2>().y)).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * moveSpeed, ref smoothMoveVelocity, smoothTime);
    }
    void CheckForJumpInput()
    {
        playerControls.Movement.Jump.performed += _ => Jump();
    }
    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }
    void FixedUpdate()
    {
        if(!isInSinglePlayerTestMode)
        {
            if(!PV.IsMine)
            {
                return;
            }
        }
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount)* Time.fixedDeltaTime);
    }
    public void BeginCountdown()
    {
        StartCoroutine(CountdownToStart());
    }
    public IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        canMove = true;
        gameController.StartGame();
        countdownText.text = "Go!";
        timerText.text = "00 : 00.00";
        timerUI.SetActive(true);
        timerGoing = true;
        StartCoroutine(Timer());
        yield return new WaitForSeconds(.5f);
        countdownUI.SetActive(false);
    }
    public IEnumerator Timer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timerText.text = timePlayingStr;
            yield return null;
        }
    }
    public void StopTimer()
    {
        timerGoing = false;
    }
    public void ExitLevel()
    {
        canMove = false;
        isLevelExited = true;
    }
    void OnEnable()
    {
        playerControls.Enable();
    }
    void OnDisable()
    {
        playerControls.Disable();
    }
}