using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string noteName;
    public AudioClip noteSound;

    private AudioSource audioSource;
    private AudioPuzzleManager puzzleManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        puzzleManager = FindAnyObjectByType<AudioPuzzleManager>();
    }

    public void PlayNote()
    {
        audioSource.PlayOneShot(noteSound);
        puzzleManager.RegisterNote(noteName);
    }

    private void OnMouseDown()
    {
        PlayNote(); 
    }
}
