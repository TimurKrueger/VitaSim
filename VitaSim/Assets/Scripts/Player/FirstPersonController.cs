using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // References
    public Transform cameraTransform;
    public CharacterController characterController;

    // Player settings
    public float cameraSensitivity;
    public float moveSpeed;
    public float moveInputDeadZone;

    // Touch detection
    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    // Camera control
    Vector2 lookInput;
    float cameraPitch;

    // Camera movement;
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;

        // Calculate the movement inpu
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }
   
    // Update is called once per frame
    void Update()
    {
        GetTouchInput();

        if (rightFingerId != -1)
        {
            Debug.Log("Rotating");
            LookAround();
        }
        if(leftFingerId != -1)
        {
            Debug.Log("Moving");
            Move();
        }
    }

    void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        leftFingerId = t.fingerId;
                        Debug.Log("Tracking left finger");

                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                        Debug.Log("Tracking right finger");
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                        Debug.Log("Stopped tracking right finger");
                    }
                    break;
                case TouchPhase.Moved: 
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerId)
                    {
                        moveInput = t.position - moveTouchStartPosition;
                    }
                    break;
                case TouchPhase.Stationary: 
                    if(t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }
    void LookAround()
    {
        // Vertical pitch rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // Horizontal yaw rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        // Don't move if the touch delta is shorter than the designated dead zone
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        // Multiply the normalized direction by the speed
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;

        // Move relatively to the local transform's direction
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }
}
