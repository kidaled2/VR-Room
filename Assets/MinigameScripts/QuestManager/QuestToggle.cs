using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestToggle : MonoBehaviour
{
    public GameObject questPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // veya XR input
            questPanel.SetActive(!questPanel.activeSelf);
    }
}
