using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StatuePiece : MonoBehaviour
{
    [SerializeField] private StatuePuzzleManager linkedPuzzleManager;
    [SerializeField] private Transform CorrectPuzzlePiece;
    private XRSocketInteractor socket;

    private void Awake() => socket = GetComponent<XRSocketInteractor>();

    [SerializeField] private EmissionBlink emissionBlink;  // Sahne içinden atanacak

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

        // Her durumda parlamayý kapat
        if (emissionBlink != null)
        {
            emissionBlink.DisableBlink();
        }

        var snappedObjectName = arg0.interactableObject;

        if (snappedObjectName.transform.name == CorrectPuzzlePiece.name)
        {
            linkedPuzzleManager.CompletedPuzzleTask();
        }
    }

    private void ObjectRemoved(SelectExitEventArgs arg0)
    {
        var removedObjectName = arg0.interactableObject;

        if (removedObjectName.transform.name == CorrectPuzzlePiece.name)
        {
            linkedPuzzleManager.PuzzlePieceRemoved();
        }
        // Çýkartýldýðýnda tekrar parlama baþlasýn istersen
        if (emissionBlink != null)
        {
            emissionBlink.EnableBlink();
        }
    }


}
