using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // "Oyuna Baþla" butonuna baðlayýn.
    public void PlayGame()
    {
        // Oyun sahnesinin adýný veya indeksini girin
        SceneManager.LoadScene("GameScene");
    }

    // "Çýkýþ" butonuna baðlayýn.
    public void QuitGame()
    {
        Application.Quit();
    }
}
