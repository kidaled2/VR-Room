using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointMarker : MonoBehaviour
{
    [Tooltip("Baðlanacak diðer EndPoint")]
    public Transform otherEndPoint;
    public float maxDistance = 0.2f;
    public Color baseColor = Color.gray;
    public Color targetColor = Color.green;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = baseColor;
    }

    void Update()
    {
        if (!otherEndPoint) return;
        float d = Vector3.Distance(transform.position, otherEndPoint.position);
        float t = Mathf.Clamp01(1f - (d / maxDistance));
        rend.material.color = Color.Lerp(baseColor, targetColor, t);
    }
}

