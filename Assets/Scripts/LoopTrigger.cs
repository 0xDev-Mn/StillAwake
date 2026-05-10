using UnityEngine;

public class LoopTrigger : MonoBehaviour
{
    public AnomalyManager anomalyManager;
    public Transform player;
    public Transform startPoint;
    public CharacterController playerController;
    public SanitySystem sanitySystem;

    void Start()
    {
        // Auto-assign if not set
        if (sanitySystem == null)
            sanitySystem = FindObjectOfType<SanitySystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (anomalyManager.AllFixed())
            {
                Debug.Log("Correct loop");
            }
            else
            {
                Debug.Log("Missed anomaly");

                if (sanitySystem != null)
                {
                    Debug.Log("Sanity penalty applied!");
                    sanitySystem.ReduceSanity(20f);
                }
                else
                {
                    Debug.LogError("SanitySystem not found!");
                }
            }

            // RESET PLAYER PROPERLY
            if (playerController != null)
            {
                playerController.enabled = false;
                player.position = startPoint.position;
                playerController.enabled = true;
            }
            else
            {
                player.position = startPoint.position;
                Debug.LogWarning("PlayerController not assigned!");
            }

            anomalyManager.StartLoop();
        }
    }
}
