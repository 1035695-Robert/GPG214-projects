using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 1f;
    public float movementSpeed = 2f;

    public float rotationY = 0f;
    public float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 MoveDirection = transform.right * moveX + transform.forward * moveZ;
        transform.position += MoveDirection * movementSpeed * Time.deltaTime;
    }
}


