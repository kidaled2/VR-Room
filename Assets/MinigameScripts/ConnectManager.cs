using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [Tooltip("Sýrasýyla tüm PipeSegment EndPoint'leri")]
    public Transform[] endPoints;
    [Tooltip("Tamamlanýnca patlayacak particle")]
    public ParticleSystem fountain;
    [Tooltip("Quest tetikleyici")]
    public QuestManager questManager;
    [Tooltip("ScriptableObject’teki Görev Baþlýðý")]
    public string questTitle = "Borularý Baðla";

    [Tooltip("Birbirine dönüklük toleransý (derece)")]
    public float angleTolerance = 15f;
    [Tooltip("Uçlar arasý maksimum mesafe (metre)")]
    public float distanceTolerance = 0.15f;

    private bool started = false;

    void Update()
    {
        bool allConnected = true;

        for (int i = 0; i < endPoints.Length - 1; i++)
        {
            Transform a = endPoints[i];
            Transform b = endPoints[i + 1];

            // 1) Uçlar arasý mesafe kontrolü
            float dist = Vector3.Distance(a.position, b.position);
            if (dist > distanceTolerance)
            {
                allConnected = false;
                break;
            }

            // 2) Yön kontrolü: a'nýn forward'u b'ye bakýyor mu?
            Vector3 toNext = (b.position - a.position).normalized;
            float angleA = Vector3.Angle(a.forward, toNext);
            if (angleA > angleTolerance)
            {
                allConnected = false;
                break;
            }

            // 3) Ayrýca b'nin forward'u da a'ya bakmalý
            float angleB = Vector3.Angle(b.forward, -toNext);
            if (angleB > angleTolerance)
            {
                allConnected = false;
                break;
            }
        }

        if (allConnected && !started)
        {
            started = true;
            fountain.Play();
            questManager.Trigger(questTitle);
        }
        else if (!allConnected && fountain.isPlaying)
        {
            fountain.Stop();
        }
    }
}



