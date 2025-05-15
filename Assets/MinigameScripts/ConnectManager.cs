using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectManager : MonoBehaviour
{
    public PipeSegment[] segments;      // Sahnedeki PipeSegment bileþenleri
    public ParticleSystem fountain;     // Su efektini oynatacak sistem

    void Update()
    {
        // Her parça tam 90° katlarýna dönük mü diye kontrol et
        bool allAligned = segments.All(s =>
            Mathf.Approximately(
                Mathf.Round(s.transform.eulerAngles.y / 90) * 90,
                s.transform.eulerAngles.y));

        if (allAligned && !fountain.isPlaying)
            fountain.Play();
        else if (!allAligned && fountain.isPlaying)
            fountain.Stop();
    }
}
