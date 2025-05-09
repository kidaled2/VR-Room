using UnityEngine;

public class EmissionBlink : MonoBehaviour
{
    public Color emissionColor = Color.yellow;
    public float blinkSpeed = 2f;

    private Material mat;
    private float emissionStrength;
    private bool blinking = true;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (!blinking) return;

        emissionStrength = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
        Color finalColor = emissionColor * Mathf.LinearToGammaSpace(emissionStrength * 0.2f);
        mat.SetColor("_EmissionColor", finalColor);
    }

    public void DisableBlink()
    {
        blinking = false;
        mat.SetColor("_EmissionColor", Color.black); // Emission'ý sýfýrla
    }

    public void EnableBlink()
    {
        blinking = true;
    }
}
