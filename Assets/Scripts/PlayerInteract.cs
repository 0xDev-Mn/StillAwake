using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 15f;

    public TextMeshProUGUI interactText;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        bool canInteract = false;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Anomaly anomaly = hit.collider.GetComponentInParent<Anomaly>();

            if (anomaly != null && anomaly.isActive)
            {
                canInteract = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    anomaly.Fix();
                }
            }
        }

        // UI Handling
        if (interactText != null)
        {
            interactText.gameObject.SetActive(canInteract);
        }
    }
}
