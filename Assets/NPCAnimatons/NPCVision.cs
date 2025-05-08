// Assets/Scripts/NPCVision.cs
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerLooking))]
[RequireComponent(typeof(Animator))]
public class NPCVision : MonoBehaviour
{
    [Header("Görüþ Ayarlarý")]
    [Tooltip("Kaç birim uzaktaki objeleri görebilsin (mesafe sýnýrý)")]
    public float viewRadius = 20f;
    [Tooltip("Açý kontrolünü atlamak istersen iþaretle (yalnýzca mesafe bazlý kontrol)")]
    public bool ignoreAngle = false;
    [Range(0, 360)]
    [Tooltip("Nesne ile NPC ön vektörü arasýndaki maksimum açý (derece)")]
    public float viewAngle = 120f;
    [Tooltip("Raycast'in hangi katmanlardaki nesnelere çarpacaðýný seçin (NPC layer’ý hariç)")]
    public LayerMask obstructionMask = ~0;

    [Header("Baðlý Bileþenler")]
    [Tooltip("NPC'nin dönmesini saðlayan PlayerLooking bileþeni")]
    public PlayerLooking lookingScript;
    [Tooltip("NPC Animator bileþeni, animasyon tetikleyicileri için")]
    public Animator animator;

    [Header("Animasyon Triggers")]
    [Tooltip("Player algýlandýðýnda tetiklenecek Trigger")] public string headNodTrigger = "HeadNod";
    [Tooltip("Görüþten çýktýðýnda tetiklenecek Trigger")] public string idleTrigger = "Idle";

    private Transform cameraT;
    private int npcLayer;
    private bool playerInSight = false;

    void Start()
    {
        // Kamera referansý
        cameraT = Camera.main?.transform;
        if (cameraT == null)
            throw new Exception("NPCVision: MainCamera bulunamadý! Tag ve sahne hiyerarþisini kontrol edin.");

        // PlayerLooking atamasý
        lookingScript = lookingScript ?? GetComponent<PlayerLooking>();
        if (lookingScript == null)
            Debug.LogError("NPCVision: PlayerLooking component bulunamadý!");
        lookingScript.enabled = false;

        // Animator atamasý
        animator = animator ?? GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("NPCVision: Animator component yok, animasyon tetiklenemeyecek.");

        // NPC layer'ýný obstructionMask'tan çýkar
        npcLayer = gameObject.layer;
        obstructionMask &= ~(1 << npcLayer);
    }

    void Update()
    {
        Vector3 dir = cameraT.position - transform.position;
        float dist = dir.magnitude;
        Vector3 flat = new Vector3(dir.x, 0f, dir.z).normalized;
        float ang = Vector3.Angle(transform.forward, flat);

        // Debug görselleþtirmeleri
        Debug.DrawLine(transform.position, transform.position + transform.forward * viewRadius, Color.red);
        Debug.DrawLine(transform.position, cameraT.position, Color.green);
        Debug.Log($"NPCVision: dist={dist:F2}, ang={ang:F1}");

        // Menzil ve açý kontrolü
        bool inRange = dist <= viewRadius;
        bool inAngle = ignoreAngle || ang <= viewAngle * 0.5f;

        if (inRange && inAngle)
        {
            Vector3 eyePos = transform.position + Vector3.up * 1.5f + transform.forward * 0.1f;
            bool blocked = Physics.Linecast(eyePos, cameraT.position, obstructionMask);
            Debug.Log($"  Raycast blocked={blocked}");

            if (!blocked)
            {
                if (!playerInSight)
                {
                    playerInSight = true;
                    lookingScript.enabled = true;
                    if (animator != null)
                        animator.SetTrigger(headNodTrigger);
                    Debug.Log(">>> Player görüldü, PlayerLooking ve HeadNod animasyonu aktifleþtirildi.");
                }
                // algýlandýktan sonra devam eden kodu engellemek için return ekleyebilirsiniz
                return;
            }
            else
            {
                Debug.Log("Ray, arada bir engele çarptý.");
            }
        }
        else if (playerInSight)
        {
            playerInSight = false;
            lookingScript.enabled = false;
            if (animator != null)
                animator.SetTrigger(idleTrigger);
            Debug.Log("--- Player görüþten çýktý, PlayerLooking ve Idle animasyonu pasif edildi.");
        }
    }
}