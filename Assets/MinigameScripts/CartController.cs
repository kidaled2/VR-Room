using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Eðer sayaç göstereceksen

public class CartController : MonoBehaviour
{
    public int cargoCount = 0;
    public TMP_Text statusText;  // Ýstersen UI Text referansý ekle

    public void AddCargo()
    {
        cargoCount++;
        if (statusText != null)
            statusText.text = $"{cargoCount}/5 paket yüklendi";
        // 5’e ulaþýldýðýnda ekstra efekt ekleyebilirsin
    }
}

