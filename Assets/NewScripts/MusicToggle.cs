using UnityEngine;

public class MusicToggle : MonoBehaviour
{
    // Arka plan müziðini içeren AudioSource bileþeni referansý
    public AudioSource musicSource;

    void Update()
    {
        // "M" tuþuna basýldýðýnda tetiklenir
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Eðer müzik çalýyorsa duraklat, deðilse baþlat
            if (musicSource.isPlaying)
            {
                musicSource.Pause(); // Duraklatýr (son konumundan devam edebilir)
            }
            else
            {
                musicSource.Play(); // Müzik çalmaya baþlar
            }
        }
    }
}
