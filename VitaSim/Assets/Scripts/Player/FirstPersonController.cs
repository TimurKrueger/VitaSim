using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    #region Singletion
    public static FirstPersonController Instance;

    private void EnforceSingleton()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }
    #endregion

    // References
    public CharacterController characterController;
    [SerializeField] public Transform cameraTransform;
    [SerializeField] private Interactable currentInteractable;

    // Player settings
    public float cameraSensitivity;
    public float moveSpeed;
    public float moveInputDeadZone;
    private bool canMove = true;
    [SerializeField] private float interactionRange = 1.5f;

    // Touch detection
    int leftFingerId, rightFingerId;
    float halfScreenWidth;

    // Camera control
    Vector2 lookInput;
    float cameraPitch;

    // Camera movement;
    Vector2 moveTouchStartPosition;
    Vector2 moveInput;

    private void Awake()
    {
        EnforceSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;

        // Calculate the movement input
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);
    }
   
    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;

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

                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
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

    public void PauseMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }

    public void UpdateTextPrompt(string hitObject)
    {
        if (hitObject.Contains("Level1"))
        {
            hitObject = "Level1";
        }
        if (hitObject.Contains("Level2"))
        {
            hitObject = "Level2";
        }
        switch (hitObject)
        {
            case "Level1":
                UIInteractPrompt.Instance.promptText.text = "Press Interact button to start Level 1";
                break;
            case "Level2":
                UIInteractPrompt.Instance.promptText.text = "Press Interact button to start Level 2";
                break;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        bool foundInteractable = false;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionRange, LayerMask.GetMask("Interactable")))
        {
            var interactable = hit.transform.GetComponentInParent<Interactable>();

            if(interactable != null)
            {
                currentInteractable = interactable;
                foundInteractable = true;
                InteractButtonController.Instance.ShowButton();
            }

            if (!foundInteractable)
            {
                currentInteractable = null;
                InteractButtonController.Instance.HideButton();
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}