using UnityEngine;

public class PadlockManager : MonoBehaviour
{
    public int[] correctCode = { 3, 7, 5 };
    private int[] currentCode = new int[3];
    public AudioSource clickSound;
    public AudioSource unlockSound;
    private bool unlocked = false;

    public void UpdateNumber(int index, int number)
    {
        if (unlocked) return;

        currentCode[index] = number;

        if (clickSound != null) clickSound.Play();

        CheckCode();
    }

    private void CheckCode()
    {
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (currentCode[i] != correctCode[i]) return;
        }

        Unlock();
    }

    private void Unlock()
    {
        unlocked = true;
        if (unlockSound != null) unlockSound.Play();
        Debug.Log("Padlock unlocked!");
        // Your unlock logic here
    }
}
