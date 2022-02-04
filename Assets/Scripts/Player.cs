using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private InputAction movement;
    private InputAction lookmovement;
    public GameManager gm;
    public GameObject head;
    private float virticalCamRotation;
    //public AnimationCurve accelerationcurve;
    public float speed = 12.5f;
    //public Vector3 velocity;
    public float gravitypower;
    //public float rotatespeed = 12.5f;
    public float lookspeed = 12.5f;
    public CharacterController characterController;
    //public float moveInputDeadZone;

    //int leftFingerId, rightFingerId;
    //float halfSreenWidth;
    //Vector2 touchlookInput = new Vector2();
    //Vector2 touchMoveStartPosition = new Vector2();
    //Vector2 touchMoveInput = new Vector2();

    private void Awake()
    {
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        movement = playerInputs.Walking.Walk;
        movement.Enable();

        lookmovement = playerInputs.Walking.Look;
        lookmovement.Enable();

        playerInputs.Walking.View.performed += viewObject;
        playerInputs.Walking.View.Enable();

        playerInputs.Walking.pause.performed += PauseGame;
        playerInputs.Walking.pause.Enable();
    }

    private void PauseGame(InputAction.CallbackContext obj)
    {
        gm.MenuButton();
    }

    private void viewObject(InputAction.CallbackContext obj)
    {
        Debug.Log("view");
    }

    private void OnDisable()
    {
        movement.Disable();
        playerInputs.Walking.View.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerLook();
    }

    private void PlayerMove()
    {
        var movementVector = movement.ReadValue<Vector2>();
        Vector3 move = movementVector.x * transform.right + movementVector.y * transform.forward;
        move = move.normalized * speed * Time.deltaTime;
        move.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravitypower;

        if (!characterController.isGrounded)
        {
            move.y += Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(move);
    }

    private void PlayerLook()
    {
        //var virticalCamRotation -= inputY;
        var lookInput = lookmovement.ReadValue<Vector2>();
        virticalCamRotation -= lookInput.y * lookspeed * Time.deltaTime;
        virticalCamRotation = Mathf.Clamp(virticalCamRotation, -90f, 90f);

        transform.Rotate(Vector3.up * lookInput.x * lookspeed * Time.deltaTime);
        head.transform.localRotation = Quaternion.Euler(virticalCamRotation, 0f, 0f);
    }
}
