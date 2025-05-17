/// TradeManager.cs
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    public string questTitle = "Tüccar Görevi";
    [Tooltip("Doðru item tag’i")]
    public string neededTag;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    private bool started;

    private void Start()
    {
        questionText.text = $"Lütfen '{neededTag.Replace("Good_", "")} ' seçin";
        feedbackText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (started || !other.CompareTag(neededTag)) return;

        questManager.Trigger(questTitle);
        feedbackText.text = "Doðru seçim!";
        started = true;
    }
}
