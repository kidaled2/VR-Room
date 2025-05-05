using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    [Tooltip("XR Rig içindeki Kamera Transform'u")]
    public Transform cameraTransform;
    [Tooltip("Dönüþ hýzý")]
    public float turnSpeed = 2f;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Yalnýzca yatay düzlemde dön
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                turnSpeed * Time.deltaTime
            );
        }
    }
}
