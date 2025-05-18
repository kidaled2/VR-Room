using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    [Header("Quest")]
    public QuestManager questManager;
    [Tooltip("Görev baþlýðý")]
    public string questTitle = "Lamba Yerleþtir";

    public void RegisterLamp()
    {
        questManager.Trigger(questTitle);
    }
}


