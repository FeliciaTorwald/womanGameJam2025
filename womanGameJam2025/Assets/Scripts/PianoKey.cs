using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string noteName; 
    public AudioClip noteSound;
    public float startAtTime = 0.5f; 

    private AudioSource audioSource;
    private AudioPuzzleManager puzzleManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        puzzleManager = FindAnyObjectByType<AudioPuzzleManager>();
    }

    private void OnMouseDown()
    {
        PlayNote();
    }

    public void PlayNote()
    {
        audioSource.Stop();

        audioSource.clip = noteSound;
        audioSource.time = Mathf.Clamp(startAtTime, 0, noteSound.length - 0.01f);

        audioSource.Play();

        puzzleManager.RegisterNote(noteName);
    }
}
