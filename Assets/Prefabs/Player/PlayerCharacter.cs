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

    private void Awake()
    {
        moveStick.onInputValueChanged += MoveInputUpdated;
        aimStick.onInputValueChanged += AimInputUpdated;
        //initializing values
        characterController = GetComponent<CharacterController>();
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
        characterController.Move(new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime);
    }
}
