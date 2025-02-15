using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    // Clock hand references
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;


    private void Update()
    {
        // Get the current system time
        DateTime currentTime = DateTime.Now;

        // Calculate angles for each hand
        float hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60f) * 30f; // 360° / 12 hours = 30° per hour
        float minuteAngle = (currentTime.Minute + currentTime.Second / 60f) * 6f;  // 360° / 60 minutes = 6° per minute
        float secondAngle = currentTime.Second * 6f;                              // 360° / 60 seconds = 6° per second

        // Rotate clock hands
        if (hourHand != null)
            hourHand.localRotation = Quaternion.Euler(0f, 0f, -hourAngle); // Negative for clockwise rotation
        if (minuteHand != null)
            minuteHand.localRotation = Quaternion.Euler(0f, 0f, -minuteAngle);
        if (secondHand != null)
            secondHand.localRotation = Quaternion.Euler(0f, 0f, -secondAngle);
    }
}
