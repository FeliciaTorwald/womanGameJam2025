using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lightsToToggle;

    [Header("Sounds")]
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;

    private bool lightsOn = true;
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
        lightsOn = !lightsOn;

        foreach (Light light in lightsToToggle)
        {
            light.enabled = lightsOn;
        }

        // Play the correct sound
        if (lightsOn && turnOnSound != null)
        {
            audioSource.PlayOneShot(turnOnSound);
        }
        else if (!lightsOn && turnOffSound != null)
        {
            audioSource.PlayOneShot(turnOffSound);
        }
    }
}
