using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSens = 2.0f;
    [SerializeField] private float upDownLimit = 80f;

    [Header("Pickup Settings")]
    [SerializeField] private KeyCode pickupKey = KeyCode.E;
    [SerializeField] private KeyCode throwKey = KeyCode.Mouse0;
    private bool isHoldingObject = false;
    private GameObject heldObject;

    private float verticalRotation;
    private Camera playerCamera;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;
    private bool isGrounded;
    private float verticalSpeed;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        else
        {
            if (!isPaused)
            {
                HandleMovement();
                HandleLook();
                HandleJump();
                HandlePickupAndThrow();
            }
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void HandleMovement()
    {
        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalMovement = transform.rotation * horizontalMovement * 2f;
        }
        else
        {
            horizontalMovement = transform.rotation * horizontalMovement;
        }
        currentMovement.x = horizontalMovement.x * walkSpeed;
        currentMovement.z = horizontalMovement.z * walkSpeed;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (isGrounded && !Input.GetKey(KeyCode.Space))
        {
            verticalSpeed = 0f;
        }
        else
        {
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }

        currentMovement.y = verticalSpeed;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleLook()
    {
        float mouseXrotation = Input.GetAxis("Mouse X") * mouseSens;
        transform.Rotate(0, mouseXrotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalSpeed = jumpForce;
        }
    }

    void HandlePickupAndThrow()
    {
        if (Input.GetKeyDown(pickupKey) && !isHoldingObject)
        {
            TryPickupObject();
        }
        else if (Input.GetKeyDown(throwKey) && isHoldingObject)
        {
            ThrowObject();
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                isHoldingObject = true;
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.parent = playerCamera.transform;
            }
        }
    }

    void ThrowObject()
    {
        if (heldObject != null)
        {
            isHoldingObject = false;
            heldObject.transform.parent = null;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * 10f; // Adjust the force as needed
            heldObject = null;
        }
    }
}