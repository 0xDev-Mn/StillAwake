using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 200f;
    public float minXRotation = -45f;
    public float maxXRotation = 45f;

    [Header("Camera & Shake")]
    public Transform playerCamera;
    public float shakeAmount = 0.02f;
    public float shakeSpeed = 10f;

    [Header("Footsteps")]
    public AudioSource footstepSource;
    public AudioClip footstepClip;
    public float stepInterval = 0.5f;

    private float stepTimer;
    private float xRotation = 0f;
    private CharacterController controller;
    private Vector3 originalCamLocalPos;

    private Vector3 moveInput; // store movement for footstep detection

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stepTimer = stepInterval;

        if (playerCamera != null)
            originalCamLocalPos = playerCamera.localPosition;
    }

    void Update()
    {
        Move();
        Look();
        CameraShake();
        HandleFootsteps();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveInput = transform.right * x + transform.forward * z;

        controller.Move(moveInput.normalized * moveSpeed * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        if (playerCamera != null)
            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void CameraShake()
    {
        if (playerCamera == null) return;

        if (moveInput.magnitude > 0.1f)
        {
            Vector3 shakeOffset = new Vector3(
                Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f,
                Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f,
                0f
            ) * shakeAmount;

            playerCamera.localPosition = originalCamLocalPos + shakeOffset;
        }
        else
        {
            playerCamera.localPosition = originalCamLocalPos;
        }
    }

    void HandleFootsteps()
    {
        if (footstepSource == null || footstepClip == null)
            return;

        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                footstepSource.pitch = Random.Range(0.95f, 1.05f);
                footstepSource.PlayOneShot(footstepClip);

                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }
}