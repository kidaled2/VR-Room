using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    [Tooltip("Beklenen tag: Column_Base, Column_Mid veya Column_Capital")]
    public string expectedTag;

    [Tooltip("ColumnManager component’i (ölçeði 1,1,1)")]
    public ColumnManager columnManager;

    [Tooltip("Eðer boþ býrakýlýrsa ColumnManager’ýn altýna baðlanýr")]
    public Transform snapParentOverride;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        TrySnap(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TrySnap(other);
    }

    private void TrySnap(Collider other)
    {
        if (occupied) return;
        if (!other.CompareTag(expectedTag)) return;

        // 1) Hangi parent'a baðlayacaðýz?
        Transform snapParent = snapParentOverride != null
            ? snapParentOverride
            : (columnManager != null ? columnManager.transform : null);

        // 2) World-space pozisyonu koruyarak parent’a taþý
        if (snapParent != null)
            other.transform.SetParent(snapParent, worldPositionStays: true);

        // 3) Parçayý tam olarak SnapZone'un pozisyonuna hizala
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        // 4) Rigidbody’yi al ve kinematik yap, hareketi kilitle
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        // 5) Collider’ý kapat
        Collider col = other.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 6) Grab etkileþimini pasifleþtir
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            // “Boþ” interaction layer için sýfýr mask
            grab.interactionLayers = InteractionLayerMask.GetMask();
        }

        occupied = true;
        Debug.Log($"{expectedTag} baþarýyla yerleþti ve sabitlendi!");

        // 7) ColumnManager’a haber ver
        if (columnManager != null)
            columnManager.OnPartSnapped();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + bc.center, bc.size);
        }
    }
#endif
}


