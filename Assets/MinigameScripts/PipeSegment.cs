using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    XRGrabInteractable _grab;

    void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
    }

    void Update()
    {
        if (_grab.isSelected)
        {
            var interactor = _grab.GetOldestInteractorSelecting();
            if (interactor != null)
            {
                float yAngle = interactor.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
            }
        }
    }
}







