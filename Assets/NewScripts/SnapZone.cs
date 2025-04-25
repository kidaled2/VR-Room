using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZone : MonoBehaviour
{
    [Tooltip("Beklenen tag: Column_Base, Column_Mid veya Column_Capital")]
    public string expectedTag;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other) => TrySnap(other);
    private void OnTriggerStay(Collider other) => TrySnap(other);

    private void TrySnap(Collider other)
    {
        if (occupied) return;
        if (other.CompareTag(expectedTag))
        {
            // Snap iþlemi
            other.transform.SetParent(transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;

            // XRGrabInteractable’i devre dýþý býrak
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.interactionLayerMask = 0;

            occupied = true;
            Debug.Log(expectedTag + " baþarýyla yerleþti!");

            // Ýsteðe baðlý: UnityEvent tetikle (GameManager’a baðlamak için)
            // onSnap?.Invoke();
        }
    }

    // [Optional] public UnityEvent onSnap;



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Sarý ýþýklý bir tel kafes çizecek
        Gizmos.color = Color.yellow;
        var col = GetComponent<BoxCollider>();
        if (col != null)
            Gizmos.DrawWireCube(transform.position + col.center, col.size);
    }
#endif

}
