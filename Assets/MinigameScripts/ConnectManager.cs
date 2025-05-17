using System.Collections;
using System.Collections.Generic;
/// ConnectManager.cs
using System.Linq;
using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [Header("Puzzle Segments")]
    public PipeSegment[] segments;
    [Header("Fountain")]
    public ParticleSystem fountain;
    [Header("Quest")]
    public QuestManager questManager;
    private bool started;
    private const string questTitle = "Borularý Baðla";

    private void Update()
    {
        bool allAligned = segments.All(s =>
            Mathf.Approximately(
                Mathf.Round(s.transform.eulerAngles.y / 90f) * 90f,
                s.transform.eulerAngles.y));

        if (allAligned && !started)
        {
            started = true;
            fountain.Play();
            questManager.Trigger(questTitle);
        }
        else if (!allAligned && fountain.isPlaying)
        {
            fountain.Stop();
        }
    }
}


