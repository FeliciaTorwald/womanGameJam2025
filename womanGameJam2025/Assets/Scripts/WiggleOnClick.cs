using UnityEngine;
using System.Collections;

public class WiggleOnClick : MonoBehaviour
{
    public float wiggleAngle = 15f;    
    public float wiggleDuration = 0.5f; 
    public float wiggleSpeed = 20f;    

    private bool isWiggling = false;
    public AudioClip wiggleSound;

    private AudioSource audioSource;

    void Awake()
    {
        // Ensure we have an AudioSource to play the sounds
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isWiggling)
            StartCoroutine(Wiggle());
    }

    IEnumerator Wiggle()
    {
        audioSource.PlayOneShot(wiggleSound);
        isWiggling = true;

        float elapsed = 0f;
        Quaternion originalRot = transform.localRotation;

        while (elapsed < wiggleDuration)
        {
            float angle = Mathf.Sin(elapsed * wiggleSpeed) * wiggleAngle;
            transform.localRotation = originalRot * Quaternion.Euler(0, 0, angle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = originalRot;
        isWiggling = false;
    }
}
