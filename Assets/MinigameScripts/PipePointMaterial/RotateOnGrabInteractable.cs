using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RotateOnSelect : MonoBehaviour
{
    XRGrabInteractable _grab;

    void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        // Eðer þu anda grab tuþuna basýlý ve obje seçili ise
        if (_grab.isSelected)
        {
            // En eski (ilk) interactor'u al
            var interactor = _grab.GetOldestInteractorSelecting();
            if (interactor != null)
            {
                // Interactor'ýn Y açýsýný alýp objeyi döndür
                float yAngle = interactor.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
            }
        }
    }
}



