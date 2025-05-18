using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Data")]
    public QuestLog questLog;        // ScriptableObject içindeki List<QuestEntry>
    public QuestLogUI questLogUI;    // Liste UI’ý

    [Header("World-Space UI Settings (match QuestLog order)")]
    public List<Transform> anchors;      // Sahnedeki takip noktalarý
    public List<Vector3> offsets;        // Her quest için UI offset’i
    public List<int> requiredCounts;     // Her quest’in tamamlanma sayýsý

    private Dictionary<string, int> progress = new Dictionary<string, int>();

    void Start()
    {
        // Listeyi baþta doldur ve uzunluklarý kontrol et
        questLogUI.PopulateQuests();
        if (questLog.quests.Count != anchors.Count ||
            anchors.Count != offsets.Count ||
            offsets.Count != requiredCounts.Count)
        {
            Debug.LogError("QuestManager: lists lengths mismatch! Check anchors, offsets, requiredCounts.");
        }
    }

    /// <summary>
    /// Bir quest tetiklendiðinde çaðrýlýr.
    /// </summary>
    public void Trigger(string title)
    {
        // 1) Progress dictionary’de var mý yok mu kontrol
        if (!progress.ContainsKey(title))
            progress[title] = 0;

        Debug.Log($"[QuestTrigger] '{title}' called. progress before = {progress[title]}");

        // 2) QuestEntry ve index bul
        var entry = questLog.quests.FirstOrDefault(q => q.title == title);
        if (entry == null)
        {
            Debug.LogWarning($"Quest '{title}' bulunamadý!");
            return;
        }
        int idx = questLog.quests.IndexOf(entry);

        // 3) Ýlgili required count, anchor ve offset
        int req = requiredCounts[idx];
        Transform anchor = anchors[idx];
        Vector3 off = offsets[idx];

        Debug.Log($"[QuestTrigger] idx = {idx}, requiredCount = {req}");

        // 4) Ýlk tetiklemede listeyi güncelle
        if (progress[title] == 0)
            questLogUI.PopulateQuests();

        // 5) Sayaç artýþý
        progress[title]++;
        Debug.Log($"[QuestTrigger] progress after = {progress[title]}");

        // 6) World-space UI göster ve sayacý güncelle
        ObjectiveDisplayManager.Instance.ShowObjectiveAbove(anchor, entry.description, off);
        ObjectiveDisplayManager.Instance.UpdateStatus(progress[title], req);

        // 7) Tamamlandýysa iþaretle ve gizle
        if (progress[title] >= req)
        {
            entry.isCompleted = true;
            questLogUI.PopulateQuests();
            ObjectiveDisplayManager.Instance.HideObjective();
        }
    }
}

