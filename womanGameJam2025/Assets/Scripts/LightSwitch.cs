using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light[] lightsToToggle;  

    private bool lightsOn = true;

    void OnMouseDown()
    {
        lightsOn = !lightsOn;

        foreach (Light light in lightsToToggle)
        {
            light.enabled = lightsOn;
        }
    }
}
