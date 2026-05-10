using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpscareManager : MonoBehaviour
{
    public Image jumpscareImage;
    public AudioSource jumpscareSound;

    public void TriggerJumpscare()
    {
        StartCoroutine(PlayJumpscare());
    }

    IEnumerator PlayJumpscare()
    {
        jumpscareImage.gameObject.SetActive(true);
        jumpscareImage.color = new Color(1, 1, 1, 1);

        if (jumpscareSound != null)
            jumpscareSound.Play();

        yield return new WaitForSeconds(0.2f);

        jumpscareImage.color = new Color(1, 1, 1, 0);
        jumpscareImage.gameObject.SetActive(false);
    }
}
