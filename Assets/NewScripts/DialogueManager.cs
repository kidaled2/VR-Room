using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro kullanýyorsan

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    // Diyalog penceresi (Canvas üzerindeki Panel)
    public GameObject dialoguePanel;
    // Diyalog metin alaný (TextMeshProUGUI veya UnityEngine.UI.Text)
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sentences = new Queue<string>();

        // Diyalog panelini baþlangýçta kapatýyoruz.
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    // Diyaloðu baþlatan metot.
    public void StartDialogue(DialogueData dialogueData)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogueData.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Bir sonraki cümleyi gösterir.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        if (dialogueText != null)
            dialogueText.text = sentence;
    }

    // Diyaloðu sonlandýrýr.
    public void EndDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}
