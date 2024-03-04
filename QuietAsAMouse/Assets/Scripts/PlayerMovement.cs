using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private Camera playerCamera;

    private float cameraRotationX = 0f; // Track camera rotation separately

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        // Rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the player around the y-axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera around the x-axis (vertical rotation)
        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f); // Limit camera rotation

        // Apply rotation to camera
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);

        // Keep camera perpendicular to the floor
        Vector3 targetForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        playerCamera.transform.rotation = Quaternion.LookRotation(targetForward, Vector3.up);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
