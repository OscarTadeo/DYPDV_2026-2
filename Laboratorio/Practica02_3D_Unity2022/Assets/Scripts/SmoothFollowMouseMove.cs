using UnityEngine;

public class SmoothFollowMove : MonoBehaviour
{
    public Transform target;

    public float distance = 5f;
    public float height = 2f;
    public float smoothSpeed = 10f;

    public float mouseSensitivity = 200f;
    public float minY = -40f;
    public float maxY = 80f;

    float currentX = 0f; // rotación horizontal
    float currentY = 0f; // rotación vertical

    void LateUpdate()
    {
        // Movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentX += mouseX;
        currentY -= mouseY;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // Rotar el jugador horizontalmente
        target.rotation = Quaternion.Euler(0f, currentX, 0f);

        // Calcular rotación completa para la cámara
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);

        // Calcular posición deseada
        Vector3 desiredPosition = target.position
                                  - rotation * Vector3.forward * distance
                                  + Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target.position + Vector3.up * height);
    }
}
