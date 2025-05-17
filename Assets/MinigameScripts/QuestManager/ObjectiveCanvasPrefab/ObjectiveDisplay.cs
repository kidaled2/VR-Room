using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveDisplay : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI statusText;

    [HideInInspector] public Transform followTarget;
    [HideInInspector] public Vector3 localOffset;

    [Header("Distance Show/Hide")]
    public float showDistance = 10f;
    public float hideDistance = 11f;

    void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position + followTarget.TransformVector(localOffset);
        var dir = (Camera.main.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-dir, Vector3.up);

        float d = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (d > hideDistance) gameObject.SetActive(false);
        else if (d <= showDistance && !gameObject.activeSelf) gameObject.SetActive(true);
    }

    public void Show(string text)
    {
        objectiveText.text = text;
        gameObject.SetActive(true);
    }

    public void UpdateStatus(int current, int total)
    {
        if (statusText != null)
            statusText.text = $"{current}/{total}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
