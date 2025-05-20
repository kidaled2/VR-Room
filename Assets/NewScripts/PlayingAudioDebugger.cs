using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingAudioDebugger : MonoBehaviour
{
    void Update()
    {
        foreach (var src in FindObjectsOfType<AudioSource>())
        {
            if (src.isPlaying && src.clip != null)
                Debug.Log($"? Playing '{src.clip.name}' on GameObject '{src.gameObject.name}'");
        }
    }
}

