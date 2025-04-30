using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Sahneye atanacak diyalog satýrlarý")] public string[] lines;
    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;
        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            // NPC Animator'ý "isTalking" parametresini true yap
            Animator animator = GetComponentInParent<Animator>();
            if (animator != null) animator.SetBool("isTalking", true);

            // Diyaloðu baþlat
            DialogueUI.Instance?.Show(lines, EndDialogue);
        }
    }

    // Diyalog bittiðinde çaðrýlacak
    private void EndDialogue()
    {
        Animator animator = GetComponentInParent<Animator>();
        if (animator != null) animator.SetBool("isTalking", false);
    }
}