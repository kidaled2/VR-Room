// Assets/Scripts/DialogueManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Atamalarý")]
    [Tooltip("Diyalog panelinin GameObject'i")]
    public GameObject panelObject;  // Panel GameObject
    [Tooltip("NPC ismini gösteren TextMeshProUGUI")]
    public TMP_Text nameText;
    [Tooltip("Diyalog satýrlarýný gösterecek TextMeshProUGUI")]
    public TMP_Text dialogueText;
    [Tooltip("Ýleri tuþu için buton (Next)")]
    public Button nextButton;

    [Header("Yazma Hýzý (saniye)")]
    [Tooltip("Her karakter arasý bekleme süresi")]
    public float typingSpeed = 0.03f;

    private Queue<string> sentences;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sentences = new Queue<string>();

        // Next butonuna listener ekle
        if (nextButton != null)
            nextButton.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.sentences == null || data.sentences.Length == 0)
        {
            Debug.LogWarning("DialogueManager: Baþlatýlacak veri yok!");
            return;
        }

        // Paneli aktif et
        if (panelObject != null)
            panelObject.SetActive(true);

        // NPC ismini ayarla
        if (nameText != null)
            nameText.text = data.npcName;

        // Kuyruðu temizle ve cümleleri ekle
        sentences.Clear();
        foreach (var s in data.sentences)
            sentences.Enqueue(s);

        // Ýlk cümleyi göster
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Eðer kuyruk boþsa diyalog bitiþi
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Bir sonraki cümle
        string line = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(line));
    }

    private IEnumerator TypeSentence(string line)
    {
        if (dialogueText == null)
            yield break;

        dialogueText.text = string.Empty;
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        // Paneli deaktif et
        if (panelObject != null)
            panelObject.SetActive(false);

        // Butonu temizle
        if (nextButton != null)
            nextButton.onClick.RemoveListener(DisplayNextSentence);
    }
}
