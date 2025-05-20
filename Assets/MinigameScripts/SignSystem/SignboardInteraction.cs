using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(XRSimpleInteractable))]
public class SignboardInteraction : MonoBehaviour
{
    [Header("Yazý Ayarlarý")]
    [Tooltip("3D TextMeshPro bileþeni")]
    public TextMeshPro text3D;
    [TextArea]
    [Tooltip("Tabelada gösterilecek açýklama metni")]
    public string description;

    [Header("Ses Ayarlarý")]
    [Tooltip("Çalýnacak ses klibi")]
    public AudioClip voiceClip;
    [Tooltip("Ses þiddetini çarpanla ayarlamak için")]
    [Range(0.1f, 3f)]
    public float volumeModifier = 1f;

    AudioSource audioSrc;
    XRSimpleInteractable interactable;

    void Awake()
    {
        // AudioSource referansýný al, klibi ata
        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = voiceClip;
        audioSrc.playOnAwake = false;
        // spatialBlend / minDistance / maxDistance ayarlarýný Inspector'dan yapabilirsiniz

        // TextMeshPro için description'ý yükle
        if (text3D != null)
            text3D.text = description;

        // XRSimpleInteractable event'lerine abone ol
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log($"[Signboard] Playing '{voiceClip?.name}' @ volume x{volumeModifier}");
        audioSrc.PlayOneShot(voiceClip, volumeModifier);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("[Signboard] Stopping audio");
        audioSrc.Stop();
    }

    void LateUpdate()
    {
        // Billboard: +Z ekseni (quad ön yüzü) her zaman kameraya dönsün
        var cam = Camera.main;
        if (cam == null) return;

        // Kamera yönüne yatay düzlemde bakýþ vektörü
        Vector3 d = cam.transform.position - transform.position;
        d.y = 0f;

        // Ýlk olarak kameraya bakacak rotasyonu al
        Quaternion lookRot = Quaternion.LookRotation(d.normalized, Vector3.up);
        // Sonra 180° Y ekseninde döndür, böylece quad'ýn +Z yüzü kameraya döner
        transform.rotation = lookRot * Quaternion.Euler(0f, 180f, 0f);
    }
}

