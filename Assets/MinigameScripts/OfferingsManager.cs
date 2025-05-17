using System.Collections;
using System.Collections.Generic;
/// OfferingsManager.cs
using UnityEngine;

public class OfferingsManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("Görev baþlýðý")]
    public string questTitle = "Sunakta Adaklar";

    /// <summary>
    /// Called by OfferingZone when an offering is placed
    /// </summary>
    public void RegisterOffering()
    {
        questManager.Trigger(questTitle);
    }
}

