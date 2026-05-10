using UnityEngine;
using TMPro;
using System.Collections;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI loopText;
    public TextMeshProUGUI sanityText;
    public TextMeshProUGUI objectiveText;

    public AnomalyManager anomalyManager;
    public SanitySystem sanitySystem;

    void Start()
    {
        StartCoroutine(HideObjective());
    }

    void Update()
    {
        UpdateLoopUI();
        UpdateSanityUI();
    }

    void UpdateLoopUI()
    {
        if (anomalyManager != null)
        {
            loopText.text = "Loop: " + anomalyManager.loopCount + " / " + anomalyManager.targetLoops;
        }
    }

    void UpdateSanityUI()
    {
        if (sanitySystem == null) return;

        float sanity = sanitySystem.sanity;

        sanityText.text = "Sanity: " + Mathf.RoundToInt(sanity) + "%";

        // 🎨 COLOR SYSTEM
        if (sanity > 70)
        {
            sanityText.color = Color.green;
        }
        else if (sanity > 30)
        {
            sanityText.color = Color.yellow;
        }
        else
        {
            sanityText.color = Color.red;

            // 🔥 LOW SANITY WARNING EFFECT (pulse)
            float scale = 1f + Mathf.Sin(Time.time * 5f) * 0.1f;
            sanityText.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    IEnumerator HideObjective()
    {
        yield return new WaitForSeconds(2f);

        float duration = 1f;
        float time = 0f;

        Color originalColor = objectiveText.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            objectiveText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        objectiveText.gameObject.SetActive(false);
    }

}
