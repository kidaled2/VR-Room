using System.Collections;
using System.Collections.Generic;
/// CartController.cs
using UnityEngine;

public class CartController : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;

    /// <summary>
    /// Called by LoadingZone when a Cargo is placed
    /// </summary>
    public void AddCargo()
    {
        questManager.Trigger("Paketleri Arabaya Yükle");
    }
}





