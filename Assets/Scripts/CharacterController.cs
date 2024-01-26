using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.0f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSens = 2.0f;
    [SerializeField] private float upDownLimit = 80f;

    private float verticalRotation;
    private Camera playerCamera;
    private Vector3 currentMovement = Vector3.zero;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = this.gameObject.GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //horizontalMovement = transform.rotation * horizontalMovement;
        //currentMovement.x = horizontalMovement.x * walkSpeed;
        //currentMovement.z = horizontalMovement.z * walkSpeed;
        //characterController.Move(currentMovement * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalMovement = transform.rotation * horizontalMovement * 1.5f;
            currentMovement.x = horizontalMovement.x * walkSpeed * 1.5f;
            currentMovement.z = horizontalMovement.z * walkSpeed * 1.5f;
            characterController.Move(currentMovement * Time.deltaTime);
        }
        else
        {
            horizontalMovement = transform.rotation * horizontalMovement;
            currentMovement.x = horizontalMovement.x * walkSpeed;
            currentMovement.z = horizontalMovement.z * walkSpeed;
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    void HandleLook()
    {
        float mouseXrotation = Input.GetAxis("Mouse X") * mouseSens;
        this.transform.Rotate(0, mouseXrotation, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}