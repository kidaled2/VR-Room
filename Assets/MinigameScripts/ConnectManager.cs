using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConnectManager : MonoBehaviour
{
    [Tooltip("Uç noktaları sırasıyla: Pipe1/EndPoint1, Pipe1/EndPoint2, Pipe2/EndPoint3, Pipe2/EndPoint4")]
    public Transform[] endPoints;    // 4 eleman

    public ParticleSystem fountain;
    public QuestManager questManager;
    public string questTitle = "Boruları Bağla";

    [Header("Sadece mesafe kontrolü")]
    public float distanceTolerance = 0.25f;  // önceki 0.02'yi 0.25'e çektik

    int lastTriggeredPair = 0;
    bool fountainPlayed = false;

    void Update()
    {
        int totalPairs = endPoints.Length / 2;  // 2
        int connectedPairs = 0;

        // Her çift için sadece mesafe kontrolü
        for (int i = 0; i < endPoints.Length; i += 2)
        {
            float d = Vector3.Distance(
                endPoints[i].position,
                endPoints[i + 1].position
            );
            Debug.Log($"[Connect] Pair#{i / 2 + 1}: dist={d:F3} (tol={distanceTolerance})");
            if (d <= distanceTolerance)
                connectedPairs++;
        }

        // Adım adım quest trigger
        while (lastTriggeredPair < connectedPairs)
        {
            Debug.Log($"[ConnectManager] Triggering step {lastTriggeredPair + 1}/{totalPairs}");
            questManager.Trigger(questTitle);
            lastTriggeredPair++;
        }

        // İkinci çift bağlandığında bir kere particle
        if (connectedPairs == totalPairs && !fountainPlayed)
        {
            fountain.Play();
            fountainPlayed = true;
        }
        else if (connectedPairs != totalPairs && fountainPlayed)
        {
            fountain.Stop();
            fountainPlayed = false;
        }
    }
}







