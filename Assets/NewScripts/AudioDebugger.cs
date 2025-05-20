using System.Collections;
using UnityEngine;

public class AudioDebugger : MonoBehaviour
{
    void Start()
    {
        var sources = FindObjectsOfType<AudioSource>();
        foreach (var src in sources)
        {
            Debug.Log(
              $"AudioSource on '{src.gameObject.name}': " +
              $"clip={(src.clip ? src.clip.name : "null")}, " +
              $"playOnAwake={src.playOnAwake}, loop={src.loop}"
            );
        }
    }
}
