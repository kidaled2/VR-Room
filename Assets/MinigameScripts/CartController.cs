using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Eðer sayaç göstereceksen

public class CartController : MonoBehaviour
{
    public int cargoCount = 0;
    public TMP_Text statusText;

    // 1?? Görev yöneticisi referansý
    public QuestManager questManager;

    public void AddCargo()
    {
        cargoCount++;
        if (statusText != null)
            statusText.text = $"{cargoCount}/5 paket yüklendi";

        // 2?? 5 paket yüklendiðinde görevi tamamla
        if (cargoCount >= 5)
        {
            if (questManager != null)
                questManager.CompleteQuest("Paketleri Arabaya Yükle");
        }
    }
}


