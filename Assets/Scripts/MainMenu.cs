using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "StillAwake";
    public FadeController fadeController;

    [Header("UI Panels")]
    public GameObject optionsPanel;

    [Header("Audio")]
    public AudioSource clickSound;

    public void StartGame()
    {
        PlayClickSound();
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        Debug.Log("Start pressed");

        if (fadeController != null)
        {
            Debug.Log("Fading out...");
            yield return StartCoroutine(fadeController.FadeOut());
        }
        else
        {
            Debug.LogWarning("FadeController not assigned — loading instantly");
        }

        Debug.Log("Loading scene...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        PlayClickSound();

        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        PlayClickSound();

        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        PlayClickSound();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    void PlayClickSound()
    {
        if (clickSound != null)
            clickSound.Play();
    }
}
