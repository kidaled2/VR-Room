using System.Collections;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    public QuestLog questLog;
    public Transform contentParent;
    public GameObject questItemPrefab;

    public void PopulateQuests()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var entry in questLog.quests)
        {
            var go = Instantiate(questItemPrefab, contentParent);
            go.GetComponent<QuestItem>().Setup(entry);
        }
    }
}


