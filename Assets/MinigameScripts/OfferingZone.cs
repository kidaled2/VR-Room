using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfferingZone : MonoBehaviour
{
    [Tooltip("Bu bölgeye kabul edilen tag")]
    public string acceptedTag;
    private bool used = false;

    void OnTriggerEnter(Collider other)
    {
        // Zaten yerleþtirilmiþse çýk
        if (used) return;

        // Doðru türde adak mý?
        if (other.CompareTag(acceptedTag))
        {
            // Adak objesini zona konumuna taþý
            other.transform.position = transform.position;
            // Kavrayabilmeyi kapat
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;
            used = true;
        }
    }
}

