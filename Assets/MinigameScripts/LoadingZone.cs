using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LoadingZone : MonoBehaviour
{
    [Tooltip("Paketi snap’leyeceðiniz nokta")]
    public Transform loadPoint;
    [Tooltip("CartController referansý")]
    public CartController cart;

    private HashSet<Collider> counted = new HashSet<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Cargo")) return;

        if (counted.Contains(other)) return;

        other.transform.position = loadPoint.position;
        other.transform.rotation = loadPoint.rotation;

        var grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null) grab.enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        cart.AddCargo();

        counted.Add(other);
    }
}



