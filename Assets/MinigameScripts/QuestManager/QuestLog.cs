using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest Log")]
public class QuestLog : ScriptableObject
{
    public QuestEntry[] quests;
}
