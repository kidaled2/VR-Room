using System;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfTasksToComplete;
    [SerializeField] private int currentlyCompletedTasks = 0;

    [Header("Completion Events")]
    public GameObject onPuzzleCompletion;

    public QuestManager questManager;

    public void CompletedPuzzleTask()
    {
        currentlyCompletedTasks++;
        CheckForPuzzleCompletion();
    }

    private void CheckForPuzzleCompletion()
    {
        if (currentlyCompletedTasks >= numberOfTasksToComplete)
        {
            onPuzzleCompletion.SetActive(true);
            questManager.Trigger("Yapbozu Tamamla");
        }
    }

    public void PuzzlePieceRemoved()
    {
        currentlyCompletedTasks--;
    }
}
