using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;           // ? Bu satýrý ekleyin


public class QuestItem : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descText;

    public void Setup(QuestEntry entry)
    {
        titleText.text = entry.title;
        descText.text = entry.description;

        if (entry.isCompleted)
        {
            // Kart arka planýný soluklaþtýr
            GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.4f);
            titleText.color = Color.gray;
        }
    }

}
