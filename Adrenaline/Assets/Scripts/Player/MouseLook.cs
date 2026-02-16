using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] private float mouseSensitivity = 0.5f;
    [SerializeField] private float smoothing = 10f;
    float mouseX, mouseY;
    float smoothedMouseX, smoothedMouseY;

    [Header("Rotation Limits")]
    [SerializeField] float xClamp = 85f;
    [SerializeField] float yClamp = 85f;

    [Header("References")]
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerBody;
    [SerializeField] Transform hands;

    [Header("Block Settings")]
    private bool isBlocking = false;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        smoothedMouseX = Mathf.Lerp(smoothedMouseX, mouseX, smoothing * Time.deltaTime);
        smoothedMouseY = Mathf.Lerp(smoothedMouseY, mouseY, smoothing * Time.deltaTime);

        if(isBlocking)
        {
            yRotation += smoothedMouseX;
            yRotation = Mathf.Clamp(yRotation, -yClamp, yClamp);

            xRotation -= smoothedMouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

            playerCamera.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            if(hands != null)
            {
                hands.localRotation = Quaternion.Euler(-xRotation, -yRotation, 0f);
            }
        }
        else
        {
            transform.Rotate(Vector3.up, smoothedMouseX);

            xRotation -= smoothedMouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            if(hands != null)
            {
                hands.localRotation = Quaternion.identity;
            }
            //Vector3 targetRotation = transform.eulerAngles;
            //targetRotation.x = xRotation;
            //playerCamera.eulerAngles = targetRotation;

            yRotation = 0f;
        }
    }
    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * mouseSensitivity;
        mouseY = mouseInput.y * mouseSensitivity;
    }

    public void SetBlocking(bool blocking)
    {
        isBlocking = blocking;
    }
}
