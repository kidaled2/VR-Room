using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Data", fileName = "New Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [Tooltip("NPC’nin diyalog baþlýðý olarak görünen adý")]
    public string npcName;

    [Tooltip("Diyalog sýrasýnda okunacak cümleler")]
    public string[] sentences;
    // -- veya --
    // public List<string> sentences;
}
