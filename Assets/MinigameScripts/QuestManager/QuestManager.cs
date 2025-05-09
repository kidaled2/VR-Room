using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public QuestLog questLog;
    public QuestLogUI questLogUI;

    /// <summary>
    /// Baþlýðý verilen görevi tamamlandý olarak iþaretler ve UI’ý yeniler.
    /// </summary>
    public void CompleteQuest(string title)
    {
        // Baþlýða göre QuestEntry bul
        var entry = questLog.quests.FirstOrDefault(q => q.title == title);
        if (entry == null)
        {
            Debug.LogWarning($"Quest '{title}' bulunamadý!");
            return;
        }

        // Tamamlandý olarak iþaretle
        entry.isCompleted = true;

        // UI’ý güncelle
        questLogUI.PopulateQuests();
    }
}
