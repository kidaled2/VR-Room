using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Data")]
    public QuestLog questLog;
    public QuestLogUI questLogUI;

    [Header("World-Space UI Settings (match QuestLog order)")]
    public List<Transform> anchors;
    public List<Vector3> offsets;
    public List<int> requiredCounts;

    [Header("End Game Event")]
    public UnityEvent onAllQuestsCompleted;

    // runtime ilerleme kayýtlarý
    private Dictionary<string, int> progress = new Dictionary<string, int>();

    void Awake()
    {
        // Oyun baþlarken önce tüm kayýtlý ilerlemeyi ve tamamlanma bayraklarýný sýfýrla
        progress.Clear();
        foreach (var entry in questLog.quests)
            entry.isCompleted = false;
    }

    void Start()
    {
        // 1) Ýlk önce UI listesini temizlenmiþ verilerle doldur
        questLogUI.PopulateQuests();

        // 2) Listelerin uzunluklarýný hala kontrol et
        if (questLog.quests.Count != anchors.Count ||
            anchors.Count != offsets.Count ||
            offsets.Count != requiredCounts.Count)
        {
            Debug.LogError(
                "QuestManager: lists lengths mismatch! " +
                "Check anchors, offsets, requiredCounts."
            );
        }
    }

    public void Trigger(string title)
    {
        if (!progress.ContainsKey(title))
            progress[title] = 0;

        Debug.Log($"[QuestTrigger] '{title}' called. before={progress[title]}");

        var entry = questLog.quests.FirstOrDefault(q => q.title == title);
        if (entry == null)
        {
            Debug.LogWarning($"Quest '{title}' bulunamadý!");
            return;
        }
        int idx = questLog.quests.IndexOf(entry);

        int reqCount = requiredCounts[idx];
        Transform anc = anchors[idx];
        Vector3 off = offsets[idx];

        if (progress[title] == 0)
            questLogUI.PopulateQuests();

        progress[title]++;
        Debug.Log($"[QuestTrigger] after={progress[title]}");

        ObjectiveDisplayManager.Instance.ShowObjectiveAbove(anc, entry.description, off);
        ObjectiveDisplayManager.Instance.UpdateStatus(progress[title], reqCount);

        if (progress[title] >= reqCount)
        {
            entry.isCompleted = true;
            questLogUI.PopulateQuests();
            ObjectiveDisplayManager.Instance.HideObjective();

            if (questLog.quests.All(q => q.isCompleted))
            {
                Debug.Log("QuestManager: tüm görevler tamamlandý, event tetikleniyor.");
                onAllQuestsCompleted?.Invoke();
            }
        }
    }
}



