
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDisplayManager : MonoBehaviour
{
    public static ObjectiveDisplayManager Instance;

    [Tooltip("Nested prefab'ý gösterir")]
    public GameObject objectivePrefab;
    [Tooltip("World-space UI objelerini toplamak için boþ GameObject (opsiyonel)")]
    public Transform worldUIContainer;

    private ObjectiveDisplay currentDisp;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// World-space'te parent'sýz instantiate,
    /// nested wrapper'ý atla, doðru pozisyona koy ve billboard + show yap.
    /// </summary>
    public void ShowObjectiveAbove(Transform target, string text, Vector3 offset)
    {
        // Önceki UI varsa temizle
        if (currentDisp != null)
            Destroy(currentDisp.gameObject);

        // 1) Prefabý sahne kökünde parent'sýz instantiate et
        var root = Instantiate(objectivePrefab);

        // 2) Direkt dünya-koordinatýna pozisyon ayarla
        root.transform.position = target.position + offset;

        // 3) Gerçek ObjectiveDisplay bileþenini bul (root veya child)
        var disp = root.GetComponent<ObjectiveDisplay>()
                ?? root.GetComponentInChildren<ObjectiveDisplay>();
        if (disp == null)
        {
            Debug.LogError("ObjectiveDisplay component'i bulunamadý! Prefab'ý kontrol et.");
            Destroy(root);
            return;
        }

        // 4) Eðer disp, root'un child'ý ise wrapper'ý sil
        if (disp.transform != root.transform)
        {
            disp.transform.SetParent(null, true); // sahne köküne al
            Destroy(root);                        // wrapper'ý kaldýr
        }

        // 5) Canvas event camera ayarý
        var canvas = disp.GetComponent<Canvas>();
        if (canvas != null && Camera.main != null)
            canvas.worldCamera = Camera.main;

        // 6) Billboard: UI her zaman kameraya baksýn
        if (Camera.main != null)
            disp.transform.rotation = Quaternion.LookRotation(
                disp.transform.position - Camera.main.transform.position);

        // 7) FollowTarget ve Show text
        disp.followTarget = target;
        disp.localOffset = offset;
        disp.Show(text);

        currentDisp = disp;
    }

    /// <summary>
    /// Eðer spawn edilmiþ UI varsa status text'i günceller.
    /// </summary>
    public void UpdateStatus(int current, int total)
    {
        if (currentDisp != null)
            currentDisp.UpdateStatus(current, total);
    }

    /// <summary>
    /// Mevcut UI'ý gizler ve yok eder.
    /// </summary>
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

