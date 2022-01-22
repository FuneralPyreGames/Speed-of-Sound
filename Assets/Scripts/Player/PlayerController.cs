using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    public bool isInSinglePlayerTestMode, canMove, amMasterPlayer, amLocalPlayer, isLevelExited;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject cameraHolder, canvasForUI, countdownUI;
    [SerializeField] private Transform orientation;
    [SerializeField] private float mouseSensitivity, walkSpeed, sprintSpeed, jumpForce, smoothTime, wallDistance, minimumJumpHeight, wallRunGravity, wallRunJumpForce, camTilt, camTiltTime;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private int countdownTime = 3;

    private enum CurrentTerrain
    {
        Nature,
        Nonmetal,
        Metal
    };

    private CurrentTerrain currentTerrain;
    private FMOD.Studio.EventInstance footSteps;
    public FMODUnity.EventReference fmodEvent;
    private float Tilt {get; set;}
    [SerializeField] private bool grounded;
    private RaycastHit leftWallHit, rightWallHit;
    private bool wallLeft, wallRight = false;
    private float verticalLookRotation;
    private float moveSpeed = 5;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    private PhotonView PV;
    public bool inWallRun;
    private PlayerManager playerManager;
    public bool isMoving;
    public bool isSprinting;
    [Header("Timer")]
    [SerializeField]
    private TMP_Text timerText;
    public GameObject timerUI;
    private float elapsedTime;
    private bool timerGoing = false;
    private TimeSpan timePlaying;
    public int totalSeconds;
    public string timePlayingStr;
    private void Awake()
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
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        if (PV.IsMine)
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
            Destroy(cameraHolder);
            Destroy(rb);
            Destroy(canvasForUI);
        }
    }
    private void Update()
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
            if(gameObject.transform.position.y <= -20f)
            {
                playerManager.BackToSpawn();
            }
        }
        else if (isInSinglePlayerTestMode)
        {
            if(gameObject.transform.position.y <= -20f)
            {
                gameObject.transform.position = new Vector3(8.81841f, 1.5f , -8.542144f);
            } 
        }
        Look();
        if (!grounded)
        {
            WallRun();
        }
        Move();
        if(!inWallRun)
        {
            CheckForJumpInput();
        }
    }
    private void WallRun()
    {
        var position = transform.position;
        var right = orientation.right;
        wallLeft = Physics.Raycast(position, -right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(position, right, out rightWallHit, wallDistance);
        if (CanWallRun())
        {
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
    private bool CanWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }
    private void StartWallRun()
    {
        inWallRun = true;
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
        if (wallLeft)
        {
            Tilt = Mathf.Lerp(Tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if(wallRight)
        {
            Tilt = Mathf.Lerp(Tilt, camTilt, camTiltTime * Time.deltaTime);
        }
        playerControls.Movement.Jump.performed += _ => WallRunJump();
    }
    private void WallRunJump()
    {
        if(inWallRun)
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                var velocity = rb.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z);
                rb.velocity = velocity;
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                var velocity = rb.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z);
                rb.velocity = velocity;
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce, ForceMode.Force);
            }
        }
    }
    private void StopWallRun()
    {
        inWallRun = false;
        rb.useGravity = true;
        Tilt = Mathf.Lerp(Tilt, 0, camTiltTime);
    }
    private void Look()
    {
        transform.Rotate((Vector3.up * playerControls.Movement.LookX.ReadValue<float>()) * mouseSensitivity);
        verticalLookRotation += playerControls.Movement.LookY.ReadValue<float>() * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        cameraHolder.transform.Rotate(Vector3.forward * Tilt);
    }
    private void Move()
    {
        playerControls.Movement.Sprint.started += _ => moveSpeed = sprintSpeed;
        playerControls.Movement.Sprint.canceled += _ => moveSpeed = walkSpeed;
        if (moveSpeed == sprintSpeed)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        Vector3 moveDir = new Vector3((playerControls.Movement.GroundMovement.ReadValue<Vector2>().x), 0, (playerControls.Movement.GroundMovement.ReadValue<Vector2>().y)).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * moveSpeed, ref smoothMoveVelocity, smoothTime);
    }
    private void CheckForJumpInput()
    {
        playerControls.Movement.Jump.performed += _ => Jump();
    }
    private void Jump()
    {
        if (grounded && !inWallRun)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }
    private void FixedUpdate()
    {
        if(!isInSinglePlayerTestMode)
        {
            if(!PV.IsMine)
            {
                return;
            }
        }

        if (grounded)
        {
            DetermineTerrain();
        }

        if (moveAmount.x > 0.0000000000000000000000000f)
        {
            isMoving = true;
        }
        else if (moveAmount.z > 0.000000000000000f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        print("Is Moving = " + isMoving + " And Is Sprinting = " + isSprinting);
        print("Player's move amount is " + moveAmount);
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount)* Time.fixedDeltaTime);
    }

    private void DetermineTerrain() // This function determines what ground type the player is on
    {
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, Vector3.down, 10.0f);
        foreach (RaycastHit rayHit in hit)
        {
            if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Nonmetal"))
            {
                currentTerrain = CurrentTerrain.Nonmetal;
            }
            else if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Metal"))
            {
                currentTerrain = CurrentTerrain.Metal;
            }
            else if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Nature"))
            {
                currentTerrain = CurrentTerrain.Nature;
            }
        }
    }
    public void BeginCountdown()
    {
        StartCoroutine(CountdownToStart());
    }
    private IEnumerator CountdownToStart()
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
    private IEnumerator Timer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            totalSeconds = (int)timePlaying.TotalSeconds;
            timerText.text = timePlayingStr;
            yield return null;
        }
    }
    public void StopTimer()
    {
        timerGoing = false;
        timerUI.SetActive(false);
        
    }
    public void ExitLevel()
    {
        canMove = false;
        isLevelExited = true;
        cameraHolder.SetActive(false);
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void RPCToMainPlayer(int bonusStarNumber)
    {
        PV.RPC("RPC_TellMainPlayerBonusStarWasCollected", RpcTarget.Others, bonusStarNumber);
    }
    public void SaveBonusStar(int bonusStarNumber)
    {
        Debug.Log("Saving bonus star");
        gameController.SaveBonusStars(bonusStarNumber);
    }

    [PunRPC]
    private void RPC_TellMainPlayerBonusStarWasCollected(int bonusStarNumber)
    {
        Debug.Log("RPC heard");
        SaveBonusStar(bonusStarNumber);
    }
}