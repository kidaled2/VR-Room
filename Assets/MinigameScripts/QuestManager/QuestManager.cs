using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Data")]
    public QuestLog questLog;        // ScriptableObject içindeki List<QuestEntry>
    public QuestLogUI questLogUI;    // Liste UI’ý

    [Header("World-Space UI Settings (match QuestLog order)")]
    public List<Transform> anchors;        // Sahnedeki Empty’ler: CargoAnchor, FountainAnchor, AltarAnchor, LampAnchor, TraderAnchor
    public List<Vector3> offsets;          // offset’ler: (0,1,0), (0,1,0), (0,0.7,0), (0,1,0), (0,1.2,0)
    public List<int> requiredCounts;       // tetikleme sayýlarý: 3, 1, 3, 6, 1

    private Dictionary<string, int> progress = new();

    void Start()
    {
        // Listeyi baþta doldur
        questLogUI.PopulateQuests();

        // Kolay hata yakalama:
        if (questLog.quests.Count != anchors.Count ||
            anchors.Count != offsets.Count ||
            offsets.Count != requiredCounts.Count)
        {
            Debug.LogError("QuestManager: lists lengths mismatch! Check anchors, offsets, requiredCounts.");
        }
    }

    /// <summary>
    /// Quest logdaki entry ile ayný index’li anchor/offset/requiredCount’u kullanarak tetikleme yapar.
    /// </summary>
    public void Trigger(string title)
    {
        // progress map
        if (!progress.ContainsKey(title))
            progress[title] = 0;

        // entry ve index
        var entry = questLog.quests.FirstOrDefault(q => q.title == title);
        if (entry == null)
        {
            Debug.LogWarning($"Quest '{title}' bulunamadý!");
            return;
        }
        int idx = questLog.quests.IndexOf(entry);

        // requiredCount, anchor, offset
        int req = requiredCounts[idx];
        Transform anchor = anchors[idx];
        Vector3 off = offsets[idx];

        // ilk tetiklemede liste güncelle
        if (progress[title] == 0)
            questLogUI.PopulateQuests();

        // sayaç
        progress[title]++;

        // world-space UI
        ObjectiveDisplayManager.Instance.ShowObjectiveAbove(anchor, entry.description, off);
        ObjectiveDisplayManager.Instance.UpdateStatus(progress[title], req);

        // tamamlandýysa
        if (progress[title] >= req)
        {
            entry.isCompleted = true;
            questLogUI.PopulateQuests();
            ObjectiveDisplayManager.Instance.HideObjective();
        }
    }
}

