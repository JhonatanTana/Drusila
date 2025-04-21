using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{

    public Transform target;         // O personagem
    public float smoothSpeed = 0.125f;
    public Vector3 offset;           // Normalmente (0, 0, -10)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
