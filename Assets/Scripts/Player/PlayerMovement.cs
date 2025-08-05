using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Configuration")]
    [SerializeField] private float walkSpeed = 100f;
    [SerializeField] private float mouseSensitivity = 500;
    [SerializeField] private float gravitity = -9.8f;
    [SerializeField] public Transform playerCamera;
    [SerializeField] private float rotationSpeed;
    private CharacterController characterController;
    private Vector3 velocity;
    float xRotation = 0f;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        Vector3 move = transform.right * xAxis + transform.forward * zAxis;
        characterController.Move(move * walkSpeed * Time.deltaTime);

        //gravity
        if (characterController.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += gravitity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //Rotation
    }

}
