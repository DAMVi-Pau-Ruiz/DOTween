
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 0.1f;

    [Header("Horizontal Limits")]
    [SerializeField] private float minHorizontal = -30f;
    [SerializeField] private float maxHorizontal = 30f;

    [Header("Vertical Limits")]
    [SerializeField] private float minVertical = -20f;
    [SerializeField] private float maxVertical = 20f;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Vector3 rotation = transform.localEulerAngles;

        rotationX = rotation.y;
        rotationY = rotation.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 mouseMovement = Mouse.current.delta.ReadValue();

        rotationX += mouseMovement.x * sensitivity;
        rotationY -= mouseMovement.y * sensitivity;

        rotationX = Mathf.Clamp(rotationX, minHorizontal, maxHorizontal);
        rotationY = Mathf.Clamp(rotationY, minVertical, maxVertical);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
