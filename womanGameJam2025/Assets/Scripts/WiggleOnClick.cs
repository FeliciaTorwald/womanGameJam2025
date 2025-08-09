using UnityEngine;
using System.Collections;

public class WiggleOnClick : MonoBehaviour
{
    public float wiggleAngle = 15f;    
    public float wiggleDuration = 0.5f; 
    public float wiggleSpeed = 20f;    

    private bool isWiggling = false;

    void OnMouseDown()
    {
        if (!isWiggling)
            StartCoroutine(Wiggle());
    }

    IEnumerator Wiggle()
    {
        isWiggling = true;

        float elapsed = 0f;
        Quaternion originalRot = transform.localRotation;

        while (elapsed < wiggleDuration)
        {
            float angle = Mathf.Sin(elapsed * wiggleSpeed) * wiggleAngle;
            transform.localRotation = originalRot * Quaternion.Euler(0, 0, angle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = originalRot;
        isWiggling = false;
    }
}
