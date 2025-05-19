using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [Header("Quest & UI")]
    public QuestManager questManager;
    public string questTitle = "Tüccar Görevi";

    [Header("Trade UI Prefab & Container")]
    public GameObject tradeCanvasPrefab;
    public Transform tradeUIContainer;

    [Header("Trade UI Elements (Prefab içindeki Binder’dan alacaðýz)")]
    private TradeUIBinder _binder;
    private GameObject _uiInstance;

    [Header("Game Logic")]
    [Tooltip("Teslim edilmesi istenen nesnelerin tag’leri")]
    public string[] neededTags;
    private HashSet<string> _delivered = new HashSet<string>();

    void Awake()
    {
        // Container’ý temizle (editörden drag-drop kalan vs)
        foreach (Transform c in tradeUIContainer) Destroy(c.gameObject);

        // Canvas’ý clone’la, parent’la, local pos=0
        _uiInstance = Instantiate(tradeCanvasPrefab, tradeUIContainer, false);

        // Ýçinden binder’ý al
        _binder = _uiInstance.GetComponent<TradeUIBinder>();
        if (_binder == null)
            Debug.LogError("TradeUIBinder bulunamadý! Prefab’ta binder ekli mi?");

        // Baþta kapalý olsun
        _uiInstance.SetActive(false);
    }

    void Start()
    {
        // Quest’i baþlat (0/neededTags.Length gösterir)
       

        // UI’ý açýp nelerin istendiðini yaz
        _uiInstance.SetActive(true);
        _binder.questionText.text = "Teslim etmeniz gerekenler:\n" +
            string.Join(", ", neededTags.Select(t => t.Replace("Good_", "")));
        _binder.feedbackText.text = $"0/{neededTags.Length} teslim edildi.";
    }

    void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        // Sadece neededTags içindekileri kabul et
        if (!neededTags.Contains(tag)) return;

        // Ayný tag’i birden fazla saymamak için kontrol et
        if (_delivered.Add(tag))
        {
            // QuestManager’ý çaðýr (progres += 1)
            questManager.Trigger(questTitle);

            // UI’ý güncelle
            int done = _delivered.Count;
            int total = neededTags.Length;
            _binder.questionText.text = $"{done}/{total} teslim edildi.";
            _binder.feedbackText.text = $"{tag.Replace("Good_", "")} teslim edildi!";

            // Eðer hepsi bitti ise tebrik et
            if (done == total)
            {
                _binder.feedbackText.text += "\nTüm eþyalar teslim edildi!";
                Invoke(nameof(HideUI), 3f);
            }
        }
    }

    void HideUI()
    {
        _uiInstance.SetActive(false);
    }
}

