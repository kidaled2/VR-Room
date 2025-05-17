using System.Collections;
/// QuestLog.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestLog", menuName = "Quest/QuestLog")]
public class QuestLog : ScriptableObject
{
    public List<QuestEntry> quests = new List<QuestEntry>();
}
