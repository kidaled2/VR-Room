using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LampZone : MonoBehaviour
{
    public Transform lampSpot;
    private bool placed = false;

    void OnTriggerEnter(Collider other)
    {
        if (placed) return;
        if (other.CompareTag("GasLamp"))
        {
            // 1) Pozisyon ve rotasyonu ayarla
            other.transform.position = lampSpot.position;
            other.transform.rotation = lampSpot.rotation;

            // 2) Grab edilemeyi kapat
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;

            // 3) Rigidbody üzerinde deðiþiklikler
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // A) Yerleþtirince gravity’i kapat ve kinematik yap
                rb.useGravity = false;
                rb.isKinematic = true;

                // — veya —

                // B) Sadece Y ekseninde hareketi dondur
                // rb.constraints = RigidbodyConstraints.FreezePositionY;
            }

            placed = true;
        }
    }
}
