using System.Collections;
using System.Collections.Generic;
/// LampManager.cs
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("Görev baþlýðý")]
    public string questTitle = "Lamba Yerleþtir";

    /// <summary>
    /// Called by LampZoneTrigger when a lamp is placed
    /// </summary>
    public void RegisterLamp()
    {
        questManager.Trigger(questTitle);
    }
}


