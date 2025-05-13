using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(PlayerLooking))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPCVision : MonoBehaviour
{
    [Header("Görüþ Ayarlarý")]
    public float viewRadius = 20f;
    public bool ignoreAngle = false;
    [Range(0, 360)] public float viewAngle = 120f;
    public LayerMask obstructionMask = ~0;

    [Header("Animasyon Ayarlarý")]
    public Animator animator;
    public PlayerLooking lookingScript;
    public NavMeshAgent navAgent;

    [Header("Animasyon Triggers")]
    public string headNodTrigger = "HeadNod";

    private Transform cameraT;
    private int npcLayer;
    private bool playerInSight = false;

    void Awake()
    {
        cameraT = Camera.main?.transform ?? throw new Exception("NPCVision: MainCamera bulunamadý!");
        lookingScript = lookingScript ?? GetComponent<PlayerLooking>();
        animator = animator ?? GetComponent<Animator>();
        navAgent = navAgent ?? GetComponent<NavMeshAgent>();
        npcLayer = gameObject.layer;
        obstructionMask &= ~(1 << npcLayer);
    }

    void Update()
    {
        // Walking animasyonu güncellenmiyor, NPCVision sadece head nod ve durdurma için
        Vector3 dir = cameraT.position - transform.position;
        float dist = dir.magnitude;
        Vector3 flat = new Vector3(dir.x, 0f, dir.z).normalized;
        float ang = Vector3.Angle(transform.forward, flat);

        Debug.DrawLine(transform.position, transform.position + transform.forward * viewRadius, Color.red);
        Debug.DrawLine(transform.position, cameraT.position, Color.green);

        bool inRange = dist <= viewRadius;
        bool inAngle = ignoreAngle || ang <= viewAngle;

        if (inRange && inAngle)
        {
            Vector3 eyePos = transform.position + Vector3.up * 1.5f;
            bool blocked = Physics.Linecast(eyePos, cameraT.position, obstructionMask);

            if (!blocked && !playerInSight)
            {
                playerInSight = true;
                navAgent.isStopped = true;
                lookingScript.enabled = true;
                if (animator != null) animator.SetTrigger(headNodTrigger);
            }
        }
        else if (playerInSight)
        {
            playerInSight = false;
            navAgent.isStopped = false;
            lookingScript.enabled = false;
            if (animator != null) animator.ResetTrigger(headNodTrigger);
        }
    }
}