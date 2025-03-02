using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    public float gazeDuration = 1f;  // Panelin açýlmasý için gereken süre
    private float gazeTimer = 0f;

    // Heykel bilgi panelleri
    public GameObject StatueInfo1;
    public GameObject StatueInfo2;
    public GameObject StatueInfo3;
    public GameObject StatueInfo4;

    private Transform vrCamera;   // VR kamerasý
    private bool isGazing = false;
    private GameObject currentPanel = null; // Aktif paneli takip etmek için

    void Start()
    {
        vrCamera = Camera.main.transform;

        // Baþlangýçta tüm panelleri kapat
        HideAllPanels();
    }

    void Update()
    {
        Ray ray = new Ray(vrCamera.position, vrCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f)) // 10 metreye kadar tarama yap
        {
            switch (hit.collider.tag)
            {
                case "Sophia":
                    HandleGaze(StatueInfo1);
                    break;
                case "Apeth":
                    HandleGaze(StatueInfo2);
                    break;
                case "Ennoia":
                    HandleGaze(StatueInfo3);
                    break;
                case "Episteme":
                    HandleGaze(StatueInfo4);
                    break;
                default:
                    ResetGaze();
                    break;
            }
        }
        else
        {
            ResetGaze();
        }

        // Eðer bir panel açýksa, sürekli kameraya dönük olmasýný saðla
        if (currentPanel != null)
        {
            currentPanel.transform.LookAt(vrCamera);
            //currentPanel.transform.rotation = Quaternion.Euler(0, currentPanel.transform.rotation.eulerAngles.y, 0); // Sadece yatay eksende döndür

            // 180 derece döndürerek yazýnýn ters görünmesini engelle
            currentPanel.transform.Rotate(0, 180, 0);
        }
    }

    void HandleGaze(GameObject targetPanel)
    {
        if (currentPanel != targetPanel)
        {
            ResetGaze(); // Önceki açýk paneli kapat
            currentPanel = targetPanel;
            gazeTimer = 0f; // Süreyi sýfýrla
        }

        gazeTimer += Time.deltaTime;
        if (gazeTimer >= gazeDuration && !isGazing)
        {
            ShowPanel(targetPanel);
            isGazing = true;
        }
    }

    void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void ResetGaze()
    {
        gazeTimer = 0f;
        HideAllPanels();
        isGazing = false;
        currentPanel = null;
    }

    void HideAllPanels()
    {
        if (StatueInfo1) StatueInfo1.SetActive(false);
        if (StatueInfo2) StatueInfo2.SetActive(false);
        if (StatueInfo3) StatueInfo3.SetActive(false);
        if (StatueInfo4) StatueInfo4.SetActive(false);
    }
}
