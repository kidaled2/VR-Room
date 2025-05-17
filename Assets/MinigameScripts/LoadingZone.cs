using System.Collections;
using System.Collections.Generic;
/// LoadingZone.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LoadingZone : MonoBehaviour
{
    [Tooltip("Paketi snap’leyeceðiniz nokta")]
    public Transform loadPoint;
    [Tooltip("CartController referansý")]
    public CartController cart;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Cargo")) return;

        // Fiziksel snap
        other.transform.position = loadPoint.position;
        other.transform.rotation = loadPoint.rotation;

        // Grab ve physics kilitle
        var grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Quest trigger
        cart.AddCargo();
    }
}



