using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, ITeamInterface
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float turnAnimationSmoothLerpFactor = 10f;
    [SerializeField] CameraRig cameraRig;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int teamID = 1;
    [SerializeField] UIManager uiManager;

    CharacterController characterController;
    InventoryComponent inventoryComponent;
    HealthComponet healthComponent;
    Vector2 moveInput;
    Vector2 aimInput;

    Vector3 moveDir;
    Vector3 aimDir;

    Camera viewCamera;

    Animator animator;

    float animTurnSpeed = 0f;
    
    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    public int GetTeamID() { return teamID; }

    private void Awake()
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        aimStick.onStickTapped += AimStickTapped;
        movementComponent = GetComponent<MovementComponent>();
        //initializing values
        healthComponent = GetComponent<HealthComponet>();
        healthComponent.onHealthEmpty += StartDeath;
        characterController = GetComponent<CharacterController>();
        viewCamera = Camera.main;
        animator = GetComponent<Animator>();
        inventoryComponent = GetComponent<InventoryComponent>();
    }

    private void StartDeath(float delta, float maxHealth)
    {
        animator.SetTrigger("death");
        uiManager.SetGameplayControlEnabled(false);
        moveStick.onInputValueChanged -= MoveInputUpdated;
        aimStick.onInputValueChanged -= AimInputUpdated;
    }

    private void AimStickTapped()
    {
        animator.SetTrigger("switchWeapon");
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
        aimDir = ConvertInputToWorldDirection(aimInput);
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        moveInput = inputVal;
        moveDir = ConvertInputToWorldDirection(moveInput);
    }

    // Start is called before the first frame update
    void Start()
    {
        //starting of logics
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoveInput();
        ProcessAimInput();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float rightSpeed = Vector3.Dot(moveDir, transform.right);
        float forwardSpeed = Vector3.Dot(moveDir, transform.forward);

        animator.SetFloat("leftSpeed", -rightSpeed);
        animator.SetFloat("fwdSpeed", forwardSpeed);


        animator.SetBool("firing", aimInput.magnitude > 0);
    }

    private void ProcessAimInput()
    {
        //if aim has input, use the aim to determin the turning, if not, use the move input.
        Vector3 lookDir = aimDir.magnitude != 0 ? aimDir : moveDir; //oneliner is often bad practice.
        
        float goalAnimTurnSpeed = movementComponent.RotateTowards(lookDir);

        //smoothes out the turning
        animTurnSpeed = Mathf.Lerp(animTurnSpeed, goalAnimTurnSpeed, Time.deltaTime * turnAnimationSmoothLerpFactor);
        if (animTurnSpeed < 0.01f)
        {
            animTurnSpeed = 0f;
        }
        animator.SetFloat("turnSpeed", animTurnSpeed);
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        if(aimDir.magnitude == 0)
        {
            cameraRig.AddYawInput(moveInput.x);
        }
    }

    Vector3 ConvertInputToWorldDirection(Vector2 inputVal)
    {
        Vector3 rightDir = viewCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);

        return (rightDir * inputVal.x + upDir * inputVal.y).normalized;
    }

    private void ProcessMoveInput()
    {
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void DamagePoint()
    {
        inventoryComponent.DamagePoint();
    }
}
