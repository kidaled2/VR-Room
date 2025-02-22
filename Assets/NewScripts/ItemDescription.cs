using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // TextMeshPro kullanýyorsanýz

public class ItemDescription : MonoBehaviour
{
    [Header("UI Ayarlarý")]
    public GameObject descriptionPanel;         // Açýklama paneli referansý
    public TextMeshProUGUI descriptionText;       // Açýklama metin bileþeni (veya UnityEngine.UI.Text)
    public string description = "Bu itemin açýklamasý."; // Açýklama metniniz
    public float displayDuration = 5f;            // Panelin açýk kalacaðý süre

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Item üzerindeki XR Grab Interactable bileþenini alýyoruz.
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            // Item alýndýðýnda çalýþacak olaya abone oluyoruz.
            grabInteractable.selectEntered.AddListener(OnItemGrabbed);
        }
    }

    // Item yakalandýðýnda çaðrýlan metot
    void OnItemGrabbed(SelectEnterEventArgs args)
    {
        // Açýklama panelini aktif hale getir
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(true);
        }

        // Açýklama metnini güncelle
        if (descriptionText != null)
        {
            descriptionText.text = description;
        }

        // Belirli bir süre sonra paneli kapat
        StartCoroutine(HideDescriptionAfterDelay());
    }

    // Paneli displayDuration süresi kadar gösterip sonra kapatýr
    IEnumerator HideDescriptionAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (descriptionPanel != null)
        {
            descriptionPanel.SetActive(false);
        }
    }
}

