using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    public Light candleLight;
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.1f;

    void Update()
    {
        candleLight.intensity = Mathf.Lerp(
            candleLight.intensity,
            Random.Range(minIntensity, maxIntensity),
            flickerSpeed
        );
    }
}
