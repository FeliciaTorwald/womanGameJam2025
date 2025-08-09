using System.Collections;
using UnityEngine;

public class PianoKey3D : MonoBehaviour
{
    public string noteName = "C";
    public AudioClip noteClip;              // drag audio clip here
    public float audioStartTime = 0.5f;

    public Color pressedColor = Color.yellow;
    private Color originalColor;
    private Renderer rend;

    public GameObject floatingTextPrefab;
    public float textLifetime = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void OnMouseDown()
    {
        PlayNote();
    }

    public void PlayNote()
    {
        if (noteClip == null)
        {
            Debug.LogWarning("No audio clip assigned!");
            return;
        }

        audioSource.clip = noteClip;
        audioSource.Play();

        // Delay setting the time so the clip can prepare
        StartCoroutine(SetAudioTimeAfterPlay(audioStartTime));

        rend.material.color = pressedColor;
        Invoke(nameof(ResetColor), 0.2f);

        if (floatingTextPrefab != null)
        {
            GameObject textObj = Instantiate(
                floatingTextPrefab,
                transform.position + Vector3.up * 0.2f,
                Quaternion.identity
            );

            var tm = textObj.GetComponent<TextMesh>();
            if (tm != null) tm.text = noteName;

            Destroy(textObj, textLifetime);
        }
    }

    private IEnumerator SetAudioTimeAfterPlay(float startTime)
    {
        yield return null; // wait one frame so the audio is prepared
        audioSource.time = startTime;
    }


    void ResetColor()
    {
        rend.material.color = originalColor;
    }
}
