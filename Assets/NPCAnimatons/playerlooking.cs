using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerlooking : MonoBehaviour
{
    [Tooltip("VR Rig içindeki Kamera (Main Camera) atanmalý ve otomatik bulunabilir.")]
    public Transform cameraTransform;
    [Tooltip("Dönüþ hýzý")] public float turnSpeed = 2f;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform == null) return;
        // Yalnýzca yatay düzlemde dön
        Vector3 direction = cameraTransform.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                target,
                Time.deltaTime * turnSpeed);
        }
    }
}
