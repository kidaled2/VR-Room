using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Header("NPC Bilgisi")]
    public string npcName;              // Inspector'da görünür

    [Header("Diyalog Cümleleri")]
    [TextArea(2, 5)]
    public string[] sentences;          // Inspector'da dizi olarak gözükür
}
