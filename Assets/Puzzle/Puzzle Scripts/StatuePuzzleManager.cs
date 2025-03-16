using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatuePuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfTasksToComplete;
    [SerializeField] private int currentlyCompletedTasks = 0;

    [Header("Completion Events")]
    [SerializeField] private Animator gate = null;
    [SerializeField] private string gateOpen = "GatesOpen";
    //public GameObject onPuzzleCompletion;

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
            //onPuzzleCompletion.SetActive(true);
            gate.Play(gateOpen, 0, 0.0f);
        }
    }

    public void PuzzlePieceRemoved()
    {
        currentlyCompletedTasks--;
        statueCountText.text = "- " + currentlyCompletedTasks + " / 4 yerleþtirilen heykel";
    }
}
