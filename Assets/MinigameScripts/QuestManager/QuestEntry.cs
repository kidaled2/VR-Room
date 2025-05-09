using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Entry")]
public class QuestEntry : ScriptableObject
{
    public string title;            // Örn. "Tüccarýn Tezgahýný Bul"
    [TextArea] public string description;  // Örn. " doðru ürünü seçip tezgaha býrak"
    [HideInInspector] public bool isCompleted = false;
}
