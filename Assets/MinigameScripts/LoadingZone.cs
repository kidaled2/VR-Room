using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LoadingZone : MonoBehaviour
{
    [Tooltip("Yüklenmiþ paketin konum noktasý")]
    public Transform loadPoint;
    [Tooltip("Paket sayýsýný takip eden script")]
    public CartController cart;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cargo"))
        {
            other.transform.position = loadPoint.position;
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;
            cart.AddCargo();
        }
    }
}
