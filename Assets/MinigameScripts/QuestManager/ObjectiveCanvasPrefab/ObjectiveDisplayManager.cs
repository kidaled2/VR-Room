using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDisplayManager : MonoBehaviour
{
    public static ObjectiveDisplayManager Instance;

    [Tooltip("ObjectiveCanvas prefab")]
    public GameObject objectivePrefab;
    [Tooltip("WorldUI container")]
    public Transform worldUIContainer;

    private ObjectiveDisplay currentDisp;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowObjectiveAbove(Transform target, string text, Vector3 offset)
    {
        Debug.Log($"[ObjectiveDisplayManager] Show called: text='{text}', target={target?.name}, container={worldUIContainer?.name}");
        if (currentDisp != null) Destroy(currentDisp.gameObject);

        var go = Instantiate(objectivePrefab, worldUIContainer);
        var disp = go.GetComponent<ObjectiveDisplay>();
        disp.followTarget = target;
        disp.localOffset = offset;
        disp.Show(text);

        currentDisp = disp;
    }

    public void UpdateStatus(int current, int total)
    {
        if (currentDisp != null)
            currentDisp.UpdateStatus(current, total);
    }

    public void HideObjective()
    {
        if (currentDisp != null)
        {
            currentDisp.Hide();
            Destroy(currentDisp.gameObject, 0.2f);
            currentDisp = null;
        }
    }
}


