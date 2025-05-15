using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    void Awake()
    {
        var grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(_ => Rotate90());
    }

    void Rotate90()
    {
        // Y ekseni etrafýnda 90° döndür
        transform.Rotate(0, 90, 0, Space.World);
    }
}




