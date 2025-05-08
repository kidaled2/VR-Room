using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    public string neededTag;
    public TMP_Text questionText;
    public TMP_Text feedbackText;

    void Start()
    {
        questionText.text = $"Lütfen '{neededTag.Replace("Good_", "")}' seçin";
        feedbackText.text = "";
    }

    public void OnItemPlaced(GameObject item)
    {
        if (item.CompareTag(neededTag))
            feedbackText.text = "Doðru seçim!";
        else
            feedbackText.text = "Yanlýþ, tekrar deneyin.";
    }

    void OnTriggerEnter(Collider other)
    {
        OnItemPlaced(other.gameObject);
    }

}
