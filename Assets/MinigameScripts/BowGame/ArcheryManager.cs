// ArcheryManager.cs
using UnityEngine;

public class ArcheryManager : MonoBehaviour
{
    [Header("Quest Integration")]
    public QuestManager questManager;
    public string questTitle = "Okçuluk Görevi";
    public int requiredHits = 5;

    private int _currentHits = 0;

    void Start()
    {
        _currentHits = 0;
        Debug.Log($"[ArcheryManager] Baþladý: 0/{requiredHits}");
    }

    /// <summary>
    /// Bir hedef vurulduðunda çaðrýlýr.
    /// </summary>
    public void RegisterHit()
    {
        Debug.Log($"[ArcheryManager] RegisterHit çaðrýldý, önceki:{_currentHits}");
        if (_currentHits >= requiredHits)
        {
            Debug.Log("[ArcheryManager] Zaten tamamlandý, return.");
            return;
        }

        _currentHits++;
        Debug.Log($"[ArcheryManager] Ýlerleme: {_currentHits}/{requiredHits}");

        // QuestManager'ý her adýmda tetikle
        questManager.Trigger(questTitle);

        if (_currentHits == requiredHits)
            Debug.Log("[ArcheryManager] Tüm hedefler vuruldu!");
    }
}




