// Assets/Scripts/NPCInteractable.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class NPCInteractable : XRBaseInteractable
{
    [Tooltip("Inspector’dan atayacaðýnýz DialogueData asset’i")]
    public DialogueData dialogueData;

    protected override void OnEnable()
    {
        base.OnEnable();
        selectEntered.AddListener(OnSelected);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(OnSelected);
        base.OnDisable();
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("NPC seçildi! Diyaloðu baþlatýlýyor...");
        DialogueManager.Instance.StartDialogue(dialogueData);
    }
}
