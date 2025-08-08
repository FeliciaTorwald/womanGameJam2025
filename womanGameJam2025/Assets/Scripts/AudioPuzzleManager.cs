using UnityEngine;
using System.Collections.Generic;

public class AudioPuzzleManager : MonoBehaviour
{
    [Header("Melody")]
    public List<string> correctSequence = new List<string> { "C", "E", "G", "F" };
    private List<string> playerSequence = new List<string>();

    [Header("Candles")]
    public List<Candle> candles;

    [Header("Audio Feedback")]
    public AudioClip successSound;
    public AudioClip failSound;
    public AudioSource audioSource;

    [Header("Puzzle Outcome")]
    public GameObject secretDoor; 

    public void RegisterNote(string note)
    {
        int currentIndex = playerSequence.Count;

        
        if (note == correctSequence[currentIndex])
        {
            playerSequence.Add(note);
            Debug.Log("Correct note: " + note);

            
            if (currentIndex < candles.Count)
                candles[currentIndex].LightUp();

            
            if (playerSequence.Count == correctSequence.Count)
            {
                Debug.Log("Melody complete!");
                audioSource.PlayOneShot(successSound);
                TriggerSuccess();
            }
        }
        else
        {
            Debug.Log("Wrong note: " + note);
            audioSource.PlayOneShot(failSound);
            ResetPuzzle();
        }
    }

    private void TriggerSuccess()
    {
        if (secretDoor != null)
        {
            
            secretDoor.SetActive(false);
        }

        
        foreach (var candle in candles)
        {
            candle.Extinguish();
        }

        playerSequence.Clear();
    }

    private void ResetPuzzle()
    {
        playerSequence.Clear();

        
        foreach (var candle in candles)
        {
            candle.Extinguish();
        }
    }
}
