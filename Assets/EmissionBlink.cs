using UnityEngine;

public class EmissionBlink : MonoBehaviour
{
    public Color emissionColor = Color.yellow;
    public float blinkSpeed = 2f;

    private Material mat;
    private float emissionStrength;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        emissionStrength = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
        Color finalColor = emissionColor * Mathf.LinearToGammaSpace(emissionStrength * 0.5f);
        mat.SetColor("_EmissionColor", finalColor);
    }
}
