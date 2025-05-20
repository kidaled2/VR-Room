using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [Header("Fade Settings")]
    [Tooltip("FadeScreen prefab’ý (Quad + FadeScreen script)")]
    public FadeScreen fadeScreenPrefab;

    [Header("Main Menu")]
    [Tooltip("Build Settings’e eklenmiþ Ana Menü sahnesinin adý")]
    public string mainMenuSceneName = "1 Start Scene";

    private FadeScreen _fadeScreenInstance;

    void Awake()
    {
        // 1) Scene’de doðrudan sahnede deðil, prefab’dan klonla
        var go = Instantiate(fadeScreenPrefab.gameObject);
        _fadeScreenInstance = go.GetComponent<FadeScreen>();

        // 2) Klon mutlaka aktif olsun
        go.SetActive(true);
    }

    public void OnGameCompleted()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        // 1) Fade-out
        _fadeScreenInstance.FadeOut();
        yield return new WaitForSeconds(_fadeScreenInstance.fadeDuration);

        // 2) Ana Menü sahnesini yükle
        SceneManager.LoadScene(mainMenuSceneName);
    }
}

