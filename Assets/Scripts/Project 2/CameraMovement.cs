using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    public float movementSpeed = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 MoveDirection = transform.right * moveX + transform.forward * moveZ;
        transform.position += MoveDirection * movementSpeed * Time.deltaTime;
    }
}


