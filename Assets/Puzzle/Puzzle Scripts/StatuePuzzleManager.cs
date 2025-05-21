using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StatuePuzzleManager : MonoBehaviour
{
    [SerializeField] private int numberOfTasksToComplete;
    [SerializeField] private int currentlyCompletedTasks = 0;

    [Header("Completion Events")]
    [SerializeField] private Animator gate = null;
    [SerializeField] private string gateOpen = "PuzzleGatesOpen";

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gateOpenSound;


    public QuestManager questManager;

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
            StartCoroutine(OpenGateWithDelay());
            //gate.Play(gateOpen, 0, 0.0f);

            // Ses çal
            if (audioSource != null && gateOpenSound != null)
            {
                audioSource.PlayOneShot(gateOpenSound);
            }

            questManager.Trigger("Heykel Yerleþtirme");
        }
    }

    public void PuzzlePieceRemoved()
    {
        currentlyCompletedTasks--;
        statueCountText.text = "- " + currentlyCompletedTasks + " / 4 yerleþtirilen heykel";
    }

    private IEnumerator OpenGateWithDelay()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        gate.Play(gateOpen, 0, 0.0f);        // Animasyonu baþlat
    }
}
