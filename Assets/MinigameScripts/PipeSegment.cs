using System.Collections;
using System.Collections.Generic;
/// PipeSegment.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<XRGrabInteractable>()
            .selectEntered.AddListener(_ => Rotate90());
    }

    private void Rotate90()
    {
        transform.Rotate(0, 90, 0, Space.Self);
    }
}






