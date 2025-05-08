// Assets/Scripts/NPCInteractable.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPCInteractable : XRGrabInteractable
{
    [Tooltip("Inspector’dan atayacaðýnýz DialogueData asset’i")]
    public DialogueData dialogueData;

    private NavMeshAgent navAgent;
    private CharacterController characterController;
    private Animator animator;

    [Header("Animasyon Triggers")]
    [Tooltip("Diyalog baþladýðýnda tetiklenecek Trigger")] public string talkingTrigger = "Talking";
    [Tooltip("Diyalog öncesi selamlaþma animasyonu (opsiyonel)")] public string wavingTrigger = "Waving";

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // 1) Hareketi durdur
        if (navAgent != null)
            navAgent.isStopped = true;
        if (characterController != null)
            characterController.enabled = false;

        // 2) Selamlaþma animasyonu
        if (animator != null && !string.IsNullOrEmpty(wavingTrigger))
            animator.SetTrigger(wavingTrigger);

        // 3) Diyalog animasyonu
        if (animator != null && !string.IsNullOrEmpty(talkingTrigger))
            animator.SetTrigger(talkingTrigger);

        Debug.Log("NPC seçildi! Diyaloðu baþlatýlýyor...");
        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}

