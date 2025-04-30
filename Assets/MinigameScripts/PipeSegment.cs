using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class PipeSegment : MonoBehaviour
{
    public bool isAligned = false;            // Döndü mü kontrolü
    public float alignThreshold = 1f;         // Angle farký toleransý

    XRGrabInteractable grabable;
    Rigidbody rb;

    void Awake()
    {
        grabable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Grab alýndýðýnda 90° döndür
        grabable.selectEntered.AddListener(_ => Rotate90());

        // Býrakýldýðýnda fýrlamayý önle
        grabable.selectExited.AddListener(OnRelease);

        // Throw atmayý kapat (project settings’e de bakabilirsiniz)
        grabable.throwOnDetach = false;
    }

    void Rotate90()
    {
        // Boruyu Y ekseninde döndürüyoruz:
        transform.Rotate(0f, 90f, 0f, Space.World);
        CheckAlignment();
    }


    void CheckAlignment()
    {
        // Y açýsýný en yakýn 90° katýna yuvarla
        float y = transform.eulerAngles.y;
        float rounded = Mathf.Round(y / 90f) * 90f;
        isAligned = Mathf.Abs(Mathf.DeltaAngle(y, rounded)) <= alignThreshold;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Fýrlamayý tamamen durdur
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Eðer doðru hizadaysa pozisyonu dondur
        if (isAligned)
        {
            // Yalnýzca dönmesini istersen:
            rb.constraints = RigidbodyConstraints.FreezePositionX
                           | RigidbodyConstraints.FreezePositionY
                           | RigidbodyConstraints.FreezePositionZ
                           | RigidbodyConstraints.FreezeRotationX
                           | RigidbodyConstraints.FreezeRotationZ;
            // Y eksenindeki rotasyonu serbest býrak 
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }

    void OnDestroy()
    {
        grabable.selectEntered.RemoveListener(_ => Rotate90());
        grabable.selectExited.RemoveListener(OnRelease);
    }
}


