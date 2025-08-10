using UnityEngine;

public class PadlockWheel : MonoBehaviour
{
    public int wheelIndex; // Set in Inspector: 0, 1, or 2
    public PadlockManager padlockManager; // Assign the main padlock script in Inspector

    public int currentNumber = 0;
    private float rotationStep = 36f;

    void OnMouseDown()
    {
        currentNumber++;
        if (currentNumber > 9) currentNumber = 0;

        // Rotate the wheel visually
        transform.localRotation = Quaternion.Euler(0f, currentNumber * rotationStep, 0f);

        // Tell manager to update the number and check code
        if (padlockManager != null)
        {
            padlockManager.UpdateNumber(wheelIndex, currentNumber);
        }
    }
}
