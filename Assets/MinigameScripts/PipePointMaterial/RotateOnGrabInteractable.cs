using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RotateOnSelect : MonoBehaviour
{
    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        // Eðer þu anda grab tuþuna basýlý ve bu interactor ile seçildiyse
        if (grab.isSelected && grab.selectingInteractor != null)
        {
            // Interactor’ýn Y açýsýný al
            float yAngle = grab.selectingInteractor.transform.eulerAngles.y;
            // Sadece Y ekseninde dön
            transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
        }
    }
}


