using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public SanitySystem sanitySystem;
    public GameObject endingScreen;
    public TextMeshProUGUI endingText;

    public void TriggerEnding()
    {
        float sanity = sanitySystem.sanity;

        endingScreen.SetActive(true);
        Time.timeScale = 0f;

        if (sanity > 60)
        {
            endingText.text = "You stayed in control.\nYou made it out.";
        }
        else if (sanity > 20)
        {
            endingText.text = "You escaped...\nbut something feels wrong.";
        }
        else
        {
            endingText.text = "You were too late.\nIt got inside your head.";
        }
    }
}
