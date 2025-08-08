using UnityEngine;
using System.Collections.Generic;

public class AudioPuzzleManager : MonoBehaviour
{
    public List<string> correctSequence = new List<string> { "C", "E", "G", "F" };
    private List<string> playerSequence = new List<string>();

    public AudioClip successSound;
    public AudioClip failSound;
    public AudioSource audioSource;

    public GameObject secretObject; 

    public void RegisterNote(string note)
    {
        playerSequence.Add(note);

        
        if (playerSequence.Count == correctSequence.Count)
        {
            if (IsCorrectSequence())
            {
                Debug.Log("Correct melody!");
                audioSource.PlayOneShot(successSound);
                TriggerSuccess();
            }
            else
            {
                Debug.Log("Wrong melody.");
                audioSource.PlayOneShot(failSound);
                playerSequence.Clear();
            }
        }
    }

    private bool IsCorrectSequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private void TriggerSuccess()
    {
        
        if (secretObject != null)
            secretObject.SetActive(false); 
    }
}
