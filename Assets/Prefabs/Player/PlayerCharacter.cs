using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;
    [SerializeField] private Joystick aimStick;
    [SerializeField] float moveSpeed = 5f;
    CharacterController characterController;
    Vector2 moveInput;
    Vector2 aimInput;

    Camera viewCamera;

    private void Awake()
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        //initializing values
        characterController = GetComponent<CharacterController>();
        viewCamera = Camera.main;
    }

    private void AimInputUpdated(Vector2 inputVal)
    {
        aimInput = inputVal;
    }

    private void MoveInputUpdated(Vector2 inputVal)
    {
        moveInput = inputVal; 
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
    }

    private void ProcessMoveInput()
    {
        Vector3 rightDir = viewCamera.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up); //cross is mpre expensive
        
        //cheaper way.
        //Vector3 upDir = viewCamera.transform.forward;
        //upDir.y = 0;
        //upDir = upDir.normalized;

        characterController.Move((upDir* moveInput.y + rightDir * moveInput.x).normalized * moveSpeed * Time.deltaTime);
    }
}
