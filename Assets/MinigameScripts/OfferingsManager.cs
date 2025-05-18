using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfferingsManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("Görev baþlýðý")]
    public string questTitle = "Sunakta Adaklar";

    public void RegisterOffering()
    {
        questManager.Trigger(questTitle);
    }
}

