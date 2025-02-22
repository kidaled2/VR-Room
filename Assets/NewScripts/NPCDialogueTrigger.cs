using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NPCDialogueTrigger : MonoBehaviour
{
    // Diyalog verilerini tutacak referans
    public DialogueData npcDialogueData;

    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.onSelectEntered.AddListener(OnNPCSelected);
        }
        else
        {
            Debug.LogWarning("XRBaseInteractable bileşeni bulunamadı!");
        }
    }

    // Oyuncu NPC'yi seçtiğinde çağrılır.
    private void OnNPCSelected(XRBaseInteractor interactor)
    {
        Debug.Log("NPC ile etkileşim başlatıldı!");
        DialogueManager.Instance.StartDialogue(npcDialogueData);
    }
}
