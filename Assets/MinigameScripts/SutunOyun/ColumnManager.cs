using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    public string questTitle = "Sütun Birleþtirme";
    [Header("Parça Sayýsý")]
    public int requiredCount = 3;

    private int currentCount = 0;

    void Start()
    {
        // Görev 0/3 olarak baþlasýn
     
    }

    // SnapZone'lardan çaðrýlacak
    public void OnPartSnapped()
    {
        currentCount++;
        // Her parça takýldýðýnda questManager.Trigger ile 1 artýþ
        questManager.Trigger(questTitle);

        if (currentCount >= requiredCount)
        {
            Debug.Log("Sütun tamamlandý!");
            // Eðer dilerseniz burada ekstra bir þey de yapabilirsiniz
        }
    }
}

