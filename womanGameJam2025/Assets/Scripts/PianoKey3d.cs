using System.Collections;
using UnityEngine;

public class PianoKey3D : MonoBehaviour
{
    public string noteName = "C";
    public AudioClip noteClip;              
    public float audioStartTime = 0.5f;

    public Color pressedColor = Color.yellow;
    private Color originalColor;
    private Renderer rend;

    //public GameObject floatingTextPrefab;
    public float textLifetime = 1f;

    private AudioSource audioSource;

    public Canvas noteCanvas;  
    public TMPro.TextMeshProUGUI noteText;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        if (noteCanvas != null)
            noteCanvas.enabled = false;
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

        
        StartCoroutine(SetAudioTimeAfterPlay(audioStartTime));

        rend.material.color = pressedColor;
        Invoke(nameof(ResetColor), 0.2f);

        if (noteCanvas != null && noteText != null)
        {
            noteText.text = noteName;
            noteCanvas.enabled = true;

            Invoke(nameof(HideCanvas), 1f);
        }
    }

    private IEnumerator SetAudioTimeAfterPlay(float startTime)
    {
        yield return null; 
        audioSource.time = startTime;
    }


    void ResetColor()
    {
        rend.material.color = originalColor;
    }

    void HideCanvas()
    {
        if (noteCanvas != null)
            noteCanvas.enabled = false;
    }
}
