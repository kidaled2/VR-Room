using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class NoteInteraction : MonoBehaviour
{
    [Header("Not UI Ayarlarý")]
    [Tooltip("Not içeriðini gösterecek UI Paneli (Canvas altýnda olabilir).")]
    public GameObject noteUIPanel;

    [Tooltip("Notun metnini gösterecek Text bileþeni.")]
    public Text noteUIText;

    [Tooltip("Gösterilecek not metni.")]
    [TextArea]
    public string noteText = "Buraya not içeriði gelecek...";

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        // XR Grab Interactable bileþenini alýyoruz
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("Bu objede XRGrabInteractable bileþeni bulunmuyor!");
        }

        // Eðer UI Paneli tanýmlý deðilse, hata mesajý verelim.
        if (noteUIPanel == null)
        {
            Debug.LogError("Note UI Paneli atanmamýþ!");
        }
    }

    private void OnEnable()
    {
        // Objenin alýndýðý (grab) ve býrakýldýðý (release) olaylara abone oluyoruz
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        // Abonelikleri kaldýrýyoruz
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    // Notu aldýðýnýzda tetiklenecek metot
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Notu elinize aldýðýnýzda, UI panelini açýp not içeriðini gösteriyoruz
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(true);
            if (noteUIText != null)
            {
                noteUIText.text = noteText;
            }
        }
    }

    // Notu býraktýðýnýzda tetiklenecek metot
    private void OnReleased(SelectExitEventArgs args)
    {
        // Not býrakýldýðýnda, UI panelini kapatýyoruz
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(false);
        }
    }
}
