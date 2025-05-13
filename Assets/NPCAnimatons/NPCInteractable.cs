using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class NPCInteractable : XRGrabInteractable
{
    [Tooltip("Inspector’dan atayacaðýnýz DialogueData asset’i")]
    public DialogueData dialogueData;
    [Tooltip("Diyalog sýrasýnda tetiklenecek trigger parametresi adý")]
    public string talkingTrigger = "Talking";
    [Tooltip("Animator içindeki yürüyüþ hýzý parametresi adý")]
    public string speedParam = "Speed";

    private NavMeshAgent navAgent;
    private Animator animator;
    private HashSet<string> animatorParams;

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // Animator parametre isimlerini önbelleðe al
        if (animator != null)
            animatorParams = new HashSet<string>(animator.parameters.Select(p => p.name));
    }

    void Update()
    {
        if (animator != null && navAgent != null && !string.IsNullOrEmpty(speedParam))
        {
            float speed = navAgent.velocity.magnitude;
            // Parametre varsa ayarla, yoksa loglama yapma
            if (animatorParams != null && animatorParams.Contains(speedParam))
                animator.SetFloat(speedParam, speed);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (navAgent != null) navAgent.isStopped = true;
        if (animator != null && !string.IsNullOrEmpty(talkingTrigger) && animatorParams.Contains(talkingTrigger))
            animator.SetTrigger(talkingTrigger);
        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}
