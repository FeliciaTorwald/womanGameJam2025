using UnityEngine;
using System.Collections;

public class Padlock3D : MonoBehaviour
{
    [System.Serializable]
    public class NumberWheel
    {
        public Transform wheelTransform; // The 3D wheel object
        public int currentNumber = 0;    // 0–9
        public float rotationStep = 36f; // 360° / 10 numbers = 36°
        [HideInInspector] public bool isSpinning = false; // prevent spam clicks
    }

    public NumberWheel[] wheels; // 3 wheels in inspector
    public int[] correctCode = { 3, 7, 5 }; // example correct combo

    public AudioSource clickSound;
    public AudioSource unlockSound;
    public float spinSpeed = 5f; // higher = faster spin

    private bool unlocked = false;

    void Start()
    {
        UpdateWheelRotations();
    }

    void UpdateWheelRotations()
    {
        foreach (var wheel in wheels)
        {
            float rotation = wheel.currentNumber * wheel.rotationStep;
            wheel.wheelTransform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        }
    }

    public void RotateWheel(int wheelIndex)
    {
        if (unlocked) return;
        if (wheels[wheelIndex].isSpinning) return; // ignore if spinning

        wheels[wheelIndex].currentNumber++;
        if (wheels[wheelIndex].currentNumber > 9)
            wheels[wheelIndex].currentNumber = 0;

        if (clickSound != null) clickSound.Play();

        StartCoroutine(SpinWheel(wheels[wheelIndex]));
        CheckCode();
    }

    IEnumerator SpinWheel(NumberWheel wheel)
    {
        wheel.isSpinning = true;

        float targetAngle = wheel.currentNumber * wheel.rotationStep;
        float currentAngle = wheel.wheelTransform.localRotation.eulerAngles.x;

        // Handle Unity's 0-360 wrap-around so spins always go forward
        if (targetAngle < currentAngle)
            targetAngle += 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * spinSpeed);
            wheel.wheelTransform.localRotation = Quaternion.Euler(currentAngle, 0f, 0f);
            yield return null;
        }

        wheel.wheelTransform.localRotation = Quaternion.Euler(wheel.currentNumber * wheel.rotationStep, 0f, 0f);
        wheel.isSpinning = false;
    }

    void CheckCode()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].currentNumber != correctCode[i])
                return;
        }

        Unlock();
    }

    void Unlock()
    {
        unlocked = true;
        if (unlockSound != null) unlockSound.Play();
        Debug.Log("Padlock unlocked!");
        // Add your animation or open logic here
    }
}
