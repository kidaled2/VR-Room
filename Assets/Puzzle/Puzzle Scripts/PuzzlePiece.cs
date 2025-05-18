using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private PuzzleManager linkedPuzzleManager;
    [SerializeField] private Transform CorrectPuzzlePiece;
    private XRSocketInteractor socket;

    public QuestManager questManager;

    private void Awake() => socket = GetComponent<XRSocketInteractor>();

    private void OnEnable()
    {
        socket.selectEntered.AddListener(ObjectSnapped);
        socket.selectExited.AddListener(ObjectRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(ObjectSnapped);
        socket.selectExited.RemoveListener(ObjectRemoved);
    }

    private void ObjectSnapped(SelectEnterEventArgs arg0)
    {
        var snappedObjectName = arg0.interactableObject;
        if (snappedObjectName.transform.name == CorrectPuzzlePiece.name)
        {
            linkedPuzzleManager.CompletedPuzzleTask();
            questManager.Trigger("Yapbozu Tamamla");
        }
    }

    private void ObjectRemoved(SelectExitEventArgs arg0)
    {
        var removedObjectName = arg0.interactableObject;
        if (removedObjectName.transform.name == CorrectPuzzlePiece.name)
        {
            linkedPuzzleManager.PuzzlePieceRemoved();
        }
    }


}
