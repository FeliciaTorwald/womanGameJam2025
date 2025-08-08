using UnityEngine;

public class Candle : MonoBehaviour
{
    public Light candleLight;
    public ParticleSystem flame;

    public void LightUp()
    {
        if (candleLight != null) candleLight.enabled = true;
        if (flame != null) flame.Play();
    }

    public void Extinguish()
    {
        if (candleLight != null) candleLight.enabled = false;
        if (flame != null) flame.Stop();
    }
}
