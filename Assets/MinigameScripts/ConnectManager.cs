using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConnectManager : MonoBehaviour
{
    [Tooltip("Tüm boru segmentlerini buraya at (veya boþ býrak, otomatik bulunur)")]
    public PipeSegment[] segments;

    [Tooltip("Su efektini tetikleyecek Particle System")]
    public ParticleSystem fountain;

    void Awake()
    {
        // Eðer inspector'da elle atamadýysan, sahnedeki tüm PipeSegment'leri bul
        if (segments == null || segments.Length == 0)
        {
            segments = FindObjectsOfType<PipeSegment>();
            Debug.Log($"ConnectManager: Otomatik olarak {segments.Length} segment bulundu.");
        }

        if (fountain == null)
            Debug.LogWarning("ConnectManager: Fountain ParticleSystem atanmamýþ!");
    }

    void Update()
    {
        // segments dizisindeki her bir segment'in gerçek bir obje olup hizalý olduðunu kontrol et
        bool allAligned = true;
        foreach (var seg in segments)
        {
            if (seg == null || !seg.isAligned)
            {
                allAligned = false;
                break;
            }
        }

        // Eðer hepsi hizalýysa suyu aç, deðilse kapat
        if (fountain != null)
        {
            if (allAligned)
            {
                if (!fountain.isPlaying)
                    fountain.Play();
            }
            else
            {
                if (fountain.isPlaying)
                    fountain.Stop();
            }
        }
    }
}
