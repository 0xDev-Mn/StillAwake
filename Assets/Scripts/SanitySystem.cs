using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    public float sanity = 100f; // 🔥 START FULL
    public float maxSanity = 100f;
    public float minSanity = 0f;

    [Header("Optional References")]
    public MonoBehaviour playerController;
    public GameObject gameOverScreen;
    public Transform playerCamera;
    private Vector3 originalCamPos;
    public AudioSource whisperSound;
    public Light hallwayLight;
    public JumpscareManager jumpscareManager;
    public AudioSource suddenSound;




    void Start()
    {
        if (playerCamera != null)
            originalCamPos = playerCamera.localPosition;
    }

    void Update()
    {
        if (sanity < 40f && playerCamera != null)
        {
            float intensity = (40f - sanity) * 0.01f;
            playerCamera.localPosition = originalCamPos + Random.insideUnitSphere * intensity;
        }
        else if (playerCamera != null)
        {
            playerCamera.localPosition = originalCamPos;
        }

        if (sanity < 50f && whisperSound != null)
        {
            if (!whisperSound.isPlaying)
                whisperSound.Play();
        }
        else
        {
            if (whisperSound.isPlaying)
                whisperSound.Stop();
        }

        if (sanity < 30f && hallwayLight != null)
        {
            hallwayLight.intensity = Random.Range(1f, 3f);
        }

        if (sanity < 20f)
        {
            if (Random.value < 0.002f)
            {
                jumpscareManager.TriggerJumpscare();
                if (suddenSound != null)
                    suddenSound.Play();
            }
}



    }


    // 🔻 CALLED WHEN PLAYER MISSES ANOMALIES
    public void ReduceSanity(float amount)
    {
        sanity -= amount;
        sanity = Mathf.Clamp(sanity, minSanity, maxSanity);

        Debug.Log("Sanity: " + sanity + "%");

    // SAFE CAMERA SHAKE
        if (Camera.main != null)
        {
            Camera.main.transform.localPosition += Random.insideUnitSphere * 0.1f;
        }

        if (sanity <= minSanity)
        {
            GameOver();
        }
    }

    // 🔺 CALLED WHEN PLAYER USES PILLS (LATER)
    public void IncreaseSanity(float amount)
    {
        sanity += amount;
        sanity = Mathf.Clamp(sanity, minSanity, maxSanity);

        Debug.Log("Sanity Restored: " + sanity + "%");
    }

    void GameOver()
    {
        FindObjectOfType<EndingManager>().TriggerEnding();
    }
}
