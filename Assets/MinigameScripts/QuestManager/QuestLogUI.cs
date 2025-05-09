using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    public QuestLog questLog;
    public GameObject questItemPrefab;
    public Transform contentParent;

    void OnEnable()
    {
        PopulateQuests();
    }

    /// <summary>
    /// ContentParent altýný temizler ve güncel görevleri listeler.
    /// </summary>
    public void PopulateQuests()
    {
        // 1) Mevcut satýrlarý sil
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // 2) Her giriþ için yeni QuestItem yarat
        foreach (var entry in questLog.quests)
        {
            var go = Instantiate(questItemPrefab, contentParent);
            var qi = go.GetComponent<QuestItem>();
            qi.Setup(entry);

            // Ýstersen burada renk kodlayabilirsin:
            if (entry.isCompleted)
                go.GetComponentInChildren<TMPro.TMP_Text>().color = Color.gray;
        }
    }
}


