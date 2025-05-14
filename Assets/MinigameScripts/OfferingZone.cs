using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OfferingZone : MonoBehaviour
{
    [Tooltip("Bu bölgeye kabul edilen tag")]
    public string acceptedTag;

    [Tooltip("Adak objesinin yerleþtirileceði nokta")]
    public Transform spot;

    [Tooltip("Görev yöneticisi")]
    public QuestManager questManager;

    [Tooltip("Tamamlama çaðrýsý için QuestEntry.title")]
    public string questTitle;

    private bool used = false;

    void OnTriggerEnter(Collider other)
    {
        // Zaten bir adak yerleþtirilmiþse çýk
        if (used) return;

        // Doðru türde adak mý?
        if (other.CompareTag(acceptedTag))
        {
            // 1) Objeyi 'spot' noktasýna taþý ve rotasyonu eþitle
            other.transform.position = spot.position;
            other.transform.rotation = spot.rotation;

            // 2) Bir daha kavranamasýn
            var grab = other.GetComponent<XRGrabInteractable>();
            if (grab != null) grab.enabled = false;

            used = true;

            // 3) Görevi tamamla (QuestManager ile)
            if (questManager != null && !string.IsNullOrEmpty(questTitle))
            {
                questManager.CompleteQuest(questTitle);
            }
        }
    }
}

