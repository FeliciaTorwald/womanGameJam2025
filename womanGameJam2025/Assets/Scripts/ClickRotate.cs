using UnityEngine;

public class ClickRotate : MonoBehaviour
{
    public float rotationStep = 36f; // Degrees to rotate per click
    public float rotationSpeed = 5f; // How fast it rotates

    private bool isRotating = false;
    private float targetAngle;

    void Start()
    {
        targetAngle = transform.localRotation.eulerAngles.x;
    }

    void OnMouseDown()
    {
        if (!isRotating)
        {
            targetAngle += rotationStep;
            if (targetAngle >= 360f) targetAngle -= 360f; // wrap-around
            StartCoroutine(RotateToTarget());
        }
    }

    System.Collections.IEnumerator RotateToTarget()
    {
        isRotating = true;
        float currentAngle = transform.localRotation.eulerAngles.x;

        // Fix for Unity's 0-360 jump
        if (targetAngle < currentAngle)
            targetAngle += 360f;

        while (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) > 0.1f)
        {
            currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
            transform.localRotation = Quaternion.Euler(currentAngle, 0f, 0f);
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(targetAngle % 360f, 0f, 0f);
        isRotating = false;
    }
}
