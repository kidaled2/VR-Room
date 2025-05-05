// Assets/Scripts/DialogueManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Atamalarý")]
    public Animator panelAnimator;   // Panel’in Animator’ý
    public TMP_Text nameText;        // Karakter adý gösterecek text
    public TMP_Text dialogueText;    // Diyalog satýrlarýný gösterecek text

    private Queue<string> sentences;

    void Awake()
    {
        // Singleton
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.sentences == null || data.sentences.Length == 0)
        {
            Debug.LogWarning("DialogueManager: Baþlatýlacak veri yok!");
            return;
        }

        // Paneli aç
        if (panelAnimator != null)
            panelAnimator.SetBool("IsOpen", true);

        nameText.text = data.npcName;

        sentences.Clear();
        foreach (var s in data.sentences)
            sentences.Enqueue(s);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    IEnumerator TypeSentence(string line)
    {
        dialogueText.text = "";
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return null;
        }
    }

    void EndDialogue()
    {
        if (panelAnimator != null)
            panelAnimator.SetBool("IsOpen", false);
    }
}
