using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VRStatueTouch : MonoBehaviour
{
    public GameObject infoPanel;
    public Text infoText;
    private string statueDescription = "Bu heykel, Roma dönemine aittir.";

    private void Start()
    {
        infoPanel.SetActive(false);
    }

    public void OnSelectEntered(XRBaseInteractor interactor)
    {
        infoPanel.SetActive(true);
        infoPanel.transform.position = transform.position + new Vector3(0, 2, 0);
        infoText.text = statueDescription;
    }

    public void OnSelectExited(XRBaseInteractor interactor)
    {
        infoPanel.SetActive(false);
    }
}

