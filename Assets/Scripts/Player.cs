using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject head;
    private float virticalCamRotation;
    public AnimationCurve accelerationcurve;
    public float speed = 12.5f;
    public Vector3 velocity;
    public float gravitypower;
    public float rotatespeed = 12.5f;
    public float lookspeed = 12.5f;
    public CharacterController characterController;
    public float moveInputDeadZone;

    int leftFingerId, rightFingerId;
    float halfSreenWidth;
    Vector2 touchlookInput = new Vector2();
    Vector2 touchMoveStartPosition = new Vector2();
    Vector2 touchMoveInput = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfSreenWidth = Screen.width / 2;
        moveInputDeadZone = MathF.Pow(Screen.height / moveInputDeadZone, 2);
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();

        if (touchMoveInput.sqrMagnitude <= moveInputDeadZone)
        {
            Debug.Log(touchMoveInput);
            PlayerMovement(0f, 0f);
        }
        else
        {
            PlayerMovement(touchMoveInput.x, touchMoveInput.y);
        }
        PlayerLook(GetInputX(touchlookInput.x),GetInputY(touchlookInput.y));
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f,0.5f,0));
        //RaycastHit hit;
        //Physics.Raycast(ray, out hit);
        //Debug.DrawLine(ray.origin, ray.direction * hit.distance);
        //Debug.Log(hit.point.ToString());
    }

    private void PlayerMovement(float outsideInputX = 0, float outsideInputY = 0)
    {
        float x = outsideInputX;
        float z = outsideInputY;
        x += Input.GetAxisRaw("Horizontal");
        z += Input.GetAxisRaw("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;
        movement = movement.normalized * speed * Time.deltaTime;
        characterController.Move(movement);

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravitypower;

        if (!characterController.isGrounded)
        {
            velocity.y = Physics.gravity.y * Time.deltaTime;
        }

        characterController.Move(velocity);
    }

    private void PlayerLook(float inputX, float inputY)
    {
        virticalCamRotation -= inputY;
        virticalCamRotation = Mathf.Clamp(virticalCamRotation, -90f, 90f);

        transform.Rotate(Vector3.up * inputX);
        head.transform.localRotation = Quaternion.Euler(virticalCamRotation, 0f, 0f);
    }

    private float GetInputX(float outsideInput)
    {
        float input = outsideInput;
        input += Input.GetAxisRaw("Mouse X");
        input += Input.GetAxisRaw("RightStickX");

        return input * rotatespeed * Time.deltaTime;
    }

    private float GetInputY(float outsideInput)
    {
        float input = outsideInput;
        input += Input.GetAxisRaw("Mouse Y");
        input += Input.GetAxisRaw("RightStickY");

        return input * lookspeed * Time.deltaTime;
    }

    private void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch(t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfSreenWidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        Debug.Log("started tracking");
                        touchMoveStartPosition = t.position;
                    }
                    //else if(t.position.x > halfSreenWidth && rightFingerId == -1)
                    //{
                    //    rightFingerId = t.fingerId;
                    //    Debug.Log("Started tracking right finger");
                    //}
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if(t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }
                    break;
                case TouchPhase.Moved:
                    if(t.fingerId == rightFingerId)
                    {
                        touchlookInput = t.deltaPosition;
                    }
                    else if(t.fingerId == leftFingerId)
                    {
                        touchMoveInput = t.position - touchMoveStartPosition;
                    }

                    break;
                case TouchPhase.Stationary:
                    if(t.fingerId == rightFingerId)
                    {
                        touchlookInput = Vector2.zero;
                    }

                    break;
            }
        }
    }
}
