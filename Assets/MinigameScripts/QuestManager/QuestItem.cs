using System.Collections;
using System.Collections.Generic;
/// QuestItem.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descText;
    public Image backgroundImage;

    [Header("Colors")]
    public Color normalBgColor = Color.white;
    public Color completedBgColor = new Color(0.2f, 0.2f, 0.2f, 0.4f);
    public Color normalTextColor = Color.black;
    public Color completedTextColor = Color.gray;

    public void Setup(QuestEntry entry)
    {
        titleText.text = entry.title;
        descText.text = entry.description;

        if (entry.isCompleted)
        {
            backgroundImage.color = completedBgColor;
            titleText.color = completedTextColor;
            descText.color = completedTextColor;
        }
        else
        {
            backgroundImage.color = normalBgColor;
            titleText.color = normalTextColor;
            descText.color = normalTextColor;
        }
    }
}



