using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpookyFlicker : MonoBehaviour
{
    public Image flickerImage;
    public float minDelay = 10f;
    public float maxDelay = 30f;
    public float flickerDuration = 0.1f;
    public int flickerCount = 3;

    void Start()
    {
        if (flickerImage != null)
            StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < flickerCount; i++)
            {
                // Turn image on
                flickerImage.color = new Color(1, 1, 1, 0.7f); // full opacity
                yield return new WaitForSeconds(flickerDuration);

                // Turn image off
                flickerImage.color = new Color(1, 1, 1, 0); // invisible
                yield return new WaitForSeconds(flickerDuration * Random.Range(0.5f, 1.5f));
            }
        }
    }
}
