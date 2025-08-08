using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string noteName; // e.g., "C", "D#", etc.
    public AudioClip noteSound;

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
        audioSource.PlayOneShot(noteSound);
        puzzleManager.RegisterNote(noteName);
    }
}
