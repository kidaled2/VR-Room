using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatuePuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfTasksToComplete;
    [SerializeField] private int currentlyCompletedTasks = 0;

    [Header("Completion Events")]
    public GameObject onPuzzleCompletion;

    [Header("UI Elements")]
    public Text statueCountText;

    public void CompletedPuzzleTask()
    {
        currentlyCompletedTasks++;
        statueCountText.text = "- " + currentlyCompletedTasks + " / 4 yerleþtirilen heykel";
        CheckForPuzzleCompletion();
    }

    private void CheckForPuzzleCompletion()
    {
        if (currentlyCompletedTasks >= numberOfTasksToComplete)
        {
            onPuzzleCompletion.SetActive(true);
        }
    }

    public void PuzzlePieceRemoved()
    {
        currentlyCompletedTasks--;
        statueCountText.text = "- " + currentlyCompletedTasks + " / 4 yerleþtirilen heykel";
    }
}
