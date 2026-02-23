using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 2.0f;
    [SerializeField] private float collisionOffset = 0.2f;
    [SerializeField] private LayerMask collisionMask;

    private float yaw = 0.0f;
    private float pitch = 15.0f;
    private Vector2 lookInput;

    public void HandleLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        Debug.Log("CameraController OnLook called with: " + lookInput);
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += lookInput.x * sensitivity;
        pitch -= lookInput.y * sensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, height, -distance);

        RaycastHit hit;
        if (Physics.Linecast(target.position + Vector3.up * 0.5f, desiredPosition, out hit, collisionMask))
        {
            transform.position = hit.point + hit.normal * collisionOffset;
        } 
        else
        {
            transform.position = desiredPosition;
        }

        transform.LookAt(target.position + Vector3.up * 1.0f);
    }
}
